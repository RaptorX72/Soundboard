namespace Soundboard {
	partial class Main {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.buttonPlayPause = new System.Windows.Forms.Button();
			this.trackBarVolume = new System.Windows.Forms.TrackBar();
			this.buttonStop = new System.Windows.Forms.Button();
			this.comboBoxDeviceList = new System.Windows.Forms.ComboBox();
			this.buttonSelectFile = new System.Windows.Forms.Button();
			this.labelAudioFileName = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panelConnectionStatus = new System.Windows.Forms.Panel();
			this.labelFolderSelected = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxUsername = new System.Windows.Forms.TextBox();
			this.buttonSelectFolder = new System.Windows.Forms.Button();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownPortNumber = new System.Windows.Forms.NumericUpDown();
			this.textBoxIPAddress = new System.Windows.Forms.TextBox();
			this.buttonReloadDevices = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBoxDeviceListSecondary = new System.Windows.Forms.ComboBox();
			this.checkBoxEnableSecondaryOutput = new System.Windows.Forms.CheckBox();
			this.labelVolume = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortNumber)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonPlayPause
			// 
			this.buttonPlayPause.Location = new System.Drawing.Point(9, 19);
			this.buttonPlayPause.Name = "buttonPlayPause";
			this.buttonPlayPause.Size = new System.Drawing.Size(88, 34);
			this.buttonPlayPause.TabIndex = 0;
			this.buttonPlayPause.Text = "Play";
			this.buttonPlayPause.UseVisualStyleBackColor = true;
			this.buttonPlayPause.Click += new System.EventHandler(this.buttonPlayPause_Click);
			// 
			// trackBarVolume
			// 
			this.trackBarVolume.Location = new System.Drawing.Point(9, 147);
			this.trackBarVolume.Maximum = 100;
			this.trackBarVolume.Name = "trackBarVolume";
			this.trackBarVolume.Size = new System.Drawing.Size(275, 45);
			this.trackBarVolume.SmallChange = 5;
			this.trackBarVolume.TabIndex = 2;
			this.trackBarVolume.TickFrequency = 5;
			this.trackBarVolume.Value = 75;
			this.trackBarVolume.ValueChanged += new System.EventHandler(this.trackBarVolume_ValueChanged);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(103, 19);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(72, 34);
			this.buttonStop.TabIndex = 3;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// comboBoxDeviceList
			// 
			this.comboBoxDeviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDeviceList.FormattingEnabled = true;
			this.comboBoxDeviceList.Location = new System.Drawing.Point(9, 39);
			this.comboBoxDeviceList.Name = "comboBoxDeviceList";
			this.comboBoxDeviceList.Size = new System.Drawing.Size(306, 21);
			this.comboBoxDeviceList.TabIndex = 4;
			this.comboBoxDeviceList.SelectedIndexChanged += new System.EventHandler(this.comboBoxDeviceList_SelectedIndexChanged);
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.Location = new System.Drawing.Point(181, 19);
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.Size = new System.Drawing.Size(133, 34);
			this.buttonSelectFile.TabIndex = 5;
			this.buttonSelectFile.Text = "Open file";
			this.buttonSelectFile.UseVisualStyleBackColor = true;
			this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
			// 
			// labelAudioFileName
			// 
			this.labelAudioFileName.AutoSize = true;
			this.labelAudioFileName.Location = new System.Drawing.Point(6, 58);
			this.labelAudioFileName.Name = "labelAudioFileName";
			this.labelAudioFileName.Size = new System.Drawing.Size(109, 13);
			this.labelAudioFileName.TabIndex = 6;
			this.labelAudioFileName.Text = "No audio file selected";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.panelConnectionStatus);
			this.groupBox1.Controls.Add(this.labelFolderSelected);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textBoxUsername);
			this.groupBox1.Controls.Add(this.buttonSelectFolder);
			this.groupBox1.Controls.Add(this.buttonConnect);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.numericUpDownPortNumber);
			this.groupBox1.Controls.Add(this.textBoxIPAddress);
			this.groupBox1.Location = new System.Drawing.Point(7, 209);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 165);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Networking";
			// 
			// panelConnectionStatus
			// 
			this.panelConnectionStatus.BackColor = System.Drawing.Color.Firebrick;
			this.panelConnectionStatus.Location = new System.Drawing.Point(7, 126);
			this.panelConnectionStatus.Name = "panelConnectionStatus";
			this.panelConnectionStatus.Size = new System.Drawing.Size(306, 26);
			this.panelConnectionStatus.TabIndex = 18;
			// 
			// labelFolderSelected
			// 
			this.labelFolderSelected.AutoSize = true;
			this.labelFolderSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelFolderSelected.ForeColor = System.Drawing.Color.Red;
			this.labelFolderSelected.Location = new System.Drawing.Point(178, 72);
			this.labelFolderSelected.Name = "labelFolderSelected";
			this.labelFolderSelected.Size = new System.Drawing.Size(136, 16);
			this.labelFolderSelected.TabIndex = 17;
			this.labelFolderSelected.Text = "No folder selected";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Username";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "IP Address";
			// 
			// textBoxUsername
			// 
			this.textBoxUsername.Location = new System.Drawing.Point(62, 43);
			this.textBoxUsername.Name = "textBoxUsername";
			this.textBoxUsername.Size = new System.Drawing.Size(251, 20);
			this.textBoxUsername.TabIndex = 14;
			// 
			// buttonSelectFolder
			// 
			this.buttonSelectFolder.Location = new System.Drawing.Point(7, 69);
			this.buttonSelectFolder.Name = "buttonSelectFolder";
			this.buttonSelectFolder.Size = new System.Drawing.Size(165, 23);
			this.buttonSelectFolder.TabIndex = 13;
			this.buttonSelectFolder.Text = "Select Folder";
			this.buttonSelectFolder.UseVisualStyleBackColor = true;
			this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(7, 98);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(306, 23);
			this.buttonConnect.TabIndex = 11;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(226, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Port";
			// 
			// numericUpDownPortNumber
			// 
			this.numericUpDownPortNumber.Location = new System.Drawing.Point(254, 17);
			this.numericUpDownPortNumber.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.numericUpDownPortNumber.Name = "numericUpDownPortNumber";
			this.numericUpDownPortNumber.Size = new System.Drawing.Size(59, 20);
			this.numericUpDownPortNumber.TabIndex = 2;
			this.numericUpDownPortNumber.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// textBoxIPAddress
			// 
			this.textBoxIPAddress.Location = new System.Drawing.Point(62, 17);
			this.textBoxIPAddress.Name = "textBoxIPAddress";
			this.textBoxIPAddress.Size = new System.Drawing.Size(158, 20);
			this.textBoxIPAddress.TabIndex = 0;
			this.textBoxIPAddress.Text = "127.0.0.1";
			// 
			// buttonReloadDevices
			// 
			this.buttonReloadDevices.Location = new System.Drawing.Point(9, 118);
			this.buttonReloadDevices.Name = "buttonReloadDevices";
			this.buttonReloadDevices.Size = new System.Drawing.Size(305, 23);
			this.buttonReloadDevices.TabIndex = 15;
			this.buttonReloadDevices.Text = "Reload device list";
			this.buttonReloadDevices.UseVisualStyleBackColor = true;
			this.buttonReloadDevices.Click += new System.EventHandler(this.buttonReloadDevices_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 18);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Primary output (Voice chat)";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 65);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(91, 13);
			this.label5.TabIndex = 18;
			this.label5.Text = "Secondary output";
			// 
			// comboBoxDeviceListSecondary
			// 
			this.comboBoxDeviceListSecondary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDeviceListSecondary.FormattingEnabled = true;
			this.comboBoxDeviceListSecondary.Location = new System.Drawing.Point(9, 83);
			this.comboBoxDeviceListSecondary.Name = "comboBoxDeviceListSecondary";
			this.comboBoxDeviceListSecondary.Size = new System.Drawing.Size(241, 21);
			this.comboBoxDeviceListSecondary.TabIndex = 17;
			this.comboBoxDeviceListSecondary.SelectedIndexChanged += new System.EventHandler(this.comboBoxDeviceListSecondary_SelectedIndexChanged);
			// 
			// checkBoxEnableSecondaryOutput
			// 
			this.checkBoxEnableSecondaryOutput.AutoSize = true;
			this.checkBoxEnableSecondaryOutput.Checked = true;
			this.checkBoxEnableSecondaryOutput.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxEnableSecondaryOutput.Location = new System.Drawing.Point(256, 85);
			this.checkBoxEnableSecondaryOutput.Name = "checkBoxEnableSecondaryOutput";
			this.checkBoxEnableSecondaryOutput.Size = new System.Drawing.Size(59, 17);
			this.checkBoxEnableSecondaryOutput.TabIndex = 19;
			this.checkBoxEnableSecondaryOutput.Text = "Enable";
			this.checkBoxEnableSecondaryOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBoxEnableSecondaryOutput.UseVisualStyleBackColor = true;
			// 
			// labelVolume
			// 
			this.labelVolume.AutoSize = true;
			this.labelVolume.Location = new System.Drawing.Point(283, 154);
			this.labelVolume.Name = "labelVolume";
			this.labelVolume.Size = new System.Drawing.Size(33, 13);
			this.labelVolume.TabIndex = 20;
			this.labelVolume.Text = "100%";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.trackBarVolume);
			this.groupBox2.Controls.Add(this.labelVolume);
			this.groupBox2.Controls.Add(this.comboBoxDeviceList);
			this.groupBox2.Controls.Add(this.checkBoxEnableSecondaryOutput);
			this.groupBox2.Controls.Add(this.buttonReloadDevices);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.comboBoxDeviceListSecondary);
			this.groupBox2.Location = new System.Drawing.Point(7, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(320, 200);
			this.groupBox2.TabIndex = 21;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Settings";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.buttonPlayPause);
			this.groupBox3.Controls.Add(this.buttonStop);
			this.groupBox3.Controls.Add(this.buttonSelectFile);
			this.groupBox3.Controls.Add(this.labelAudioFileName);
			this.groupBox3.Location = new System.Drawing.Point(7, 380);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(320, 82);
			this.groupBox3.TabIndex = 22;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Play single file";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 471);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Soundboard";
			((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortNumber)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonPlayPause;
		private System.Windows.Forms.TrackBar trackBarVolume;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.ComboBox comboBoxDeviceList;
		private System.Windows.Forms.Button buttonSelectFile;
		private System.Windows.Forms.Label labelAudioFileName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxIPAddress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownPortNumber;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonSelectFolder;
		private System.Windows.Forms.TextBox textBoxUsername;
		private System.Windows.Forms.Button buttonReloadDevices;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelFolderSelected;
		private System.Windows.Forms.Panel panelConnectionStatus;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboBoxDeviceListSecondary;
		private System.Windows.Forms.CheckBox checkBoxEnableSecondaryOutput;
		private System.Windows.Forms.Label labelVolume;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
	}
}

