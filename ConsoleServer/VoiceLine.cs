using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServer {
	class VoiceLine {
		private string speakerName;
		private string audioFile;
		private int duration;
		private int afterPause;
		public string SpeakerName { get => speakerName; set => speakerName = value; }
		public string AudioFile { get => audioFile; set => audioFile = value; }
		public int Duration { get => duration; set => duration = value; }
		public int AfterPause { get => afterPause; set => afterPause = value; }

		public VoiceLine() {

		}

		public VoiceLine(string speakerName, string audioFile, int duration, int afterPause) {
			this.SpeakerName = speakerName;
			this.AudioFile = audioFile;
			this.Duration = duration;
			this.AfterPause = afterPause;
		}
	}
}
