using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ConsoleServer {
	class AudioQueue {
		private string queueName;
		private List<VoiceLine> voicelines = new List<VoiceLine>();
		public string QueueName { get => queueName; set => queueName = value; }
		internal List<VoiceLine> Voicelines { get => voicelines; set => voicelines = value; }
		public AudioQueue(string queueName, List<VoiceLine> voicelines) {
			this.QueueName = queueName;
			this.Voicelines = voicelines;
		}
	}

	static class QueueLoader {
		private static List<AudioQueue> audioQueues = new List<AudioQueue>();
		public static List<AudioQueue> LoadAudioQueuesFromXmlFile(string filepath) {
			audioQueues.Clear();
			XmlDocument doc = new XmlDocument();
			doc.Load(filepath);
			XmlElement root = doc.DocumentElement;
			foreach (XmlElement audioQueue in root.ChildNodes) {
				string name = "";
				List<VoiceLine> voiceLines = new List<VoiceLine>();
				foreach (XmlElement item in audioQueue.ChildNodes) {
					if (item.Name == "Name") name = item.InnerText;
					if (item.Name == "VoiceLines") {
						foreach (XmlElement voiceline in item) {
							VoiceLine vl = new VoiceLine();
							foreach (XmlElement lineItem in voiceline) {
								if (lineItem.Name == "SpeakerName") vl.SpeakerName = lineItem.InnerXml;
								else if (lineItem.Name == "AudioFile") vl.AudioFile = lineItem.InnerXml;
								else if (lineItem.Name == "Duration") vl.Duration = Convert.ToInt32(lineItem.InnerXml);
								else if (lineItem.Name == "AfterPause") vl.AfterPause = Convert.ToInt32(lineItem.InnerXml);
							}
							voiceLines.Add(vl);
						}
					}
				}
				audioQueues.Add(new AudioQueue(name, voiceLines));
			}
			return audioQueues;
		}
		public static AudioQueue LoadAudioQueue(string name) {
			foreach (AudioQueue queue in audioQueues) {
				if (queue.QueueName == name) return queue;
			}
			return null;
		}
	}
}
