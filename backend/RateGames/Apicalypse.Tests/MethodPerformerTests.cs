using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq.Expressions;

using Apicalypse.Core.Extensions;
using Apicalypse.Core.Implementations;
using Apicalypse.Core.Interfaces;

namespace Apicalypse.Tests;
public class MethodPerformerTests
{
	private readonly IMethodPerformer _sut = new MethodPerformer();

	[Theory]
	[MemberData(nameof(StringOneParamTestData))]
	public void PerformAllStringMethods_ShouldReturnQueryPart_WhenOneParameterPassed(
		string methodName,
		Type passedType,
		object passedValue,
		string expected)
	{
		// Arrange
		var method = typeof(string).GetMethod(methodName, new[] { passedType })!;
		var testText = "Some text";
		var methodCallExpression = Expression.Call(
			Expression.Constant(testText),
			method,
			Expression.Constant(passedValue));

		// Act
		var result = _sut.PerformStringComparison(methodCallExpression);
		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(StringTwoParamsTestData))]
	public void PerformAllStringMethods_ShouldReturnQueryPart_WhenTwoParameterPassed(
		string methodName,
		string passedValue,
		StringComparison passedComparison,
		string expected)
	{
		// Arrange
		var method = typeof(string).GetMethod(methodName, new[] { typeof(string), typeof(StringComparison) })!;
		var testText = "Some text";
		var methodCallExpression = Expression.Call(
			Expression.Constant(testText),
			method,
			Expression.Constant(passedValue),
			Expression.Constant(passedComparison));

		// Act
		var result = _sut.PerformStringComparison(methodCallExpression);
		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(StringThreeParamsTestData))]
	public void PerformEndsAndStarts_ShouldReturnQueryPart_WhenThreeParameterPassed(
		string methodName,
		string passedValue,
		bool ignoreCase,
		string expected)
	{
		// Arrange
		var method = typeof(string).GetMethod(methodName, new[] { typeof(string), typeof(bool), typeof(CultureInfo) })!;
		var testText = "Some text";
		var methodCallExpression = Expression.Call(
			Expression.Constant(testText),
			method,
			Expression.Constant(passedValue),
			Expression.Constant(ignoreCase),
			Expression.Constant(new CultureInfo("ru")));

		// Act
		var result = _sut.PerformStringComparison(methodCallExpression);
		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void PerformIncludeProperties_ShouldReturnQueryPart()
	{
		// Arrange
		object obj = 3;
		var methodCallExpression = Expression.Call(
			typeof(ObjectExtensions),
			nameof(ObjectExtensions.IncludeProperties),
			new[] { typeof(int) },
			Expression.Constant(obj));
		var expected = ".*";
		// Act
		var result = _sut.PerformIncludeProperties();
		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(EnumerableMethodsTestData))]
	public void PerformArrayMethods_ShouldReturnQueryPart(Type type, string methodName, char leftBrace, char rightBrace)
	{
		// Arrange
		var source = new[] { 1, 2, 3 };
		var methodCallExpression = Expression.Call(
			type,
			methodName,
			new[] { typeof(int) },
			Expression.Constant(source),
			Expression.NewArrayInit(typeof(int), new[] { Expression.Constant(1), Expression.Constant(2) }));
		var expected = $" = {leftBrace}1,2{rightBrace}";

		// Act
		var result = _sut.PerformArrayComparison(methodCallExpression);
		// Assert
		Assert.Equal(expected, result);
	}

	public static IEnumerable<object[]> EnumerableMethodsTestData()
	{
		yield return new object[] { typeof(EnumerableExtensions), nameof(EnumerableExtensions.ContainsAll), '[', ']' };
		yield return new object[] { typeof(EnumerableExtensions), nameof(EnumerableExtensions.ContainsAny), '(', ')' };
		yield return new object[] { typeof(Enumerable), nameof(Enumerable.SequenceEqual), '{', '}' };
	}
	public static IEnumerable<object[]> StringThreeParamsTestData()
	{
		var value = "Some";

		yield return new object[]
		{
			nameof(string.StartsWith),
			value,
			true,
			$" ~ \"{value}\"*"
		};
		yield return new object[]
		{
			nameof(string.EndsWith),
			value,
			false,
			$" = *\"{value}\""
		};
	}
	public static IEnumerable<object[]> StringTwoParamsTestData()
	{
		var value = "Some";

		yield return new object[]
		{
			nameof(string.StartsWith),
			value,
			StringComparison.Ordinal,
			$" = \"{value}\"*"
		};
		yield return new object[]
		{
			nameof(string.EndsWith),
			value,
			StringComparison.InvariantCultureIgnoreCase,
			$" ~ *\"{value}\""
		};
	}
	public static IEnumerable<object[]> StringOneParamTestData()
	{
		var stringValue = "Some";
		var charValue = 'S';

		yield return new object[] { nameof(string.StartsWith), typeof(string), stringValue, $" = \"{stringValue}\"*" };
		yield return new object[] { nameof(string.StartsWith), typeof(char), charValue, $" = \"{charValue}\"*" };

		yield return new object[] { nameof(string.EndsWith), typeof(string), stringValue, $" = *\"{stringValue}\"" };
		yield return new object[] { nameof(string.EndsWith), typeof(char), charValue, $" = *\"{charValue}\"" };

		yield return new object[] { nameof(string.Contains), typeof(string), stringValue, $" = *\"{stringValue}\"*" };
		yield return new object[] { nameof(string.Contains), typeof(char), charValue, $" = *\"{charValue}\"*" };
	}
}
