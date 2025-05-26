using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

public class GlobalExceptionFilterTests
{
    [Fact]
    public void OnException_ShouldSetExceptionHandledAndReturnObjectResult()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<GlobalExceptionFilter>>();
        var filter = new GlobalExceptionFilter(loggerMock.Object);

        var exception = new Exception("Test exception");
        var context = new ExceptionContext(new ActionContext(), new List<IFilterMetadata>())
        {
            Exception = exception
        };

        // Act
        filter.OnException(context);

        // Assert
        context.ExceptionHandled.Should().BeTrue();
        context.Result.Should().BeOfType<ObjectResult>();

        var objectResult = context.Result as ObjectResult;
        objectResult.StatusCode.Should().Be(500);

        var value = objectResult.Value as dynamic;
        string message = value.Message;
        message.Should().Be("An unexpected error occurred. Please try again later.");

        // Verify logger was called
        loggerMock.Verify(
            x => x.LogError(exception, "Unhandled exception occurred"),
            Times.Once
        );
    }
}
