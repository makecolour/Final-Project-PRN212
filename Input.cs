using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Main_Project.Models;
using Microsoft.IdentityModel.Tokens;
using Question2;

namespace Main_Project
{
    internal class Input
    {
        private FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;
        public Boolean isEmailValid(String email)
        {
            if (!email.Contains("@") && email.Contains(".")||email.IsNullOrEmpty())
            {
                MessageBox.Show("Invalid email address", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(context.Customers.FirstOrDefault(x=>x.EmailAddress==email)!=null)
            {
                MessageBox.Show("Email already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public Boolean isDateValid(DateOnly start, DateOnly end)
        {
            if(start>end)
            {
                MessageBox.Show("Start date must be less than end date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        public Boolean isDateAfterToday(DateOnly date)
        {
            if (date > DateOnly.FromDateTime(DateTime.Now))
            {
                return true;
            }
            MessageBox.Show("Date must be after today", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
        public Boolean isDateOverlap(BookingDetail bd)
        {
            DateOnly start1 = bd.StartDate;
            DateOnly end1 = bd.EndDate;
            int roomId = bd.RoomId;
            foreach (var x in context.BookingDetails)
            {
                if (!(start1<x.StartDate&&end1<x.EndDate) && !(start1 > x.StartDate && end1 > x.EndDate) && x.RoomId == roomId)
                {
                    MessageBox.Show("Cannot book this date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }
            }
            return false;
        }

        public Boolean isValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 10 && phoneNumber.StartsWith("0") && !phoneNumber.All(char.IsDigit))
            {
                MessageBox.Show("Invalid phone number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if(context.Customers.FirstOrDefault(x=>x.Telephone==phoneNumber)!=null)
            {
                MessageBox.Show("Phone number already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
               return true;
            }
        }

        public string generateRandomString(int index)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < index; i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * new Random().NextDouble() + 65))));
            }
            return builder.ToString();
        }

        public bool isDate18YearsAgo(DateOnly date)
        {
            if (date.ToDateTime(new TimeOnly(0, 0)).AddYears(18) > DateTime.Now)
            {
                MessageBox.Show("Customer must be 18 years old", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
