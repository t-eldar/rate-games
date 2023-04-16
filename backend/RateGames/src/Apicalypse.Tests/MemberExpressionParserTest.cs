using Apicalypse.Core.Implementations.Parsers;
using Apicalypse.Tests.TestModels;

namespace Apicalypse.Tests;
public class MemberExpressionParserTest
{
    private readonly MemberExpressionParser _sut = new();

    [Fact]
    public void Parse_ReturnsMemberString_WhenTwoAccessess()
    {
        // Arrange
        Expression<Func<Person, string>> expression = c => c.Country.Name;
        var expected = "Country.Name";
        // Act
        var result = _sut.Parse((expression.Body as MemberExpression)!);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_ReturnsMemberString_WhenThreeAccessess()
    {
        // Arrange
        Expression<Func<Person, string>> expression = c => c.Country.Flag.Name;
        var expected = "Country.Flag.Name";
        // Act
        var result = _sut.Parse((expression.Body as MemberExpression)!);
        // Assert
        Assert.Equal(expected, result);
    }
}
