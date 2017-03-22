using System;
using Google.Apis.CloudSpeechAPI.v1beta1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;
using Google.Apis.CloudSpeechAPI.v1beta1.Data;
using System.Collections.Generic;
using System.Linq;

namespace DoWhatImplementation
{
    public class DoWhat
    {
        //instance variables coded agl11 and nac33
        private String audioFileLocation;   //(.flac)
        private String STTString;
        private String verb;
        private String subject;
        private String target;
        private List<string> commands = "open play search".Split(' ').ToList();
        private List<string> skipWords;
        
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
        //default constructor builds with null coded agl11 and nac33
        public DoWhat()
        {
            //audioFile = file from record;
            this.STTString = "";
            this.verb = "";
            this.subject = "";
            this.target = "";
            string line;
            skipWords = new List<string>();
            // Read the file and display it line by line coded nac33
            System.IO.StreamReader file =
               new System.IO.StreamReader("../../../stopwords.txt");
            while ((line = file.ReadLine()) != null)
            {
                skipWords.Add(line);
            }
            file.Close();
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
            //authentication coded agl11
            CloudSpeechAPIService service = CreateAuthorizedClient();
            //construction of request coded agl11
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
        //NLP coded nac33
        public void ProcessViaNLP(string input)
        {
            input = input.ToLower();
            foreach (string word in commands)
            {
                if (input.Contains(word))
                {
                    input = string.Join(" ", input.Split(' ').Except(commands).Except(skipWords));
                    this.setVerb(word);
                    this.setSubject(input);
                    this.execute(word, input);
                }
            }
        }
        //coded nac33
        public void execute(string command, string input)
        {
            if (command.Equals("open"))
            {
                Console.WriteLine("Opening " + input);
            }
            else if (command.Equals("play"))
            {
                Console.WriteLine("Playing " + input);
            }
            else if (command.Equals("search"))
            {
                Console.WriteLine("Searching " + input);
            }
        }
    }
}





