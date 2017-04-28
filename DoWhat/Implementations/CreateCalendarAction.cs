using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Interfaces;
using Google.Apis.Calendar.v3.Data;


namespace Implementations
{
    //coded agl1 and nac33
    public class CreateCalendarAction : IAction
    {
        string summary;
        DateTime start;
        Context context;
        public void CreateCalendarEvent(string summary, DateTime start)
        {
            //string clientId = "924106574067-il14a6fmiqv515i955osn2tu7ij700o8.apps.googleusercontent.com";   //From Google Developer console https://console.developers.google.com 
            string userName = "agl11";                                                                     // A string used to identify a user. 
            string[] scopes = new string[]
            {
                CalendarService.Scope.Calendar,                                                             // Manage your calendars 
            }; 
            // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData% 
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
               credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
               GoogleClientSecrets.Load(stream).Secrets,
               scopes,
               userName, CancellationToken.None, new FileDataStore("DoWhat.GoogleCalendar.Auth.Store")).Result;
            }
            //below comment contains contents of the client_secrets.json file, in case you can hardcode it
            //{ "installed":{ "client_id":"924106574067-il14a6fmiqv515i955osn2tu7ij700o8.apps.googleusercontent.com","project_id":"dowhat-160203","auth_uri":"https://accounts.google.com/o/oauth2/auth","token_uri":"https://accounts.google.com/o/oauth2/token","auth_provider_x509_cert_url":"https://www.googleapis.com/oauth2/v1/certs","redirect_uris":["urn:ietf:wg:oauth:2.0:oob","http://localhost"]
 
            //create the service
            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DoWhat",
            });
            //define the event
            Event event1 = new Event()
            {
                Summary = summary,                                                          //need string from doWhat                              
                Start = new EventDateTime()
                {
                    DateTime = start,                                                       //need DateTime from doWhat
                    TimeZone = TimeZone.CurrentTimeZone.ToString()                          //default central
                },
                End = new EventDateTime()
                {
                    DateTime = start.AddHours(1.0),                                             //default 1.0 hours
                    TimeZone = TimeZone.CurrentTimeZone.ToString()                             //default central
                },
            };
            service.Events.Insert(event1, "primary").Execute();
        }
        public CreateCalendarAction(Context context)
        {
            this.context = context;
        }
        public void setArguments(string args)
        {
            string input = args;
            int first = input.IndexOf(" ") + 1;
            string date = input.Substring(first);
            Console.WriteLine("date:  " + date);
            string[] words = args.ToLower().Split(' ');
            string eventName = words[0];
            this.summary = eventName;
            Console.WriteLine("eventName:  " + eventName);

            IList<string> months = new List<string>();
            months.Add("january");
            months.Add("february");
            months.Add("march");
            months.Add("april");
            months.Add("may");
            months.Add("june");
            months.Add("july");
            months.Add("august");
            months.Add("september");
            months.Add("october");
            months.Add("november");
            months.Add("december");
            CalendarStringMatcher matcher = new CalendarStringMatcher(months);
            this.start = DateTime.Parse(date);
            

        }
        public void run()
        {
            CreateCalendarEvent(summary, start);
        }
    }
}