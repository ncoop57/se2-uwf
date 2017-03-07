/* TESTED TO WORK - audio is being stored properly
*
* To find the file after recording, go to Settings on your device. Go to Internal Storage and then Explore. 
* Pathname: /Android/data/com.companyname.dowhatapp/files/audiotest.awb
*
*/
using System;
using System.Threading;
using System.Threading.Tasks;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Media;
// This class allows for the app to run. This will be cleaned up as I do more Android stuff
namespace DoWhatapp
{
	[Activity(Label = "DoWhatapp", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		Audio audio = new Audio();
		Button record = null;
		bool isRecording = false;
		String fileName = null;

		private void onRecord(bool start)
		{
			if (start)
			{
				audio.RecordAudioCompressed(fileName);
			}
			else
			{
				audio.Stop();
			}
		}

		/* runs the app */
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Record to our directory
			fileName = GetExternalFilesDir(null).AbsolutePath;
			fileName += "/audiotest.awb";


			//Record button
			record = FindViewById<Button>(Resource.Id.record);

			record.Enabled = true;
			record.Click += (object sender, EventArgs e) =>
			{
				isRecording = !isRecording;
				onRecord(isRecording);
			};

		}

	}
}

