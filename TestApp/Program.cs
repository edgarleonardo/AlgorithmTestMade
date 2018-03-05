using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            /// Calculate the weeks among two month selected
            Console.WriteLine(p.solution(2014, "April", "May", "Wednesday"));
            /// Calculate Money to pay among the time passed
            /// 
            Console.WriteLine(p.solution("10:00", "13:21"));
            Console.Read();
        }
        private string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        private int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        
        private Dictionary<string, int[][]> CalendarDictionary = new Dictionary<string, int[][]>();
        private Dictionary<string, int> CalendarMonthLimitDictionary = new Dictionary<string, int>();
        public int solution(int Y, string A, string B, string W)
        {
            FillCalendar(W, Y);
            return GetWeeks(A, B);
        }
        private int GetWeeks(string firstMonth, string lastMonth)
        {
            var calendarFirstMonth = CalendarDictionary[firstMonth];
            var dayInFirstMonth = CalendarMonthLimitDictionary[firstMonth];
            var calendarLastMonth = CalendarDictionary[lastMonth];
            var dayInLastMonth = CalendarMonthLimitDictionary[lastMonth];
            var daysFirstMonth = CountOfDaysFisrtMonthForVacations(calendarFirstMonth, dayInFirstMonth);
            var daysLastMonth = CountOfDaysLastMonthForVacations(calendarLastMonth, dayInLastMonth);
           
            return (daysFirstMonth+ daysLastMonth)/7;
        }
        private int CountOfDaysLastMonthForVacations(int[][] CalendarMatrix, int dayInMonth)
        {
            var dayStart = 0;
            for (int count = CalendarMatrix.Length - 1; count >= 0; count--)
            {
                for (int countRow = CalendarMatrix[count].Length-1; countRow >= 0; countRow--)
                {
                    if (CalendarMatrix[count][countRow] != 0 && countRow != 6)
                    {
                        dayStart++;
                    }
                }
                if (dayStart > 0)
                {
                    break;
                }

            }
            return dayInMonth - dayStart;
        }
        private int CountOfDaysFisrtMonthForVacations(int[][] CalendarMatrix, int dayInMonth)
        {
            var dayStart = 0;
            for (int count = 0; count < CalendarMatrix.Length; count++)
            {
                for (int countRow = 0; countRow < CalendarMatrix[count].Length; countRow++)
                {
                    if (CalendarMatrix[count][countRow] != 0 && countRow == 0)
                    {
                        dayStart = dayInMonth - CalendarMatrix[count][countRow] + 1;
                        break;
                    }
                }
                if (dayStart != 0)
                {
                    break;
                }

            }
            return dayStart;
        }
        private int GetDayIndex(string day)
        {
            var resultValue = 0;
            for(int index =0; index < days.Length;index++)
            {
                if (days[index] == day)
                {
                    resultValue = index;
                    break;
                }
            }
            return resultValue;
        }
        private void FillCalendar(string dayStart, int year)
        {
            var currentDay = GetDayIndex(dayStart);
            for (int monthCounter = 0; monthCounter < daysInMonth.Length;monthCounter++)
            {
                int[][] CalendarMatrix = new int[6][];
                var daysCounter = 1;
                var daysInMonths = daysInMonth[monthCounter];
                if (monthCounter == 1 && (year%4) == 0)
                {
                    daysInMonths = 29;
                }
                for (int counter = 0; counter < 6; counter++)
                {
                    CalendarMatrix[counter] = new int[days.Length];
                    for (int counterDays = 0; counterDays < days.Length; counterDays++)
                    {
                        if (daysCounter <= daysInMonths && currentDay == counterDays)
                        {
                            CalendarMatrix[counter][counterDays] = daysCounter;
                            daysCounter++;
                            currentDay ++;
                            if (currentDay >= 7)
                            {
                                currentDay = 0;
                            }
                        }
                        else
                        {
                            CalendarMatrix[counter][counterDays] = 0;
                        }
                    }
                }
                CalendarDictionary.Add(months[monthCounter], CalendarMatrix);
                CalendarMonthLimitDictionary.Add(months[monthCounter], daysInMonths);
            }
        }

        const int ENTRANCE = 2;
        const int FIRST_HOUR = 3;
        const int AFTER_FIRST_HOUR = 4;
        public int solution(string E, string L)
        {
            var arrayEDate = E.Split(':');
            var arrayLDate = L.Split(':');
            var timeE = new TimeSpan(int.Parse(arrayEDate[0]), int.Parse(arrayEDate[1]), 0);
            var timeL = new TimeSpan(int.Parse(arrayLDate[0]), int.Parse(arrayLDate[1]), 0);
            var diff = timeL.Subtract(timeE);
            var totalHours = diff.TotalHours;
            var totalHoursWithoutDecimalPart = (int)totalHours;
            /// Per entrance
            var totalAmoutToPay = 2;
            /// Per hours
            if (totalHours <= 1)
            {
                totalAmoutToPay += FIRST_HOUR;
            }
            else if (totalHours > 1)
            {
                totalAmoutToPay += FIRST_HOUR;
                var restOfHours = (int)(totalHours - 1);
                if (restOfHours >= 1)
                {
                    totalAmoutToPay += restOfHours * AFTER_FIRST_HOUR;
                }
                else
                {
                    totalAmoutToPay += AFTER_FIRST_HOUR;
                }
                if (totalHours > totalHoursWithoutDecimalPart)
                {
                    totalAmoutToPay += AFTER_FIRST_HOUR;
                }
            }
            return totalAmoutToPay;
        }
    }
}
