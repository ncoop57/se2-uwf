using System;
using Google.Apis.CloudSpeechAPI.v1beta1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;
using Google.Apis.CloudSpeechAPI.v1beta1.Data;
using System.Collections.Generic;

namespace DoWhatImplementation
{
    public class DoWhat
    {
        //instance variables
        private String audioFileLocation;   //(.flac)
        //private Stream audioStream; //.flac     //we will use whichever Julien can coax out of the UI
        private String STTString;
        private String NLPString;
        private String verb;
        private String subject;
        private String target;
        public String getAudioFileLocation()
        {
            return this.audioFileLocation;
        }
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
        public String getNLPString()
        {
            return this.NLPString;
        }
        public void setNLPString(String inputString)
        {
            this.NLPString = inputString;
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
        //default constructor builds with null
        public DoWhat()
        {
            //audioFile = file from record;
            this.STTString = "";
            this.NLPString = "";
            this.verb = "";
            this.subject = "";
            this.target = "";
        }
        public void SendToSpeech()
        {
            IList<string> list = new List<string>();    //refactor
            list.Add("Wink Hub");                       //refactor
            list.Add("Todoist");                        //refactor
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
        //static method to create a client for authentication
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
    }
}





