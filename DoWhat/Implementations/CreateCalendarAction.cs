using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Interfaces;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.Threading;
using Google.Apis.Util.Store;
using System.IO;

namespace Implementations
{
	public class CreateCalendarAction : IAction
	{
		string summary;
		string location;
		DateTime start;
		DateTime end;
		string emailAddress;
        Context context;

        //overloaded method with two parameters
        /*public void createCalendarEvent(string summary, DateTime start)
		{
			UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
				new ClientSecrets
				{
					ClientId = "CLIENTID",
					ClientSecret = "CLIENTSECRET",
				},
				new[] { CalendarService.Scope.Calendar },
				"user",
				CancellationToken.None).Result;

			// Create the service.
			var service = new CalendarService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = "DoWhat",
			});

			//define the event
			Event event1 = new Event()
			{
				Summary = summary,                                                          //need string from doWhat
																							//Location = location,                                
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
				//Attendees = new List<EventAttendee>()
				//{
				//new EventAttendee() { Email = emailAddress}                               //need email from doWhat or phone
				//}
			};
			//Event recurringEvent = service.Events.Insert(event1, "primary").Execute();
		}*/
        //overloaded method with all possible variables
        /*public void createCalendarEvent(string summary, string location, DateTime start, DateTime end, string emailAddress, string timeZone)
        {
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "CLIENTID",
                    ClientSecret = "CLIENTSECRET",
                },
                new[] { CalendarService.Scope.Calendar },
                "user",
                CancellationToken.None).Result;
            // Create the service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DoWhat",
            });
            //define the event
            Event event1 = new Event()
            {
                Summary = summary,                                  //need string from doWhat
                Location = location,                                //need string from doWhat
                Start = new EventDateTime()
                {
                    DateTime = start,                               //need DateTime from doWhat
                    TimeZone = timeZone                             //need TimeZone from doWhat or phone
                },
                End = new EventDateTime()
                {
                    DateTime = end,                                 //need DateTime from doWhat
                    TimeZone = timeZone                             //need TimeZone from doWhat or phone
                },
                Attendees = new List<EventAttendee>()                       
                {
                    new EventAttendee() { Email = emailAddress}     //need email from doWhat or phone
                }
            };
            Event recurringEvent = service.Events.Insert(event1, "primary").Execute();
        }*/


        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "DoWhat";

        public CreateCalendarAction(Context context)
        {

            this.context = context;

        }

        private void createEvent()
        {

            Console.WriteLine("Creating Event...");

            UserCredential credential;
            try
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = "924106574067-m766vb0884dlqgchq7ejajch02i13d5k.apps.googleusercontent.com",
                        ClientSecret = "L766a0iHpOi8s3WKOlDo0OKC"
                    },
                Scopes,
                "user",
                CancellationToken.None).Result;
                /*var auth = new OAuth2Authenticator(
                   clientId: "924106574067-tk5gm1lqphsn16tt567d8mama57s7pm8.apps.googleusercontent.com",
                   clientSecret: "MWOyUjpri3n3PHNg4CVrQzM4",
                   scope: "profile",
                   authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/auth"),
                   redirectUrl: new Uri("https://www.google.com/"),
                   accessTokenUrl: new Uri("https://accounts.google.com/o/oauth2/token"),
                   isUsingNativeUI: true);

                System.Uri uri_netfx = auth.GetInitialUrlAsync().Result;
                global::Android.Net.Uri uri_android = global::Android.Net.Uri.Parse(uri_netfx.AbsoluteUri);
                var chrome_tab = (global::Android.Support.CustomTabs.CustomTabsIntent.Builder)auth.GetUI(this);
                global::Android.Support.CustomTabs.CustomTabsIntent ct_intent = chrome_tab.Build();

                ct_intent.LaunchUrl(this, uri_android);
                */

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                // List events.
                Events events = request.Execute();
                Console.WriteLine("Upcoming events:");
                if (events.Items != null && events.Items.Count > 0)
                {
                    foreach (var eventItem in events.Items)
                    {
                        string when = eventItem.Start.DateTime.ToString();
                        if (String.IsNullOrEmpty(when))
                        {
                            when = eventItem.Start.Date;
                        }
                        Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                    }
                }
                else
                {
                    Console.WriteLine("No upcoming events found.");
                }
                }
            catch (AggregateException e)
            {

                Console.WriteLine(e.StackTrace);

            }
            Console.Read();

        }

		public void setArguments(string args)
		{
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

			/*CalendarStringMatcher matcher = new CalendarStringMatcher(months);
			string date = matcher.process(args);
			this.start = DateTime.Parse(date);
			this.summary = matcher.KeyWord;*/
		}

		public void run()
		{
			createEvent();
		}
	}
}