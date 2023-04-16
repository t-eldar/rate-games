using Apicalypse.Core.Implementations.Parsers;
using Apicalypse.Tests.TestModels;

namespace Apicalypse.Tests;
public class ConstantExpressionParserTests
{
    private readonly ConstantExpressionParser _sut = new();

    [Fact]
    public void Parse_ReturnsMemberString_WhenIntPassed()
    {
        // Arrange
        Expression<Func<Person, int>> expression = c => 12;
        var expected = "12";
        // Act
        var result = _sut.Parse((expression.Body as ConstantExpression)!);
        // Assert
        Assert.Equal(expected, result);
    }
    [Fact]
    public void Parse_ReturnsMemberString_WhenNegativeIntPassed()
    {
        // Arrange
        Expression<Func<Person, int>> expression = c => -12;
        var expected = "-12";
        // Act
        var result = _sut.Parse((expression.Body as ConstantExpression)!);
        // Assert
        Assert.Equal(expected, result);
    }
    [Fact]
    public void Parse_ReturnsMemberString_WhenDoublePassed()
    {
        // Arrange
        Expression<Func<Person, double>> expression = c => 1.5d;
        var expected = "1.5";
        // Act
        var result = _sut.Parse((expression.Body as ConstantExpression)!);
        // Assert
        Assert.Equal(expected, result);
    }
    [Fact]
    public void Parse_ReturnsMemberString_WhenNegativeDoublePassed()
    {
        // Arrange
        Expression<Func<Person, double>> expression = c => -1.5d;
        var expected = "-1.5";
        // Act
        var result = _sut.Parse((expression.Body as ConstantExpression)!);
        // Assert
        Assert.Equal(expected, result);
    }
    [Fact]
    public void Parse_ReturnsMemberString_WhenStringPassed()
    {
        // Arrange
        Expression<Func<Person, string>> expression = c => "Hello";
        var expected = "\"Hello\"";
        // Act
        var result = _sut.Parse((expression.Body as ConstantExpression)!);
        // Assert
        Assert.Equal(expected, result);
    }
    [Fact]
    public void Parse_ReturnsMemberString_WhenCharPassed()
    {
        // Arrange
        Expression<Func<Person, char>> expression = c => 'F';
        var expected = "\"F\"";
        // Act
        var result = _sut.Parse((expression.Body as ConstantExpression)!);
        // Assert
        Assert.Equal(expected, result);
    }
}
