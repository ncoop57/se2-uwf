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
            string clientId = "924106574067-tk5gm1lqphsn16tt567d8mama57s7pm8.apps.googleusercontent.com";   //From Google Developer console https://console.developers.google.com 
            string clientSecret = "MWOyUjpri3n3PHNg4CVrQzM4";                                               //From Google Developer console https://console.developers.google.com 
            string userName = "agl11";                                                                     // A string used to identify a user. 
            string[] scopes = new string[]
            {
                CalendarService.Scope.Calendar,                                                             // Manage your calendars 
            }; // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData% 
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            }, scopes, userName, CancellationToken.None, new FileDataStore("Daimto.GoogleCalendar.Auth.Store")).Result;
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