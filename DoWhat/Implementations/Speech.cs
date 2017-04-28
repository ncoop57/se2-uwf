using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Speech;
using Interfaces;

namespace Implementations
{
    // coded by Nathan
    public class Speech : ISpeech
    {

        public int VOICE
        {
            get;
        }

        public Speech(int voice)
        {

            VOICE = voice;

        }

        /**
         * Sets up the intent for speech recognition
         * @param intent a base intent to be set up for speech recognition
         * @return the set up speech recognition intent
         */
        public Intent setUpIntent(Intent voiceIntent)
        {

            // create the intent and start the activity
            voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

            // put a message on the modal dialog
            //voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.messageSpeakNow));

            // if there is more then 1.5s of silence, consider the speech over
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

            // you can specify other languages recognised here, for example
            // voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.German);
            // if you wish it to recognise the default Locale language and German
            // if you do use another locale, regional dialects may not be recognised very well
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);

            return voiceIntent;

            //StartActivityForResult(voiceIntent, VOICE);

        }

        // Currently not being used
        public void startRecording()
        {
            throw new NotImplementedException();
        }
        
    }
}
