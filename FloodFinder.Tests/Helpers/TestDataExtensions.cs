using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FloodFinder.Tests.Helpers
{
  public static class TestDataExtensions
  {
    public static void SetProperty<TSource, TProperty>(
      this TSource source,
      Expression<Func<TSource, TProperty>> prop,
      TProperty value)
    {
      var propertyInfo = (PropertyInfo)((MemberExpression)prop.Body).Member;
      propertyInfo.SetValue(source, value);
    }
  }
}