using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Question2.Models;

namespace Main_Project
{
    internal class Input
    {
        private FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;
        public Boolean isEmailValid(String email)
        {
            return email.Contains("@") && email.Contains(".");
        }
        public Boolean isDateValid(DateOnly start, DateOnly end)
        {
            return start < end;
        }
        public Boolean isDateAfterToday(DateOnly date)
        {
            return date > DateOnly.FromDateTime(DateTime.Now);
        }
        public Boolean isDateOverlap(DateOnly start1, DateOnly end1)
        {
            foreach (var x in context.BookingDetails)
            {
                if (start1 < x.EndDate && end1 > x.StartDate)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
