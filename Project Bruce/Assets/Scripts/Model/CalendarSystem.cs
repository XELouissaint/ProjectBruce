using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarSystem
{

    public CalendarSystem()
    {
        Months = new List<Month>();
        Day = 1;
        Month = 1;
        Year = 1;
    }

    public List<Month> Months;

    public int Day;
    public int Month;
    public int Year;

    public static CalendarSystem Gregorian()
    {
        CalendarSystem calendar = new CalendarSystem();

        calendar.Months.Add(new Month(calendar, "Jan", 31));
        calendar.Months.Add(new Month(calendar, "Feb", 28));
        calendar.Months.Add(new Month(calendar, "Mar", 31));
        calendar.Months.Add(new Month(calendar, "Apr", 30));
        calendar.Months.Add(new Month(calendar, "May", 31));
        calendar.Months.Add(new Month(calendar, "Jun", 30));
        calendar.Months.Add(new Month(calendar, "Jul", 31));
        calendar.Months.Add(new Month(calendar, "Aug", 31));
        calendar.Months.Add(new Month(calendar, "Sep", 30));
        calendar.Months.Add(new Month(calendar, "Oct", 31));
        calendar.Months.Add(new Month(calendar, "Nov", 30));
        calendar.Months.Add(new Month(calendar, "Dec", 31));

        return calendar;
    }

    public void TickDay()
    {
        AdvanceDay(ref Day, ref Month, ref Year);
    }

    void AdvanceDay(ref int day, ref int month, ref int year)
    {
        if (day >= Months[month - 1].NumberDays)
        {
            if (month == Months.Count)
            {
                month = 0;
                year++;
            }
            month++;
            day = 1;
            return;
        }

        day++;

    }

    public string GetFutureDateString(int daysAhead)
    {
        int currentDay = Day;
        int currentMonth = Month;
        int currentYear = Year;
        for (int i = 0; i < daysAhead; i++)
        {
            AdvanceDay(ref currentDay, ref currentMonth, ref currentYear);
        }

        return DateString(currentDay, currentMonth, currentYear);
    }

    public string DateString(int day, int month, int year)
    {
        return string.Format("{0}/{1}/{2}", day, month, year.ToString("00"));

    }
    public string DateString()
    {
        return string.Format("{0}/{1}/{2}", Day, Month, Year.ToString("00"));
    }
}

public class Month
{
    public Month(CalendarSystem time, string name, int numberDays)
    {
        CalandarSystem = time;
        Name = name;
        NumberDays = numberDays;
    }

    public string Name;
    public CalendarSystem CalandarSystem;
    public int NumberDays;

    public Action OnEnded;
}
