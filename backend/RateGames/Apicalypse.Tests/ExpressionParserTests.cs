using System.Linq.Expressions;
using System.Reflection;

using Apicalypse.Core.Extensions;
using Apicalypse.Core.Implementations;
using Apicalypse.Core.Interfaces;

using NSubstitute;

namespace Apicalypse.Tests;
public class ExpressionParserTests
{
	private readonly IMethodPerformer _methodPerformer;
	private readonly IExpressionParser _sut;
	public ExpressionParserTests()
	{
		_methodPerformer = Substitute.For<IMethodPerformer>();
		_sut = new ExpressionParser(_methodPerformer);
	}

	[Fact]
	public void Parse_ReturnsParsed_WhenMemberAccesChainPassed()
	{
		// Arrange
		Expression<Func<TestMain, string>> expression = (main) => main.Dependence.Name;
		var expected = "Dependence.Name";

		// Act
		var result = _sut.Parse(expression);
		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(ConditionalTestData))]
	public void Parse_ReturnsParsed_WhenConditionalPassed(Expression<Func<TestMain, bool>> expression, string expected)
	{
		_methodPerformer.Perform(null!).ReturnsForAnyArgs(" = \"Hello\"");
		var result = _sut.Parse(expression);
		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(NewObjectTestData))]
	public void Parse_ReturnsParsed_WhenNewObjectPassed(Expression<Func<TestMain, object>> expression, string expected)
	{
		_methodPerformer.Perform(null!).ReturnsForAnyArgs(".*");
		var result = _sut.Parse(expression);
		Assert.Equal(expected, result);
	}

	public static IEnumerable<object[]> NewObjectTestData()
	{
		yield return new object[]
		{
			(Expression<Func<TestMain, object>>)((TestMain main) => new { main.Id, main.Dependence, main.Dependence.Name }),
			"Id,Dependence,Dependence.Name",
		};
		yield return new object[]
		{
			(Expression<Func<TestMain, object>>)((TestMain main) => new
			{
				All = main.Dependence.IncludeProperties(),
				main.Id
			}),
			"Dependence.*,Id",
		};
	}

	public static IEnumerable<object[]> ConditionalTestData()
	{
		yield return new object[]
		{
			(Expression<Func<TestMain, bool>>)((TestMain main) => main.Id == 25 || main.Dependence.Name == "Hello"),
			"Id = 25 | Dependence.Name = \"Hello\""
		};
		yield return new object[]
		{
			(Expression<Func<TestMain, bool>>)((TestMain main) => main.Id != 10 && main.Dependence.Id > 12),
			"Id != 10 & Dependence.Id > 12"
		};
		yield return new object[]
		{
			(Expression<Func<TestMain, bool>>)((TestMain main) => main.Id < 12 && main.Id >= 12 && main.Id <= 12),
			"Id < 12 & Id >= 12 & Id <= 12"
		};
		yield return new object[]
		{
			(Expression<Func<TestMain, bool>>)((TestMain main) => main.Dependence.Name.StartsWith("Hello")),
			"Dependence.Name = \"Hello\""
		};
	}

	public class TestMain
	{
		public int Id { get; set; }
		public TestDependence Dependence { get; set; } = null!;
		public int[] Array { get; set; } = null!;
	}
	public class TestDependence
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
	}
}
