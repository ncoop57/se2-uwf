using System;
using Google.Apis.CloudSpeechAPI.v1beta1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;
using Google.Apis.CloudSpeechAPI.v1beta1.Data;
using System.Collections.Generic;
using opennlp.tools;
using opennlp.tools.postag;

namespace DoWhatImplementation
{
    public class DoWhat
    {
        //instance variables coded agl11
        private String audioFileLocation;   //(.flac)
        //private Stream audioStream; //.flac     //we will use whichever Julien can coax out of the UI
        private String STTString;
        private String verb;
        private String subject;
        private String target;
        //coded agl11
        public String getAudioFileLocation()
        {
            return this.audioFileLocation;
        }
        //coded agl11
        public void setAudioFileLocation(String inputAudioFileLocation)
        {
            this.audioFileLocation = inputAudioFileLocation;
        }
        //code in place in case we decide to use a stream instead
        /*public Stream getAudioStream()
        {
            return this.audioStream;
        }
        public void setAudioStream(Stream inputStream)
        {
            this.audioStream = inputStream;
        }*/
        public String getSTTString()
        {
            return this.STTString;
        }
        public void setSTTString(String inputString)
        {
            this.STTString = inputString;
        }
        public String getVerb()
        {
            return this.verb;
        }
        public void setVerb(String inputString)
        {
            this.verb = inputString;
        }
        public String getSubject()
        {
            return this.subject;
        }
        public void setSubject(String inputString)
        {
            this.subject = inputString;
        }
        public String getTarget()
        {
            return this.target;
        }
        public void setTarget(String inputString)
        {
            this.target = inputString;
        }
        //default constructor builds with null coded agl11
        public DoWhat()
        {
            //audioFile = file from record;
            this.STTString = "";
            this.verb = "";
            this.subject = "";
            this.target = "";
        }
        //coded agl11
        public void SendToSpeech()
        {

            IList<string> list = new List<string>();
            list.Add("Wink Hub");
            list.Add("Todist");

            SpeechContext context = new SpeechContext();
            context.Phrases = list;

            String inputFileString = this.getAudioFileLocation();
            String outputProcessedString = "";

            //authentication
            CloudSpeechAPIService service = CreateAuthorizedClient();
            //construction of request
            SyncRecognizeRequest request = new Google.Apis.CloudSpeechAPI.v1beta1.Data.SyncRecognizeRequest()
            {
                Config = new Google.Apis.CloudSpeechAPI.v1beta1.Data.RecognitionConfig()
                {
                    Encoding = "LINEAR16",
                    SampleRate = 16000,
                    LanguageCode = "en-US",
                    SpeechContext = context
                },
                Audio = new Google.Apis.CloudSpeechAPI.v1beta1.Data.RecognitionAudio()
                {
                    Content = Convert.ToBase64String(File.ReadAllBytes(inputFileString))
                }
            };
            //construct the send request
            var response = service.Speech.Syncrecognize(request).Execute();
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                    outputProcessedString = outputProcessedString + " " + (alternative.Transcript);
            }
            this.setSTTString(outputProcessedString);

        }
        //static method to create a client for authentication coded agl11
        static public CloudSpeechAPIService CreateAuthorizedClient()
        {
            GoogleCredential credential = GoogleCredential.GetApplicationDefaultAsync().Result;
            // Inject the Cloud Storage scope if required, per API tutorials
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    CloudSpeechAPIService.Scope.CloudPlatform
                });
            }
            return new CloudSpeechAPIService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DoWhat Application Audio",
            });
        }

        public void ProcessViaNLP(string sentence)
        {

            string modelPath = @"..\..\..\NLPModels\Models\";
            java.io.FileInputStream modelInpStream = new java.io.FileInputStream(modelPath + "en-pos-perceptron.bin");

            POSModel model = new POSModel(modelInpStream);
            POSTaggerME tagger = new POSTaggerME(model);

            string[] words = sentence.Split(' ');
            string[] tags = tagger.tag(words);

            for (int i = 0; i < words.Length; i++)
            {

                if (this.isNoun(tags[i]))
                    this.setSubject(words[i]);
                else if (this.isVerb(tags[i]))
                    this.setVerb(words[i]);

            }

        }

        public bool isNoun(string token)
        {

            if (token.Contains("NN"))
                return true;

            return false;

        }

        public bool isVerb(string token)
        {

            if (token.Contains("VB"))
                return true;

            return false;

        }

    }
}





