using Apicalypse.Core.Implementations.Parsers;
using Apicalypse.Core.Interfaces.ExpressionParsers;

using NSubstitute;

namespace Apicalypse.Tests;
public class NewArrayExpressionParserTests
{
    private readonly NewArrayExpressionParser _sut;
    private readonly IConstantExpressionParser _constantExpressionParser;
    public NewArrayExpressionParserTests()
    {
        _constantExpressionParser = Substitute.For<IConstantExpressionParser>();
        _sut = new NewArrayExpressionParser(_constantExpressionParser);
    }
    [Fact]
    public void Parse_ReturnsString_WhenCorrectIntsPassed()
    {
        // Arrange
        ConstantExpression constantOne = Expression.Constant(1);
        ConstantExpression constantTwo = Expression.Constant(2);
        ConstantExpression constantThree = Expression.Constant(3);
        NewArrayExpression expression = Expression.NewArrayInit(typeof(int), constantOne, constantTwo, constantThree);

        _constantExpressionParser.Parse(constantOne).Returns("1");
        _constantExpressionParser.Parse(constantTwo).Returns("2");
        _constantExpressionParser.Parse(constantThree).Returns("3");

        var expected = "1,2,3";
        // Act
        var result = _sut.Parse(expression);
        // Assert
        Assert.Equal(expected, result);
    }
    [Fact]
    public void Parse_Throws_WhenNewArrayBounds()
    {
        // Arrange
        ConstantExpression constantThree = Expression.Constant(3);
        NewArrayExpression expression = Expression.NewArrayBounds(typeof(int), constantThree);
        _constantExpressionParser.Parse(constantThree).Returns("3");
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _sut.Parse(expression));
    }
}
