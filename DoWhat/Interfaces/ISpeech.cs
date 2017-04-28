using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    // coded by Nathan
    public interface ISpeech
    {

        // Required for android speech api
        int VOICE { get; }

        /**
         * Sets up the intent for speech recognition
         * @param intent a base intent to be set up for speech recognition
         * @return the set up speech recognition intent
         */
        Intent setUpIntent(Intent intent);

        // Currently not being used
        void startRecording();

    }
}
