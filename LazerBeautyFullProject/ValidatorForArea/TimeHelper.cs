
using System;

public class TimeHelper
{
  
        private readonly TimeZoneInfo azerbaijanTimeZone;

        public TimeHelper()
        {
           
            azerbaijanTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
        }

        public DateTime GetAzerbaijanTime()
        {
            
            DateTime utcNow = DateTime.UtcNow;
            
            DateTime azerbaijanTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, azerbaijanTimeZone);
            return azerbaijanTime;
        }

        public DateTime ConvertToAzerbaijanTime(DateTime dateTime)
        {
           
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, azerbaijanTimeZone);
        }
    
}