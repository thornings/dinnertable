using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMadbordet.Utils
{
    public static class DateTimeHelper
    {
        public static DateTime SetDateTimeOrDefault(int year, int month, int day)
        {
            DateTime date = DateTime.Now; 
            try
            {
                if (!(year == 0 || (month) == 0 || day == 0))
                {
                    date = new DateTime(year, month, day, 0, 0, 0);
                }
            }
            catch (Exception)
            {
                // nothing allready set to now    
            }

            return date;
        }

    }
}
