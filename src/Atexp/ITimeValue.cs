﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Atexp
{
    public interface ITimeValue
    {
        int GetValue(Time time);
    }
    public record Year4TimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return time.Datetime.Year;
        }
    }
    public record Year3TimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return time.Datetime.Year % 1000;
        }
    }
    public record Year2TimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return time.Datetime.Year % 100;
        }
    }
    public record MonthTimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return time.Datetime.Month;

        }
    }
    public record DayTimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return time.Datetime.Day;
        }
    }
    //当月的倒数第几天
    public record LastDayTimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            var nextMonth = new DateTime(time.Datetime.Year, time.Datetime.Month, 1).AddMonths(1);
            return (int)(nextMonth - time.Datetime.Date).TotalDays;
        }
    }
    public record Hour24TimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return time.Datetime.Hour;
        }
    }
    public record Hour12TimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return time.Datetime.Hour % 12;
        }
    }
    public record MinuteTimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return time.Datetime.Minute;
        }
    }
    public record SecondTimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return time.Datetime.Second;
        }
    }
    public record WeekTimeValue : ITimeValue
    {
        public int GetValue(Time time)
        {
            return (int)time.Datetime.DayOfWeek;
        }
    }
}
