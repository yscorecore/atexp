﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Atexp
{
    public class TimeExpression
    {
        private static readonly Dictionary<string, ITimeValue> valueResolvers = new();
        static TimeExpression()
        {
            AddResolvers(new Year4TimeValue(), "Year", "year", "yyyy", "yyy", "yy", "y");
            AddResolvers(new MonthTimeValue(), "Month", "month", "MM", "M");
            AddResolvers(new DayTimeValue(), "Day", "day", "dd", "d");
            AddResolvers(new Hour24TimeValue(), "Hour", "hour", "HH", "H", "hh", "h");
            AddResolvers(new MinuteTimeValue(), "Minute", "minute", "min", "mm", "m");
            AddResolvers(new SecondTimeValue(), "Second", "second", "sec", "ss", "s");
            AddResolvers(new WeekTimeValue(), "Week", "week", "www", "w");
            AddResolvers(new LastDayTimeValue(), "LastDayOfMonth", "lastdayofmonth", "ldom");
            AddResolvers(new TimeStampTimeValue(), "TimeStamp", "timestamp", "ts");
            AddResolvers(new TimeStampOfDayTimeValue(), "TimeStampOfDay", "timestampofday", "tsod");
            static void AddResolvers(ITimeValue valueResolver, params string[] names)
            {
                Array.ForEach(names, (name) => valueResolvers.Add(name, valueResolver));
            }
        }
        private readonly string[] names;
        private readonly Delegate @delegate;
        private TimeExpression(string[] names, Delegate @delegate)
        {
            this.names = names;
            this.@delegate = @delegate;
        }
        public static bool Match(string expression, DateTimeOffset dateTimeOffset)
        {
            return Create(expression).Match(Time.FromDatetimeOffset(dateTimeOffset));
        }

        private static ConcurrentDictionary<string, object> cache = new ConcurrentDictionary<string, object>();
        public static TimeExpression Create(string expression)
        {
            var obj = cache.GetOrAdd(expression, (exp) =>
            {
                try
                {
                    return CreateInternal(exp);
                }
                catch (Exception ex)
                {
                    return ex;
                }
            });
            if (obj is TimeExpression te)
            {
                return te;
            }
            else
            {
                throw (Exception)obj;
            }
        }
        public static TimeExpression CreateInternal(string expression)
        {

            if (string.IsNullOrEmpty(expression))
            {
                throw new TimeExpressionException("Invalid expression.");
            }

            if (expression.Length > 1024)
            {
                throw new TimeExpressionException("Time expression too long.");
            }
            var names = Regex.Matches(expression, @"[a-zA-Z]+")
                    .OfType<Match>()
                    .Select(p => p.Value)
                    .Distinct().ToArray();

            var unknowName = names.Where(p => !valueResolvers.ContainsKey(p)).FirstOrDefault();
            if (unknowName != null)
            {
                throw new TimeExpressionException($"Unknow time part name '{unknowName}'.");
            }
            try
            {
                var parameters = names.Select(p => Expression.Parameter(typeof(int), p)).ToArray();
                var expar = DynamicExpressionParser.ParseLambda(parameters, typeof(bool), expression);
                var delegateFunc = expar.Compile();
                return new TimeExpression(names, delegateFunc);
            }
            catch (Exception ex)
            {
                throw new TimeExpressionException("Invalid expression.", ex);
            }
        }
        public bool Match(Time time)
        {
            object[] values = this.names.Select(p => valueResolvers[p].GetValue(time)).Cast<object>().ToArray();
            return (bool)this.@delegate.DynamicInvoke(values);
        }

    }
}
