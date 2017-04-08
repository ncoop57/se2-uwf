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
    public class CalendarAction : ICalendarAction
    {
        public void createCalendarEvent(string summary, string location, DateTime start, DateTime end, string emailAddress, string timeZone)
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
        }
    }
}