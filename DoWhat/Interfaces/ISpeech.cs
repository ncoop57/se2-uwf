using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface ISpeech
    {
        int VOICE { get; }

        Intent setUpIntent(Intent intent);

        void startRecording();

    }
}
