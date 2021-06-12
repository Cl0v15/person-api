using System;

namespace TPICAP.Persons.API.Core
{
    public class Request
    {
        public DateTime DateAndTime { get; }
        public string Caller { get; }
        public string Payload { get; }
        public string Url { get; }

        public Request(
            DateTime dateAndTime,
            string caller,
            string payload,
            string url)
        {
            DateAndTime = dateAndTime;
            Caller = caller;
            Payload = payload;
            Url = url;
        }
    }
}
