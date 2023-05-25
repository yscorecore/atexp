using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;

namespace Atexp.UnitTest
{
    public class TimeExpressionTest
    {

        [Theory]

        [InlineData("", "2023-05-23")]
        public void ShouldMatch(string expression, string dateTime)
        {
            TimeExpression.Match(expression, DateTimeOffset.Parse(dateTime)).Should().BeTrue();
        }

        [Theory]

        [InlineData("", "2023-05-23")]
        public void ShouldNotMatch(string expression, string dateTime)
        {
            TimeExpression.Match(expression, DateTimeOffset.Parse(dateTime)).Should().BeFalse();
        }

        [Theory]
        [InlineData("", "2023-05-23")]
        public void ShouldThrowTimeExpression(string expression, string message)
        {
            Action action = () => TimeExpression.Match(expression, DateTimeOffset.Now);
            action.Should().Throw<TimeExpressionException>().WithMessage(message);
        }
    }
}
