using System;
using FluentAssertions;
using Xunit;

namespace Atexp.UnitTest
{
    public class TimeExpressionTest
    {

        [Theory]
        [InlineData("Year=2023", "2023-05-23")]
        [InlineData("year=2023", "2023-05-23")]
        [InlineData("yyyy=2023", "2023-05-23")]
        [InlineData("yyy=2023", "2023-05-23")]
        [InlineData("yy=2023", "2023-05-23")]
        [InlineData("y=2023", "2023-05-23")]


        [InlineData("Month=5", "2023-05-23")]
        [InlineData("month=5", "2023-05-23")]
        [InlineData("MM=5", "2023-05-23")]
        [InlineData("M=5", "2023-05-23")]

        [InlineData("Day=23", "2023-05-23")]
        [InlineData("day=23", "2023-05-23")]
        [InlineData("dd=23", "2023-05-23")]
        [InlineData("d=23", "2023-05-23")]

        [InlineData("Hour=15", "2023-05-23 15:41:08")]
        [InlineData("hour=15", "2023-05-23 15:41:08")]
        [InlineData("hh=15", "2023-05-23 15:41:08")]
        [InlineData("h=15", "2023-05-23 15:41:08")]

        [InlineData("Minute=41", "2023-05-23 15:41:08")]
        [InlineData("minute=41", "2023-05-23 15:41:08")]
        [InlineData("min=41", "2023-05-23 15:41:08")]
        [InlineData("mm=41", "2023-05-23 15:41:08")]
        [InlineData("m=41", "2023-05-23 15:41:08")]


        [InlineData("Second=8", "2023-05-23 15:41:08")]
        [InlineData("second=8", "2023-05-23 15:41:08")]
        [InlineData("sec=8", "2023-05-23 15:41:08")]
        [InlineData("ss=8", "2023-05-23 15:41:08")]
        [InlineData("s=8", "2023-05-23 15:41:08")]

        [InlineData("Week=2", "2023-05-23 15:41:08")]
        [InlineData("week=2", "2023-05-23 15:41:08")]
        [InlineData("www=2", "2023-05-23 15:41:08")]
        [InlineData("w=2", "2023-05-23 15:41:08")]


        [InlineData("LastDayOfMonth=9", "2023-05-23")]
        [InlineData("lastdayofmonth=9", "2023-05-23")]
        [InlineData("ldom=9", "2023-05-23")]

        [InlineData("TimeStamp%5=3", "2023-05-23 15:41:08")]
        [InlineData("timestamp%5=3", "2023-05-23 15:41:08")]
        [InlineData("ts%5=3", "2023-05-23 15:41:08")]

        [InlineData("TimeStampOfDay=68", "2023-05-23 00:01:08")]
        [InlineData("timestampofday=68", "2023-05-23 00:01:08")]
        [InlineData("tsod=68", "2023-05-23 00:01:08")]


        [InlineData("Time=154108", "2023-05-23 15:41:08")]
        [InlineData("time=154108", "2023-05-23 15:41:08")]
        [InlineData("t=154108", "2023-05-23 15:41:08")]

        [InlineData("Date=20230523", "2023-05-23 15:41:08")]
        [InlineData("date=20230523", "2023-05-23 15:41:08")]

        [InlineData("d=23 & month=5 & hour = 15", "2023-05-23 15:41:08")]
        public void ShouldMatch(string expression, string dateTime)
        {
            TimeExpression.Match(expression, DateTimeOffset.Parse(dateTime)).Should().BeTrue();
        }

        [Theory]
        [InlineData("Year!=2023", "2023-05-23")]
        [InlineData("year!=2023", "2023-05-23")]
        [InlineData("yyyy!=2023", "2023-05-23")]
        [InlineData("yyy!=2023", "2023-05-23")]
        [InlineData("yy!=2023", "2023-05-23")]
        [InlineData("y!=2023", "2023-05-23")]


        [InlineData("Month!=5", "2023-05-23")]
        [InlineData("month!=5", "2023-05-23")]
        [InlineData("MM!=5", "2023-05-23")]
        [InlineData("M!=5", "2023-05-23")]

        [InlineData("Day!=23", "2023-05-23")]
        [InlineData("day!=23", "2023-05-23")]
        [InlineData("dd!=23", "2023-05-23")]
        [InlineData("d!=23", "2023-05-23")]

        [InlineData("Hour!=15", "2023-05-23 15:41:08")]
        [InlineData("hour!=15", "2023-05-23 15:41:08")]
        [InlineData("hh!=15", "2023-05-23 15:41:08")]
        [InlineData("h!=15", "2023-05-23 15:41:08")]

        [InlineData("Minute!=41", "2023-05-23 15:41:08")]
        [InlineData("minute!=41", "2023-05-23 15:41:08")]
        [InlineData("min!=41", "2023-05-23 15:41:08")]
        [InlineData("mm!=41", "2023-05-23 15:41:08")]
        [InlineData("m!=41", "2023-05-23 15:41:08")]


        [InlineData("Second!=8", "2023-05-23 15:41:08")]
        [InlineData("second!=8", "2023-05-23 15:41:08")]
        [InlineData("sec!=8", "2023-05-23 15:41:08")]
        [InlineData("ss!=8", "2023-05-23 15:41:08")]
        [InlineData("s!=8", "2023-05-23 15:41:08")]

        [InlineData("Week!=2", "2023-05-23 15:41:08")]
        [InlineData("week!=2", "2023-05-23 15:41:08")]
        [InlineData("www!=2", "2023-05-23 15:41:08")]
        [InlineData("w!=2", "2023-05-23 15:41:08")]
        public void ShouldNotMatch(string expression, string dateTime)
        {
            TimeExpression.Match(expression, DateTimeOffset.Parse(dateTime)).Should().BeFalse();
        }

        [Theory]
        [InlineData("", "Invalid expression.")]
        [InlineData("abc=1", "Unknow time part name 'abc'.")]
        [InlineData("year+1", "Invalid expression.")]
        public void ShouldThrowTimeExpression(string expression, string message)
        {
            Action action = () => TimeExpression.Match(expression, DateTimeOffset.Now);
            action.Should().Throw<TimeExpressionException>().WithMessage(message);
        }
    }
}
