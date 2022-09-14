using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Soundboard {
	class AudioPlayer {
        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;
        private ISampleProvider sampleProvider;
        private float volume;
        private int outputDeviceNumber;
        private string filePath;
        private string fileName;

        public AudioPlayer() {
            volume = 0.5f;
            outputDeviceNumber = 0;
            filePath = "";
            fileName = "";
            
        }

        public float Volume {
            get { return Volume; }
            set {
                if (value > 1f || value < 0f) return;
                volume = value;
                if (waveOut != null) waveOut.Volume = volume;
            }
		}
        
        public int OutputDeviceNumber {
            get { return outputDeviceNumber; }
            set {
                outputDeviceNumber = value;
                if (filePath != "") SetAudio(filePath);
            }
		}

        public TimeSpan AudioLength {
            get { return audioFileReader != null ? audioFileReader.TotalTime : TimeSpan.Zero; }
		}

        public string FileName {
            get { return fileName; }
		}

        public void SetAudio(string filePath) {
            try {
                CreateWaveOut(outputDeviceNumber, 200, WaveCallbackStrategy.Event);//Increase or decrease latency if audio is choppy
                sampleProvider = CreateInputStream(filePath);
                waveOut.Init(sampleProvider);
            } catch (Exception) {
                throw;
            }
            this.filePath = filePath;
            int lastIndex = this.filePath.LastIndexOf('\\');
            fileName = filePath.Substring(lastIndex + 1, (this.filePath.Length - lastIndex - 1));
        }

        public void Play() {
            if (waveOut != null) {
                if (waveOut.PlaybackState == PlaybackState.Playing) return;
                else if (waveOut.PlaybackState == PlaybackState.Paused || waveOut.PlaybackState == PlaybackState.Stopped) {
                    waveOut.Volume = volume;
                    waveOut.Play();
                }
            }
        }

        public void Pause() {
            if (waveOut != null) {
                if (waveOut.PlaybackState == PlaybackState.Paused) return;
                else if (waveOut.PlaybackState == PlaybackState.Playing) waveOut.Pause();
            }
        }

        public void Stop() {
            if (waveOut != null) {
                waveOut.Stop();
                SetAudio(filePath);
                /*if (waveOut.PlaybackState == PlaybackState.Stopped) return;
                else {
                    waveOut.Stop();
                    SetAudio(filePath);
                }*/
            }
        }

        private ISampleProvider CreateInputStream(string fileName) {
            audioFileReader = new AudioFileReader(fileName);
            var sampleChannel = new SampleChannel(audioFileReader, true);
            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            return postVolumeMeter;
        }

        private void CreateWaveOut(int deviceID, int latency, WaveCallbackStrategy strategy) {
            CloseWaveOut();
            waveOut = CreateDevice(deviceID, latency, strategy);
        }

        private IWavePlayer CreateDevice(int deviceNumber, int latency, WaveCallbackStrategy strategy) {
            IWavePlayer device;
            if (strategy == WaveCallbackStrategy.Event) {
                var waveOut = new WaveOutEvent();
                waveOut.DeviceNumber = deviceNumber;
                waveOut.DesiredLatency = latency;
                device = waveOut;
            } else {
                WaveCallbackInfo callbackInfo = strategy == WaveCallbackStrategy.NewWindow ? WaveCallbackInfo.NewWindow() : WaveCallbackInfo.FunctionCallback();
                WaveOut outputDevice = new WaveOut(callbackInfo);
                outputDevice.DeviceNumber = deviceNumber;
                outputDevice.DesiredLatency = latency;
                device = outputDevice;
            }
            return device;
        }

        private void CloseWaveOut() {
            if (waveOut != null) waveOut.Stop();
            if (audioFileReader != null) {
                audioFileReader.Dispose();
                audioFileReader = null;
            }
            if (waveOut != null) {
                waveOut.Dispose();
                waveOut = null;
            }
        }
    }
}