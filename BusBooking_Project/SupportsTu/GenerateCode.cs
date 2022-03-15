using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking_Project.SupportsTu
{
    public sealed class GenerateCode
    {
        public static long GenerateNumber(int start, int end)
        {
            return new Random().Next(start, end);
        }

        public static string GenerateString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int letterOffset = 26;

            Random rd = new Random();
            for (int i = 0; i < size; i++)
            {
                var @char = (char)rd.Next(offset, offset + letterOffset);
                builder.Append(@char);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(GenerateString(4, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(GenerateNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(GenerateString(2));
            return passwordBuilder.ToString();
        }
    }
}
