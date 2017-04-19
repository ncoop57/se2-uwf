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

namespace Implementations
{
	public class CreateCalendarAction /*:IAction*/
	{
		string summary;
		string location;
		DateTime start;
		DateTime end;
		string emailAddress;

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

			CalendarStringMatcher matcher = new CalendarStringMatcher(months);
			string date = matcher.process(args);
			this.start = DateTime.Parse(date);
			this.summary = matcher.KeyWord;
		}

		/*public void run()
		{
			createCalendarEvent(this.summary, this.start);
		}*/
	}
}