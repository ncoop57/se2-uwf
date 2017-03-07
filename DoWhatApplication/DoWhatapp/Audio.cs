using System;
using System.IO;
using Android.Media;
// Set up RECORD_AUDIO permission in the AndroidManifest.xml before use.
namespace DoWhatapp
{
	public class Audio
	{
		protected MediaRecorder c_recorder;		// compressed audio recorder

		/* Try this if the RecordAudioLossless method doesn't work */
		public void RecordAudioCompressed(string filePath) 
		{
			try
			{
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
				if (c_recorder == null)
				{
					c_recorder = new MediaRecorder();     // Initial state
				}
				else
				{
					c_recorder.Reset();
				}
				c_recorder.SetAudioSource(AudioSource.Mic);
				c_recorder.SetOutputFormat(OutputFormat.AmrWb);
				c_recorder.SetAudioEncoder(AudioEncoder.AmrWb);
				// Initialized state.
				c_recorder.SetOutputFile(filePath);
				// DataSourceConfigured state.
				c_recorder.Prepare();     // Prepared state.
				c_recorder.Start();		// Recording state.
			}
			catch (Exception e)
			{
				Console.Out.WriteLine(e.StackTrace);
			}
		}

		/* Lossless audio recorder */
		public void RecordAudioLossless()
		{
			
		}

		public void Stop()
		{
			StopRecorder();
		}

		public void StopRecorder()
		{
			if (c_recorder != null)
			{
				c_recorder.Stop();
				c_recorder.Release();
				c_recorder = null;
			}
		}



	}
}
