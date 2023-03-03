using System.Linq.Expressions;

using Apicalypse.Core.Extensions;
using Apicalypse.Core.Implementations;
using Apicalypse.Core.Interfaces;
using Apicalypse.Core.Interfaces.ExpressionParsers;
using Apicalypse.Tests.TestModels;

using NSubstitute;

namespace Apicalypse.Tests;
public class ExpressionParserTests
{
	private readonly QueryParser _sut;

	private readonly IConstantExpressionParser _constantParser;
	private readonly IMemberExpressionParser _memberParser;
	private readonly IBinaryExpressionParser _binaryParser;
	private readonly INewExpressionParser _newParser;
	private readonly IMethodCallExpressionParser _methodCallParser;

	public ExpressionParserTests()
	{
		_constantParser = Substitute.For<IConstantExpressionParser>();
		_memberParser = Substitute.For<IMemberExpressionParser>();
		_binaryParser = Substitute.For<IBinaryExpressionParser>();
		_newParser = Substitute.For<INewExpressionParser>();
		_methodCallParser = Substitute.For<IMethodCallExpressionParser>();

		_sut = new QueryParser(_memberParser, _newParser, _binaryParser, _constantParser, _methodCallParser);
	}

	[Fact]
	public void Parse_ReturnsParsed_WhenMemberAccesChainPassed()
	{
		// Arrange
		Expression<Func<Person, string>> expression = (main) => main.Country.Name;
		var expected = "Country.Name";
		_memberParser.Parse(null!, null!).ReturnsForAnyArgs(expected);
		// Act
		var result = _sut.Parse(expression);
		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Parse_ReturnsParsed_WhenConditionalPassed()
	{
		// Arrange
		Expression<Func<Person, bool>> expression = (p) => p.Id == 2 || p.Country.Name == "ff";
		var expected = "Id = 2 | Country.Name = \"ff\"";
		_binaryParser.Parse(null!, null!).ReturnsForAnyArgs(expected);
		// Act
		var result = _sut.Parse(expression);
		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Parse_ReturnsParsed_WhenNewObjectPassed()
	{
		// Arrange
		Expression<Func<Person, object>> expression = (p) => new
		{
			p.Id,
			p.Array,
			Dependence = p.Country.IncludeAllProperties()
		};
		var expected = "Id,Array,Country.*";
		_newParser.Parse(null!, null!).ReturnsForAnyArgs(expected);
		// Act
		var result = _sut.Parse(expression);
		// Assert
		Assert.Equal(expected, result);
	}
}
