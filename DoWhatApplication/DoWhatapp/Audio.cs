using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using System.Threading.Tasks;
// Set up RECORD_AUDIO permission in the AndroidManifest.xml before use. - coded by julien
namespace DoWhatapp
{
	public class Audio
	{
		public Action<bool> RecordingStateChanged;
		protected MediaRecorder c_recorder;     // compressed audio recorder

		byte[] audioBuffer = null;
		AudioRecord audioRecord = null;
		bool endRecording = false;
		bool isRecording = false;
		private string filePath = "";

		/* Try this if the RecordAudioLossless method doesn't work */
		public void RecordAudioCompressed()
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
				c_recorder.Start();     // Recording state.
			}
			catch (Exception e)
			{
				Console.Out.WriteLine(e.StackTrace);
			}
		}

		/* Lossless audio recorder */
		public void setFilePath(String initfilePath)
		{
			this.filePath = initfilePath;
		}

		public void StopCom()
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

		public Boolean IsRecording
		{
			get { return (isRecording); }
		}

		async Task ReadAudioAsync()
		{
			using (var fileStream = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
			{
				while (true)
				{
					if (endRecording)
					{
						endRecording = false;
						break;
					}
					try
					{
						// Keep reading the buffer while there is audio input.
						int numBytes = await audioRecord.ReadAsync(audioBuffer, 0, audioBuffer.Length);
						await fileStream.WriteAsync(audioBuffer, 0, numBytes);
						// Do something with the audio input.
					}
					catch (Exception ex)
					{
						Console.Out.WriteLine(ex.Message);
						break;
					}
				}
				fileStream.Close();
			}
			audioRecord.Stop();
			audioRecord.Release();
			isRecording = false;

			RaiseRecordingStateChangedEvent();
		}

		private void RaiseRecordingStateChangedEvent()
		{
			if (RecordingStateChanged != null)
				RecordingStateChanged(isRecording);
		}

		protected async Task StartRecorderAsync()
		{
			endRecording = false;
			isRecording = true;

			RaiseRecordingStateChangedEvent();

			audioBuffer = new Byte[100000];
			audioRecord = new AudioRecord(
				// Hardware source of recording.
				AudioSource.Mic,
				// Frequency
				16000,
				// Mono or stereo
				ChannelIn.Mono,
				// Audio encoding
				Android.Media.Encoding.Pcm16bit,
				// Length of the audio clip.
				audioBuffer.Length
			);

			audioRecord.StartRecording();

			// Off line this so that we do not block the UI thread.
			await ReadAudioAsync();
		}

		public async Task StartAsync()
		{
			await StartRecorderAsync();
		}

		public void Stop()
		{
			endRecording = true;
			Thread.Sleep(500); // Give it time to drop out.
		}

	}
}
