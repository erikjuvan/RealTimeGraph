//#define PC

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using ZedGraph;

namespace RealTimeGraph
{
    public partial class Form1 : Form
    {
        bool threadActive = false;
        bool filtered = true;

        const int N_CHANNELS = 8;
        const int DATA_CHANNEL_SIZE = 100;
        const int DATA_BUFFER_SIZE = DATA_CHANNEL_SIZE * N_CHANNELS;
        const int DATA_WIDTH = 4;

        const int PACKET_SIZE = DATA_BUFFER_SIZE * DATA_WIDTH;
        const int PAIR_LIST_MULTIPLES = 70;
        const int PAIR_LIST_DATA_SIZE = DATA_CHANNEL_SIZE * PAIR_LIST_MULTIPLES;

        static SerialPort serialPort;

        byte[] packetData = new byte[PACKET_SIZE];
        int[] intPacketData = new int[DATA_BUFFER_SIZE];
        float[] floatPacketData = new float[DATA_BUFFER_SIZE];

        //short[,] adcData = new short[DATA_BUFFER_SIZE, N_CHANNELS];
        float[,] adcProcessedData = new float[N_CHANNELS, DATA_CHANNEL_SIZE];

        GraphPane myPane;

        PointPairList[] adcPairList = new PointPairList[N_CHANNELS];

        PointPairList[] adcPairListRaw = new PointPairList[N_CHANNELS];

        List<PointPairList>[] adcHistory = new List<PointPairList>[N_CHANNELS];

        Color[] color_array = { Color.Red, Color.Green, Color.Blue, Color.Black, Color.Brown, Color.Orange, Color.Aqua, Color.Firebrick, Color.DarkCyan, Color.Peru, Color.HotPink, Color.Orchid };

        int adcHistoryIdx = 0;
        int adcHistorySize = 0;

        bool recordData = false;

        long stopwatch_usb_timer_total_us;
        long stopwatch_graphplot_us;

        StringBuilder[] textFile = new StringBuilder[N_CHANNELS];

        int Blind_time_const = 500;
        int[] blind_time = new int[N_CHANNELS];
        int globCapsuleCount = 0;
        int[] capsuleCount = new int[N_CHANNELS];
        bool[] capsulePresent = new bool[N_CHANNELS];

        float threashold = 7.0F;

        bool single_capture = false;
        bool single_capture_caught = false;
        int single_capture_cntr = 0;

        public Form1()
        {
            InitializeComponent();
        }

        void AddRawSignal()
        {
            for (int i = 0; i < N_CHANNELS; i++)
                myPane.AddCurve("adcRaw" + (i + 1).ToString(), adcPairListRaw[i], color_array[i], SymbolType.None);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort = new SerialPort();
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.None;
            serialPort.BaudRate = 1000;
            serialPort.Encoding = Encoding.Default;

            for (int ch_i = 0; ch_i < N_CHANNELS; ch_i++)
            {
                adcHistory[ch_i] = new List<PointPairList>();
                adcPairList[ch_i] = new PointPairList();
                adcPairListRaw[ch_i] = new PointPairList();
                for (int i = 0; i < PAIR_LIST_DATA_SIZE; i++)
                {
                    adcPairList[ch_i].Add(i, 0);
                    adcPairListRaw[ch_i].Add(i, 0);
                }
            }

            myPane = zedGraphControl1.GraphPane;
            for (int i = 0; i < N_CHANNELS; i++)
                myPane.AddCurve("adc" + (i + 1).ToString(), adcPairList[i], color_array[i], SymbolType.None);

            zedGraphControl1.GraphPane.YAxis.Scale.Max = 100;
            zedGraphControl1.GraphPane.YAxis.Scale.Min = -20;
            zedGraphControl1.GraphPane.XAxis.Scale.Max = PAIR_LIST_DATA_SIZE + PAIR_LIST_DATA_SIZE / 20;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            myPane.Title.Text = "LINEX sorting control devel";
            myPane.XAxis.Title.Text = "Sample";
            myPane.YAxis.Title.Text = "ADC value";

            for (int i = 0; i < textFile.Length; i++)
            {
                textFile[i] = new StringBuilder();
            }

            //AddRawSignal();
        }

        private int FindStartFrame(byte[] ba, int offset, int len)
        {
            for (int i = offset; i < (len - 1); i++)
            {
                if (ba[i] == 0xFF && i + 1 < len)
                    if (ba[i + 1] == 0xFF)
                        return i;
            }
            return -1;
        }

        public static void ShiftLeft(PointPairList lst, int shifts)
        {
            for (int i = shifts; i < lst.Count; i++)
                lst[i - shifts].Y = lst[i].Y;

            for (int i = lst.Count - shifts; i < lst.Count; i++)
                lst[i].Y = 0;
        }

        int bytesToRead = 0;
        private void HandleUSBTraffic()
        {
            while (threadActive)
            {
                bytesToRead = serialPort.BytesToRead;
                if (bytesToRead >= PACKET_SIZE)
                {
                    long start = Stopwatch.GetTimestamp();
                    int dataRead = serialPort.Read(packetData, 0, PACKET_SIZE < bytesToRead ? PACKET_SIZE : bytesToRead);
                    if (!filtered)
                    {
                        System.Buffer.BlockCopy(packetData, 0, intPacketData, 0, dataRead);
                        //ProcessData(ref adcProcessedData, ref intPacketData, dataRead / DATA_WIDTH);
                        RawCopyData(ref adcProcessedData, ref intPacketData, dataRead / DATA_WIDTH);
                    }
                    else
                    {
                        System.Buffer.BlockCopy(packetData, 0, floatPacketData, 0, dataRead);
                        RawCopyData(ref adcProcessedData, ref floatPacketData, dataRead / DATA_WIDTH);
                    }
                    FillPairList(ref adcProcessedData, dataRead / DATA_WIDTH);
                    stopwatch_usb_timer_total_us = (long)((double)(Stopwatch.GetTimestamp() - start) * (double)(1e6 / (double)Stopwatch.Frequency));
                }
            }
        }

        public void RawCopyData(ref float[,] dst, ref float[] data, int n_samples)
        {
            for (int i = 0; i < n_samples; i++)
                dst[i % N_CHANNELS, i / N_CHANNELS] = data[i];
        }
        public void RawCopyData(ref float[,] dst, ref int[] data, int n_samples)
        {
            for (int i = 0; i < n_samples; i++)
                dst[i % N_CHANNELS, i / N_CHANNELS] = (float)data[i];
        }

        // new short[N_CHANNELS, DATA_CHANNEL_SIZE];
        float[,] u = new float[N_CHANNELS, DATA_CHANNEL_SIZE];
        float[,] w = new float[N_CHANNELS, DATA_CHANNEL_SIZE];
        float[,] y = new float[N_CHANNELS, DATA_CHANNEL_SIZE];
        float[,] ftr = new float[N_CHANNELS, DATA_CHANNEL_SIZE];
        public void ProcessData(ref float[,] dst, ref float[] data, int n_samples)
        {
            const float y_scale = (float)(1 / 0.57);
            const float threashold = (float)10.0;
            float a0 = (float)0.01;
            float a1 = (float)0.03;
            float ftr_a0 = (float)0.03;

            // LOW PASS: ylp[i] = a * x[i] + b * ylp[i-1]
            for (int i = 0; i < N_CHANNELS; i++)
                u[i, 0] = a0 * data[i] + (1 - a0) * u[i, DATA_CHANNEL_SIZE - 1];
            //u[i, 0] = shortPacketData[i];
            for (int i = N_CHANNELS; i < n_samples; i++)
                u[i % N_CHANNELS, i / N_CHANNELS] = a0 * data[i] + (1 - a0) * u[i % N_CHANNELS, (i - N_CHANNELS) / N_CHANNELS];

            for (int i = 0; i < N_CHANNELS; i++)
                w[i, 0] = a1 * u[i, DATA_CHANNEL_SIZE - 1] + (1 - a1) * w[i, DATA_CHANNEL_SIZE - 1];
            for (int i = N_CHANNELS; i < n_samples; i++)
                w[i % N_CHANNELS, i / N_CHANNELS] = a1 * u[i % N_CHANNELS, i / N_CHANNELS] + (1 - a1) * w[i % N_CHANNELS, (i - N_CHANNELS) / N_CHANNELS];

            for (int i = 0; i < n_samples; i++)
                y[i % N_CHANNELS, i / N_CHANNELS] = u[i % N_CHANNELS, i / N_CHANNELS] - w[i % N_CHANNELS, i / N_CHANNELS];

            // f_yData_ftr[i_sample] = ftr_a0 * np.abs(y_out_d2[i_sample]) + ftr_b1 * f_yData_ftr[i_sample - 1]
            for (int i = 0; i < N_CHANNELS; i++)
                ftr[i, 0] = ftr_a0 * Math.Abs(y[i, DATA_CHANNEL_SIZE - 1]) + (1 - ftr_a0) * ftr[i, DATA_CHANNEL_SIZE - 1];
            for (int i = N_CHANNELS; i < n_samples; i++)
                ftr[i % N_CHANNELS, i / N_CHANNELS] = ftr_a0 * Math.Abs(y[i % N_CHANNELS, i / N_CHANNELS]) + (1 - ftr_a0) * ftr[i % N_CHANNELS, (i - N_CHANNELS) / N_CHANNELS];

            /*
			for (int i = 0; i < n_samples; i++)
				ftr[i % N_CHANNELS, i / N_CHANNELS] *= y_scale;
			*/

            for (int j = 0; j < N_CHANNELS; j++)
            {
                if (blind_time[j] > 0)
                    blind_time[j] -= DATA_CHANNEL_SIZE;

                if (blind_time[j] <= 0)
                {
                    for (int i = 0; i < (n_samples / N_CHANNELS); i++)
                    {
                        if (ftr[j, i] > threashold)
                        {
                            ftr[j, i] = 100;
                            capsuleCount[j]++;
                            globCapsuleCount++;
                            blind_time[j] = Blind_time_const;
                            break;
                        }
                    }
                }
            }


            dst = ftr;
        }

        public void ProcessData(ref float[,] dst, ref int[] data, int n_samples)
        {
            const float y_scale = (float)(1 / 0.57);
            const float threashold = (float)10.0;
            float a0 = (float)0.01;
            float a1 = (float)0.03;
            float ftr_a0 = (float)0.03;

            // LOW PASS: ylp[i] = a * x[i] + b * ylp[i-1]
            for (int i = 0; i < N_CHANNELS; i++)
                u[i, 0] = a0 * (float)data[i] + (1 - a0) * u[i, DATA_CHANNEL_SIZE - 1];
            //u[i, 0] = shortPacketData[i];
            for (int i = N_CHANNELS; i < n_samples; i++)
                u[i % N_CHANNELS, i / N_CHANNELS] = a0 * (float)data[i] + (1 - a0) * u[i % N_CHANNELS, (i - N_CHANNELS) / N_CHANNELS];

            for (int i = 0; i < N_CHANNELS; i++)
                w[i, 0] = a1 * u[i, DATA_CHANNEL_SIZE - 1] + (1 - a1) * w[i, DATA_CHANNEL_SIZE - 1];
            for (int i = N_CHANNELS; i < n_samples; i++)
                w[i % N_CHANNELS, i / N_CHANNELS] = a1 * u[i % N_CHANNELS, i / N_CHANNELS] + (1 - a1) * w[i % N_CHANNELS, (i - N_CHANNELS) / N_CHANNELS];

            for (int i = 0; i < n_samples; i++)
                y[i % N_CHANNELS, i / N_CHANNELS] = u[i % N_CHANNELS, i / N_CHANNELS] - w[i % N_CHANNELS, i / N_CHANNELS];

            // f_yData_ftr[i_sample] = ftr_a0 * np.abs(y_out_d2[i_sample]) + ftr_b1 * f_yData_ftr[i_sample - 1]
            for (int i = 0; i < N_CHANNELS; i++)
                ftr[i, 0] = ftr_a0 * Math.Abs(y[i, DATA_CHANNEL_SIZE - 1]) + (1 - ftr_a0) * ftr[i, DATA_CHANNEL_SIZE - 1];
            for (int i = N_CHANNELS; i < n_samples; i++)
                ftr[i % N_CHANNELS, i / N_CHANNELS] = ftr_a0 * Math.Abs(y[i % N_CHANNELS, i / N_CHANNELS]) + (1 - ftr_a0) * ftr[i % N_CHANNELS, (i - N_CHANNELS) / N_CHANNELS];

            /*
			for (int i = 0; i < n_samples; i++)
				ftr[i % N_CHANNELS, i / N_CHANNELS] *= y_scale;
			*/

            for (int j = 0; j < N_CHANNELS; j++)
            {
                if (blind_time[j] > 0)
                    blind_time[j] -= DATA_CHANNEL_SIZE;

                if (blind_time[j] <= 0)
                {
                    for (int i = 0; i < (n_samples / N_CHANNELS); i++)
                    {
                        if (ftr[j, i] > threashold)
                        {
                            ftr[j, i] = 100;
                            capsuleCount[j]++;
                            globCapsuleCount++;
                            blind_time[j] = Blind_time_const;
                            break;
                        }
                    }
                }
            }


            dst = u;
        }

        int pairListIdx = 0;
        private void FillPairList(ref float[,] data, int n_samples)
        {
            if (single_capture && single_capture_cntr > 0)
            {
                for (int i = 0; i < N_CHANNELS; i++)
                    ShiftLeft(adcPairList[i], DATA_CHANNEL_SIZE);

                for (int i = 0; i < n_samples; i++)
                {
                    float tmp_data = data[i % N_CHANNELS, i / N_CHANNELS];
                    if (tmp_data >= threashold)
                    {
                        single_capture_caught = true;
                    }
                    adcPairList[i % N_CHANNELS][(PAIR_LIST_MULTIPLES - 1) * DATA_CHANNEL_SIZE + i / N_CHANNELS].Y = tmp_data;

                    if (recordData)
                        textFile[i % N_CHANNELS].Append(intPacketData[i].ToString() + ",");
                }

                if (single_capture_caught)
                    single_capture_cntr -= n_samples / N_CHANNELS;

                pairListIdx++;
                if (pairListIdx >= PAIR_LIST_MULTIPLES)
                {
                    pairListIdx = 0;

                    if (recordData)
                    {
                        for (int i = 0; i < N_CHANNELS; i++)
                            adcHistory[i].Add(new PointPairList(adcPairList[i]));

                        adcHistorySize++;
                    }
                }
            }
            else if (!single_capture)
            {
                for (int i = 0; i < N_CHANNELS; i++)
                    ShiftLeft(adcPairList[i], DATA_CHANNEL_SIZE);

                for (int i = 0; i < n_samples; i++)
                {
                    adcPairList[i % N_CHANNELS][(PAIR_LIST_MULTIPLES - 1) * DATA_CHANNEL_SIZE + i / N_CHANNELS].Y = data[i % N_CHANNELS, i / N_CHANNELS];

                    if (recordData)
                        textFile[i % N_CHANNELS].Append(intPacketData[i].ToString() + ",");
                }

                // Counter for David
                FindDerivativeTriggers(n_samples);

                pairListIdx++;
                if (pairListIdx >= PAIR_LIST_MULTIPLES)
                {
                    pairListIdx = 0;

                    if (recordData)
                    {
                        for (int i = 0; i < N_CHANNELS; i++)
                            adcHistory[i].Add(new PointPairList(adcPairList[i]));

                        adcHistorySize++;
                    }
                }
            }

        }

        private void ResetProgramState()
        {
            pairListIdx = 0;
            adcHistoryIdx = 0;
            adcHistorySize = 0;
            for (int i = 0; i < N_CHANNELS; i++)
            {
                blind_time[i] = Blind_time_const;
                capsuleCount[i] = 0;
                capsulePresent[i] = false;
                adcHistory[i].Clear();
                textFile[i].Clear();
            }
        }

        private void Run_button_Click(object sender, EventArgs e)
        {
            ResetProgramState();

            try
            {
                serialPort.PortName = textBox1.Text;
                serialPort.Open();
                if (serialPort.IsOpen && timer1.Enabled == false)
                {
                    //serialPort.Write("go");
                    Run_button.BackColor = Color.Green;
                    button_auto_capture.BackColor = Color.Yellow;
                    Thread comThread = new Thread(HandleUSBTraffic);
                    comThread.Start();
                    threadActive = true;

                    timer1.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Can't open COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            long start = Stopwatch.GetTimestamp();

            zedGraphControl1.Refresh();

            stopwatch_graphplot_us = (long)((double)(Stopwatch.GetTimestamp() - start) * (double)(1e6 / (double)Stopwatch.Frequency));

            label1.Text = bytesToRead.ToString();
            label2.Text = stopwatch_graphplot_us.ToString();
            label3.Text = adcHistorySize.ToString();
            label5.Text = stopwatch_usb_timer_total_us.ToString();
            label6.Text = globCapsuleCount.ToString();

            label_counter.Text = david_counter.ToString();
        }

        private void Stop_button_Click(object sender, EventArgs e)
        {
            threadActive = false;
            Thread.Sleep(10);
            timer1.Enabled = false;
            if (serialPort.IsOpen)
            {
                serialPort.DiscardInBuffer();
                serialPort.Close();
            }
            Run_button.BackColor = DefaultBackColor;
        }

        private void Prev_button_Click(object sender, EventArgs e)
        {
            if (adcHistoryIdx > 0 && recordData)
            {
                adcHistoryIdx--;

                for (int i = 0; i < N_CHANNELS; i++)
                    adcPairList[i] = adcHistory[i][adcHistoryIdx];

                myPane.CurveList.Clear();

                for (int i = 0; i < N_CHANNELS; i++)
                    myPane.AddCurve("adc" + (i + 1).ToString(), adcPairList[i], color_array[i], SymbolType.None);


                zedGraphControl1.Refresh();
                label3.Text = adcHistoryIdx.ToString();
            }
        }

        private void Next_button_Click(object sender, EventArgs e)
        {
            if (adcHistoryIdx < adcHistory[0].Count - 1 && recordData)
            {
                adcHistoryIdx++;

                for (int i = 0; i < N_CHANNELS; i++)
                    adcPairList[i] = adcHistory[i][adcHistoryIdx];

                myPane.CurveList.Clear();

                for (int i = 0; i < N_CHANNELS; i++)
                    myPane.AddCurve("adc" + (i + 1).ToString(), adcPairList[i], color_array[i], SymbolType.None);


                zedGraphControl1.Refresh();
                label3.Text = adcHistoryIdx.ToString();
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (recordData)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.Title = "Save data";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        string textToWrite = "";
                        foreach (StringBuilder sb in textFile)
                        {
                            string s = sb.ToString();
                            textToWrite += s.Remove(s.Length - 1) + "\n";
                        }

                        switch (saveFileDialog1.FilterIndex)
                        {
                            case 1:
                                System.IO.File.WriteAllText(saveFileDialog1.FileName, textToWrite);
                                break;
                            default:
                                System.IO.File.WriteAllText(saveFileDialog1.FileName, textToWrite);
                                break;
                        }
                    }
                }
            }

        }

        private void Record_button_Click(object sender, EventArgs e)
        {
            if (!recordData)
            {
                recordData = true;
                Record_button.BackColor = Color.Red;
            }
            else
            {
                recordData = false;
                Record_button.BackColor = DefaultBackColor;
            }
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                int value = 0;
                if (int.TryParse(textBox_samplef.Text, out value))
                {
                    if (value > 0)
                    {
                        serialPort.Write("CSETF," + textBox_samplef.Text);
                        MessageBox.Show("Sample frequency set to " + textBox_samplef.Text + " Hz", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error: sample frequency must be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error: sample frequency NaN", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button_send_params_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("CPARAMS," + textBox_param.Text);
                char[] delimiterChars = { ',' };
                string[] floats = textBox_param.Text.Split(delimiterChars);
                threashold = float.Parse(floats[3].Replace('.', ','));
            }
        }

        private void button_send_times_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                // Save Blind time as the new gui Blind_time_const
                string st = textBox_times.Text;
                st = System.Text.RegularExpressions.Regex.Replace(st, " ", "");
                string[] ans = st.Split(',');
                Blind_time_const = Convert.ToInt32(ans[2]);    // blind time

                serialPort.Write("CTIMES," + textBox_times.Text);
            }
        }

        private void button_single_capture_Click(object sender, EventArgs e)
        {
            single_capture = true;
            single_capture_caught = false;
            single_capture_cntr = PAIR_LIST_DATA_SIZE / 10 * 7;

            button_single_capture.BackColor = Color.Yellow;
            button_auto_capture.BackColor = DefaultBackColor;
        }

        private void button_auto_capture_Click(object sender, EventArgs e)
        {
            single_capture = false;
            single_capture_caught = false;

            button_auto_capture.BackColor = Color.Yellow;
            button_single_capture.BackColor = DefaultBackColor;
        }

        private void checkBox_skip_2nd_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                if (checkBox_skip_2nd.Checked == true)
                {
                    serialPort.Write("CSKIPSCND,1");
                }
                else
                {
                    serialPort.Write("CSKIPSCND,0");
                }
            }
        }

        private void button_train_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("CTRAIN");
            }
        }

        private void radioButton_raw_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("CRAW");
            }
        }

        private void radioButton_trained_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("CTRAINED");
            }
        }
        private void radioButton_filtered_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("CFILTERED");
            }
        }

        // David funkcionalnost od tukaj naprej
        int david_counter = 0;
        double blind_level;

        private void button_reset_Click(object sender, EventArgs e)
        {
            david_counter = 0;
            label_counter.Text = "0";
        }

        private void button_set_edges_Click(object sender, EventArgs e)
        {
            blind_level = Convert.ToDouble(textBox_blind_level.Text);            
        }

        const double Min_Derivative = 10.0;
        double[] prev_value = new double[N_CHANNELS];
        double[] prev_derivative = new double[N_CHANNELS];

        public void FindDerivativeTriggers(int n_samples)
        {
            for (int ch = 0; ch < N_CHANNELS; ++ch)
            {
                for (int i = 0; i < (n_samples / N_CHANNELS); i += 10)
                {
                    if (adcPairList[ch][i].Y >= blind_level)
                    {
                        double derivative = adcPairList[ch][i].Y - prev_value[ch];
                        if (Math.Abs(derivative) >= Min_Derivative)
                        {
                            if (derivative < 0 && prev_derivative[ch] > 0)
                            {
                                david_counter++;
                            }

                            prev_value[ch] = adcPairList[ch][i].Y;
                            prev_derivative[ch] = derivative;
                        }                        
                    }                    
                }
            }
        }
    }

}