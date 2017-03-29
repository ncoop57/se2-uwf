/* TESTED TO WORK - audio is being stored properly
*
* To find the file after recording, go to Settings on your device. Go to Internal Storage and then Explore. 
* Pathname: /Android/data/com.companyname.dowhatapp/files/audiotest.awb
*
*/
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Content;
using Android.Content.Res;
using DoWhatImplementation;

using Android.Runtime;
using Android.Views;
using Android.Media;
// This class allows for the app to run. This will be cleaned up as I do more Android stuff - coded by julien
namespace DoWhatapp
{
	[Activity(Label = "DoWhatapp", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		AssetManager assets;
		Audio audio = new Audio();
		Button record = null;
		bool isRecording = false;
        public string filePath;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            filePath = "test";
            assets = this.Assets;
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //GetInstalledApplications(/*PackageInfoFlags.Services*/);

            // Record to our directory
            try
            {

                filePath = Android.OS.Environment.DataDirectory.AbsolutePath;
               
                filePath += "/audiotest.wav";
                Console.WriteLine("The filePath:  " + filePath);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(filePath);
                Console.WriteLine("The error from filePath:  " + e.GetBaseException());
            }


            //Record button
            record = FindViewById<Button>(Resource.Id.record);
            DoWhat dowhatobject = new DoWhat(ReadStopWords());
            record.Enabled = true;
            record.Click += (object sender, EventArgs e) =>
            {
                isRecording = !isRecording;
                if (isRecording == false)
                {
                    audio.Stop();
                    audio.RecordingStateChanged += (recording) =>
                    {
                        audio.RecordingStateChanged = null;
                    };
                    dowhatobject.setAudioFileLocation(filePath);
                    Console.WriteLine("setAudioFileLocation called...");
					dowhatobject.SendToSpeech(ReadKey());
                    Console.WriteLine("SendToSpeech called...");
                    Console.WriteLine("what is happening: " + dowhatobject.getSTTString() + "here?");
                    dowhatobject.ProcessViaNLP(dowhatobject.getSTTString());
                    Console.WriteLine("ProcessViaNLP called...");
                    string inputVerb = dowhatobject.getVerb();
                    Console.WriteLine(dowhatobject.getVerb());
                    string inputSubject = dowhatobject.getSubject();
                    Console.WriteLine(dowhatobject.getSubject());
                    string openVerb = "open";
                    bool result = inputVerb.Equals(openVerb, StringComparison.Ordinal);
                    if (result)
                    {
                        openApplication(inputSubject);
                    }
                }
                else
                {
                    Record();
                }
            };
        }
        async Task Record()
		{
			//if (start)
			//{
			audio.setFilePath(filePath);
			await audio.StartAsync();
			//audio.RecordAudioCompressed(fileName);
			//	}
			//else
			//{
			//audio.StopCom();
			//	audio.Stop();
			//	}
		}

		// Creates list of installed app package names - coded by matthew
		//[Android.Runtime.Register("getInstalledApplications", "(I)Ljava/util/List;", "GetGetInstalledApplications_IHandler")]
		//public IList<string> GetInstalledApplications(/*[Android.Runtime.GeneratedEnum] PackageInfoFlags flags*/) 
		//{
		//	var appList = new List<string>();
		//	foreach (var item in PackageManager.GetInstalledApplications
		//	 (new PackageInfoFlags()))
		//	{
		//		var context = CreatePackageContext(
		//						  item.PackageName, PackageContextFlags.IgnoreSecurity);
        //
		//		appList.Add(context.PackageName);
		//	}
		//	Console.WriteLine(appList);
		//	return appList;
		//}
        public void openApplication(string appName)
        {

            Intent intent = PackageManager.GetLaunchIntentForPackage("com.package.address");
            StartActivity(intent);

        }

		public System.IO.Stream ReadKey() 
		{
			return assets.Open("DoWhat-65e8c7b1824e.json");
		}

        public System.IO.Stream ReadStopWords()
        {
            return assets.Open("stopwords.txt");
        }

        /* runs the app */
    }
}
