using System.Reflection;

namespace Apicalypse.Core.Interfaces;
/// <summary>
/// Storage for <see cref="MemberInfo"/>s to optimize reflection using.
/// </summary>
public interface IMemberInfoStorage
{
	PropertyInfo[] GetProperties<T>(); 
}
