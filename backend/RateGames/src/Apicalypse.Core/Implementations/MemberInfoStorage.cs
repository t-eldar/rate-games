using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Apicalypse.Core.Interfaces;

namespace Apicalypse.Core.Implementations;

/// <inheritdoc cref="IMemberInfoStorage"/>
internal class MemberInfoStorage : IMemberInfoStorage
{
	private readonly Dictionary<Type, PropertyInfo[]> _store = new();
	public PropertyInfo[] GetProperties<T>()
	{
		if (_store.TryGetValue(typeof(T), out var propertyInfos))
		{
			return propertyInfos;
		}

		var props = typeof(T).GetProperties();
		_store.Add(typeof(T), props);

		return props;
	}
}
