namespace RealTimeGraph
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.Run_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Stop_button = new System.Windows.Forms.Button();
            this.Prev_button = new System.Windows.Forms.Button();
            this.Next_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Save_button = new System.Windows.Forms.Button();
            this.Record_button = new System.Windows.Forms.Button();
            this.textBox_samplef = new System.Windows.Forms.TextBox();
            this.button_send_freq = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_send_params = new System.Windows.Forms.Button();
            this.textBox_param = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_times = new System.Windows.Forms.TextBox();
            this.button_send_times = new System.Windows.Forms.Button();
            this.button_single_capture = new System.Windows.Forms.Button();
            this.button_auto_capture = new System.Windows.Forms.Button();
            this.checkBox_skip_2nd = new System.Windows.Forms.CheckBox();
            this.button_train = new System.Windows.Forms.Button();
            this.radioButton_raw = new System.Windows.Forms.RadioButton();
            this.radioButton_trained = new System.Windows.Forms.RadioButton();
            this.radioButton_filtered = new System.Windows.Forms.RadioButton();
            this.groupBox_display_data = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox_display_data.SuspendLayout();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(222, 12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(1356, 681);
            this.zedGraphControl1.TabIndex = 6;
            // 
            // Run_button
            // 
            this.Run_button.Location = new System.Drawing.Point(25, 56);
            this.Run_button.Name = "Run_button";
            this.Run_button.Size = new System.Drawing.Size(90, 30);
            this.Run_button.TabIndex = 1;
            this.Run_button.Text = "Run";
            this.Run_button.UseVisualStyleBackColor = true;
            this.Run_button.Click += new System.EventHandler(this.Run_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(25, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(90, 20);
            this.textBox1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "label3";
            // 
            // Stop_button
            // 
            this.Stop_button.Location = new System.Drawing.Point(25, 98);
            this.Stop_button.Name = "Stop_button";
            this.Stop_button.Size = new System.Drawing.Size(90, 30);
            this.Stop_button.TabIndex = 3;
            this.Stop_button.Text = "Stop";
            this.Stop_button.UseVisualStyleBackColor = true;
            this.Stop_button.Click += new System.EventHandler(this.Stop_button_Click);
            // 
            // Prev_button
            // 
            this.Prev_button.Location = new System.Drawing.Point(25, 140);
            this.Prev_button.Name = "Prev_button";
            this.Prev_button.Size = new System.Drawing.Size(35, 35);
            this.Prev_button.TabIndex = 4;
            this.Prev_button.Text = "<";
            this.Prev_button.UseVisualStyleBackColor = true;
            this.Prev_button.Click += new System.EventHandler(this.Prev_button_Click);
            // 
            // Next_button
            // 
            this.Next_button.Location = new System.Drawing.Point(80, 140);
            this.Next_button.Name = "Next_button";
            this.Next_button.Size = new System.Drawing.Size(35, 35);
            this.Next_button.TabIndex = 5;
            this.Next_button.Text = ">";
            this.Next_button.UseVisualStyleBackColor = true;
            this.Next_button.Click += new System.EventHandler(this.Next_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(126, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(90, 112);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "label5";
            // 
            // Save_button
            // 
            this.Save_button.Location = new System.Drawing.Point(25, 187);
            this.Save_button.Name = "Save_button";
            this.Save_button.Size = new System.Drawing.Size(90, 30);
            this.Save_button.TabIndex = 8;
            this.Save_button.Text = "Save";
            this.Save_button.UseVisualStyleBackColor = true;
            this.Save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // Record_button
            // 
            this.Record_button.Location = new System.Drawing.Point(135, 146);
            this.Record_button.Name = "Record_button";
            this.Record_button.Size = new System.Drawing.Size(75, 23);
            this.Record_button.TabIndex = 9;
            this.Record_button.Text = "Record";
            this.Record_button.UseVisualStyleBackColor = true;
            this.Record_button.Click += new System.EventHandler(this.Record_button_Click);
            // 
            // textBox_samplef
            // 
            this.textBox_samplef.Location = new System.Drawing.Point(25, 338);
            this.textBox_samplef.Name = "textBox_samplef";
            this.textBox_samplef.Size = new System.Drawing.Size(104, 20);
            this.textBox_samplef.TabIndex = 10;
            // 
            // button_send_freq
            // 
            this.button_send_freq.Location = new System.Drawing.Point(135, 335);
            this.button_send_freq.Name = "button_send_freq";
            this.button_send_freq.Size = new System.Drawing.Size(75, 23);
            this.button_send_freq.TabIndex = 11;
            this.button_send_freq.Text = "Send";
            this.button_send_freq.UseVisualStyleBackColor = true;
            this.button_send_freq.Click += new System.EventHandler(this.Send_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 322);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Sample freq:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 376);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Filter parameters (a1,a2,a3,thr):";
            // 
            // button_send_params
            // 
            this.button_send_params.Location = new System.Drawing.Point(135, 392);
            this.button_send_params.Name = "button_send_params";
            this.button_send_params.Size = new System.Drawing.Size(75, 23);
            this.button_send_params.TabIndex = 15;
            this.button_send_params.Text = "Send";
            this.button_send_params.UseVisualStyleBackColor = true;
            this.button_send_params.Click += new System.EventHandler(this.button_send_params_Click);
            // 
            // textBox_param
            // 
            this.textBox_param.Location = new System.Drawing.Point(25, 395);
            this.textBox_param.Name = "textBox_param";
            this.textBox_param.Size = new System.Drawing.Size(104, 20);
            this.textBox_param.TabIndex = 14;
            this.textBox_param.Text = "0.01,0.03,0.03,7.0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 432);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(159, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Time parameters (dly, dur, blind):";
            // 
            // textBox_times
            // 
            this.textBox_times.Location = new System.Drawing.Point(25, 448);
            this.textBox_times.Name = "textBox_times";
            this.textBox_times.Size = new System.Drawing.Size(104, 20);
            this.textBox_times.TabIndex = 18;
            this.textBox_times.Text = "0,100,1000";
            // 
            // button_send_times
            // 
            this.button_send_times.Location = new System.Drawing.Point(135, 446);
            this.button_send_times.Name = "button_send_times";
            this.button_send_times.Size = new System.Drawing.Size(75, 23);
            this.button_send_times.TabIndex = 19;
            this.button_send_times.Text = "Send";
            this.button_send_times.UseVisualStyleBackColor = true;
            this.button_send_times.Click += new System.EventHandler(this.button_send_times_Click);
            // 
            // button_single_capture
            // 
            this.button_single_capture.Location = new System.Drawing.Point(25, 524);
            this.button_single_capture.Name = "button_single_capture";
            this.button_single_capture.Size = new System.Drawing.Size(90, 23);
            this.button_single_capture.TabIndex = 20;
            this.button_single_capture.Text = "Single capture";
            this.button_single_capture.UseVisualStyleBackColor = true;
            this.button_single_capture.Click += new System.EventHandler(this.button_single_capture_Click);
            // 
            // button_auto_capture
            // 
            this.button_auto_capture.Location = new System.Drawing.Point(25, 495);
            this.button_auto_capture.Name = "button_auto_capture";
            this.button_auto_capture.Size = new System.Drawing.Size(90, 23);
            this.button_auto_capture.TabIndex = 21;
            this.button_auto_capture.Text = "Auto";
            this.button_auto_capture.UseVisualStyleBackColor = true;
            this.button_auto_capture.Click += new System.EventHandler(this.button_auto_capture_Click);
            // 
            // checkBox_skip_2nd
            // 
            this.checkBox_skip_2nd.AutoSize = true;
            this.checkBox_skip_2nd.Location = new System.Drawing.Point(135, 495);
            this.checkBox_skip_2nd.Name = "checkBox_skip_2nd";
            this.checkBox_skip_2nd.Size = new System.Drawing.Size(68, 17);
            this.checkBox_skip_2nd.TabIndex = 22;
            this.checkBox_skip_2nd.Text = "Skip 2nd";
            this.checkBox_skip_2nd.UseVisualStyleBackColor = true;
            this.checkBox_skip_2nd.CheckedChanged += new System.EventHandler(this.checkBox_skip_2nd_CheckedChanged);
            // 
            // button_train
            // 
            this.button_train.Location = new System.Drawing.Point(135, 262);
            this.button_train.Name = "button_train";
            this.button_train.Size = new System.Drawing.Size(75, 23);
            this.button_train.TabIndex = 23;
            this.button_train.Text = "Train";
            this.button_train.UseVisualStyleBackColor = true;
            this.button_train.Click += new System.EventHandler(this.button_train_Click);
            // 
            // radioButton_raw
            // 
            this.radioButton_raw.AutoSize = true;
            this.radioButton_raw.Checked = true;
            this.radioButton_raw.Location = new System.Drawing.Point(16, 19);
            this.radioButton_raw.Name = "radioButton_raw";
            this.radioButton_raw.Size = new System.Drawing.Size(47, 17);
            this.radioButton_raw.TabIndex = 24;
            this.radioButton_raw.TabStop = true;
            this.radioButton_raw.Text = "Raw";
            this.radioButton_raw.UseVisualStyleBackColor = true;
            this.radioButton_raw.CheckedChanged += new System.EventHandler(this.radioButton_raw_CheckedChanged);
            // 
            // radioButton_trained
            // 
            this.radioButton_trained.AutoSize = true;
            this.radioButton_trained.Location = new System.Drawing.Point(16, 42);
            this.radioButton_trained.Name = "radioButton_trained";
            this.radioButton_trained.Size = new System.Drawing.Size(61, 17);
            this.radioButton_trained.TabIndex = 25;
            this.radioButton_trained.Text = "Trained";
            this.radioButton_trained.UseVisualStyleBackColor = true;
            this.radioButton_trained.CheckedChanged += new System.EventHandler(this.radioButton_trained_CheckedChanged);
            // 
            // radioButton_filtered
            // 
            this.radioButton_filtered.AutoSize = true;
            this.radioButton_filtered.Location = new System.Drawing.Point(16, 65);
            this.radioButton_filtered.Name = "radioButton_filtered";
            this.radioButton_filtered.Size = new System.Drawing.Size(59, 17);
            this.radioButton_filtered.TabIndex = 26;
            this.radioButton_filtered.Text = "Filtered";
            this.radioButton_filtered.UseVisualStyleBackColor = true;
            this.radioButton_filtered.CheckedChanged += new System.EventHandler(this.radioButton_filtered_CheckedChanged);
            // 
            // groupBox_display_data
            // 
            this.groupBox_display_data.Controls.Add(this.radioButton_filtered);
            this.groupBox_display_data.Controls.Add(this.radioButton_trained);
            this.groupBox_display_data.Controls.Add(this.radioButton_raw);
            this.groupBox_display_data.Location = new System.Drawing.Point(25, 223);
            this.groupBox_display_data.Name = "groupBox_display_data";
            this.groupBox_display_data.Size = new System.Drawing.Size(104, 88);
            this.groupBox_display_data.TabIndex = 27;
            this.groupBox_display_data.TabStop = false;
            this.groupBox_display_data.Text = "Display data";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(142, 199);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Verbose";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1590, 705);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox_display_data);
            this.Controls.Add(this.button_train);
            this.Controls.Add(this.checkBox_skip_2nd);
            this.Controls.Add(this.button_auto_capture);
            this.Controls.Add(this.button_single_capture);
            this.Controls.Add(this.button_send_times);
            this.Controls.Add(this.textBox_times);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_send_params);
            this.Controls.Add(this.textBox_param);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_send_freq);
            this.Controls.Add(this.textBox_samplef);
            this.Controls.Add(this.Record_button);
            this.Controls.Add(this.Save_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Next_button);
            this.Controls.Add(this.Prev_button);
            this.Controls.Add(this.Stop_button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Run_button);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox_display_data.ResumeLayout(false);
            this.groupBox_display_data.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private ZedGraph.ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.Button Run_button;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button Stop_button;
		private System.Windows.Forms.Button Prev_button;
		private System.Windows.Forms.Button Next_button;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button Save_button;
		private System.Windows.Forms.Button Record_button;
		private System.Windows.Forms.TextBox textBox_samplef;
		private System.Windows.Forms.Button button_send_freq;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_send_params;
        private System.Windows.Forms.TextBox textBox_param;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_times;
        private System.Windows.Forms.Button button_send_times;
        private System.Windows.Forms.Button button_single_capture;
        private System.Windows.Forms.Button button_auto_capture;
        private System.Windows.Forms.CheckBox checkBox_skip_2nd;
        private System.Windows.Forms.Button button_train;
        private System.Windows.Forms.RadioButton radioButton_raw;
        private System.Windows.Forms.RadioButton radioButton_trained;
        private System.Windows.Forms.RadioButton radioButton_filtered;
        private System.Windows.Forms.GroupBox groupBox_display_data;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

