using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;
using System.Net.Sockets;

namespace Soundboard {
	public partial class Main : Form {

		private enum ConnectionStatus {
			CONNECTED,
			DISCONNECED
		}
		string filePath = "";
		string fileName = "";
		string folderPath = "";
		private AudioPlayer ap;
		private AudioPlayer ap_secondary;
		bool isPlaying = false;
		private Socket ClientSocket = new Socket
			(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		private BackgroundWorker listeningBGWorker;
		public Main() {
			InitializeComponent();
			ap = new AudioPlayer();
			ap_secondary = new AudioPlayer();
			LoadDevicesToComboBox();
			ap.Volume = trackBarVolume.Value / 100f;
			ap_secondary.Volume = trackBarVolume.Value / 100f;
			labelVolume.Text = $"{trackBarVolume.Value}%";
			buttonPlayPause.Enabled = false;
			//textBoxIPAddress.Text = GetIPAddress();
			listeningBGWorker = new BackgroundWorker();
			listeningBGWorker.WorkerSupportsCancellation = true;
			listeningBGWorker.WorkerReportsProgress = true;
			listeningBGWorker.DoWork += backgroundWorker1_DoWork;
			listeningBGWorker.ProgressChanged += backgroundWorker1_ProgressChanged;
		}

		private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
			RequestLoop();
		}

		private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) {
			ChangeConnectionStatus(ConnectionStatus.DISCONNECED);
		}

		private void StopThread() {
			listeningBGWorker.CancelAsync();
			listeningBGWorker.DoWork += backgroundWorker1_DoWork;
		}

		static string GetIPAddress() {
			String address = "";
			WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
			using (WebResponse response = request.GetResponse())
			using (StreamReader stream = new StreamReader(response.GetResponseStream())) {
				address = stream.ReadToEnd();
			}
			int first = address.IndexOf("Address: ") + 9;
			int last = address.LastIndexOf("</body>");
			address = address.Substring(first, last - first);
			return address;
		}

		private void LoadDevicesToComboBox() {
			comboBoxDeviceList.Items.Clear();
			comboBoxDeviceListSecondary.Items.Clear();
			for (int i = 0; i < WaveOut.DeviceCount; i++) {
				var caps = WaveOut.GetCapabilities(i);
				comboBoxDeviceList.Items.Add(caps.ProductName);
				comboBoxDeviceListSecondary.Items.Add(caps.ProductName);
			}
			if (comboBoxDeviceList.Items.Count > 0) comboBoxDeviceList.SelectedIndex = 0;
			if (comboBoxDeviceListSecondary.Items.Count > 0) comboBoxDeviceListSecondary.SelectedIndex = 0;
		}

		private void buttonPlayPause_Click(object sender, EventArgs e) {
			if (isPlaying) {
				if (checkBoxEnableSecondaryOutput.Checked) ap_secondary.Pause();
				ap.Pause();
				isPlaying = false;
				buttonPlayPause.Text = "Play";
			} else {
				if (checkBoxEnableSecondaryOutput.Checked) ap_secondary.Play();
				ap.Play();
				isPlaying = true;
				buttonPlayPause.Text = "Pause";
			}
			comboBoxDeviceList.Enabled = false;
			if (checkBoxEnableSecondaryOutput.Checked) comboBoxDeviceListSecondary.Enabled = false;
		}

		private void buttonStop_Click(object sender, EventArgs e) {
			if (checkBoxEnableSecondaryOutput.Checked) ap_secondary.Stop();
			ap.Stop();
			isPlaying = false;
			buttonPlayPause.Text = "Play";
			comboBoxDeviceList.Enabled = true;
			if (checkBoxEnableSecondaryOutput.Checked) comboBoxDeviceListSecondary.Enabled = true;
		}

		private void trackBarVolume_ValueChanged(object sender, EventArgs e) {
			ap.Volume = trackBarVolume.Value / 100f;
			ap_secondary.Volume = trackBarVolume.Value / 100f;
			labelVolume.Text = $"{trackBarVolume.Value}%";
		}

		private void comboBoxDeviceList_SelectedIndexChanged(object sender, EventArgs e) {
			ap.OutputDeviceNumber = comboBoxDeviceList.SelectedIndex;
		}

		private void comboBoxDeviceListSecondary_SelectedIndexChanged(object sender, EventArgs e) {
			ap_secondary.OutputDeviceNumber = comboBoxDeviceListSecondary.SelectedIndex;
		}

		private void buttonSelectFile_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Sound Files|*.mp3";
			if (ofd.ShowDialog() == DialogResult.OK) {
				buttonStop_Click(new object(), new EventArgs());
				filePath = ofd.FileName;
				int lastIndex = filePath.LastIndexOf("\\");
				fileName = filePath.Substring(lastIndex + 1, (filePath.Length - lastIndex - 1));
				ap.SetAudio(filePath);
				if (checkBoxEnableSecondaryOutput.Checked) ap_secondary.SetAudio(filePath);
				labelAudioFileName.Text = fileName;
				buttonPlayPause.Enabled = true;
			}
		}

		private void ConnectToServer() {
			if (ClientSocket.Connected) {
				StopThread();
				SendString("exit");
				CloseConnectionToServer();
			} else {
				if (textBoxUsername.Text == string.Empty) {
					MessageBox.Show("Username can't be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (textBoxIPAddress.Text == string.Empty) {
					MessageBox.Show("IP Address can't be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (folderPath == string.Empty) {
					MessageBox.Show("A folder must be selected first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (!ValidateIP(textBoxIPAddress.Text)) {
					MessageBox.Show("IP Address invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				try {
					//ClientSocket.Connect(IPAddress.Loopback, Convert.ToInt32(numericUpDownPortNumber.Value));
					ClientSocket.Connect(textBoxIPAddress.Text, Convert.ToInt32(numericUpDownPortNumber.Value));
					if (!Authenticate(textBoxUsername.Text)) {
						MessageBox.Show($"Failed to connect with username {textBoxUsername.Text}! Already exists?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						CloseConnectionToServer();
						return;
					}
				} catch (SocketException) {
					MessageBox.Show($"Failed to connect at {textBoxIPAddress.Text}:{numericUpDownPortNumber.Value}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				ChangeConnectionStatus(ConnectionStatus.CONNECTED);
				if (!listeningBGWorker.IsBusy)
					listeningBGWorker.RunWorkerAsync();
				MessageBox.Show($"Authentication successful! Welcome {textBoxUsername.Text}!", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

			}
		}

		private bool ValidateIP(string address) {
			string[] subaddress = address.Split('.');
			if (subaddress.Length != 4) return false;
			int[] subaddressNumbers = new int[] { -1, -1, -1, -1 };
			for (int i = 0; i < 4; i++) {
				int.TryParse(subaddress[i], out subaddressNumbers[i]);
				if (subaddressNumbers[i] == -1 || subaddressNumbers[i] > 255 || subaddressNumbers[i] < 0) return false;
			}
			return true;
		}

		private bool Authenticate(string username) {
			SendString($"AUTH REQ USERNAME:{username}");
			string response = ReceiveResponse();
			return response == "AUTH RESP USERNAME OK";
		}

		private void RequestLoop() {
			while (true) {
				string response = ReceiveResponse();
				if (response == string.Empty) continue;
				if (response == "SERVER STOP" || response == "CONNECTIONERROR") {
					CloseConnectionToServer(true);
					listeningBGWorker.ReportProgress(100);
					break;
				} else if (response.Contains("PLAY AUDIO")) {
					string filename = response.Split(':')[1];
					ap.Stop();
					ap.SetAudio($"{folderPath}\\{filename}.mp3");
					ap.Play();
					if (checkBoxEnableSecondaryOutput.Checked) {
						ap_secondary.Stop();
						ap_secondary.SetAudio($"{folderPath}\\{filename}.mp3");
						ap_secondary.Play();
					}
				} else if (response == "STOP PLAYING") {
					ap.Stop();
					if (checkBoxEnableSecondaryOutput.Checked) ap_secondary.Stop();
				}
			}
		}

		private void CloseConnectionToServer() {
			CloseConnectionToServer(true);
			ChangeConnectionStatus(ConnectionStatus.DISCONNECED);
		}

		private void CloseConnectionToServer(bool noButton) {
			ClientSocket.Shutdown(SocketShutdown.Both);
			ClientSocket.Close();
			ClientSocket = new Socket
			(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		private void ChangeConnectionStatus(ConnectionStatus status) {
			if (status == ConnectionStatus.CONNECTED) {
				buttonConnect.Text = "Disconnect";
				panelConnectionStatus.BackColor = Color.ForestGreen;
				//
			} else {
				buttonConnect.Text = "Connect";
				panelConnectionStatus.BackColor = Color.Firebrick;
			}

		}

		private void SendString(string text) {
			if (!ClientSocket.Connected) return;
			byte[] buffer = Encoding.ASCII.GetBytes(text);
			ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
		}

		private string ReceiveResponse() {
			try {
				if (!ClientSocket.Connected) return "CONNECTIONERROR";
				var buffer = new byte[2048];
				int received = ClientSocket.Receive(buffer, SocketFlags.None);
				if (received == 0) return string.Empty;
				var data = new byte[received];
				Array.Copy(buffer, data, received);
				string text = Encoding.ASCII.GetString(data);
				return text;
			} catch (Exception) {
				return "CONNECTIONERROR";
			}
		}

		private void buttonConnect_Click(object sender, EventArgs e) {
			ConnectToServer();
		}

		private void buttonSelectFolder_Click(object sender, EventArgs e) {
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			if (fbd.ShowDialog() == DialogResult.OK) {
				if (Directory.GetFiles(fbd.SelectedPath).Length == 0) return;
				folderPath = fbd.SelectedPath;
				labelFolderSelected.Text = "Folder selected";
				labelFolderSelected.ForeColor = Color.ForestGreen;
			}
		}

		private void buttonReloadDevices_Click(object sender, EventArgs e) {
			LoadDevicesToComboBox();
		}
	}
}
