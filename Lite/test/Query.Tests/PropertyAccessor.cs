using System.Linq.Expressions;
using System.Reflection;

namespace Query;

internal readonly struct PropertyAccessor<TModel, TProperty>
{
    public PropertyAccessor(Expression<Func<TModel, TProperty>> expr)
    {
        if (expr.Body is MemberExpression m)
        {
            _name = m.Member.Name;
            return;
        }
        if (expr.Body is UnaryExpression u && u.Operand is MemberExpression um)
        {
            _name = um.Member.Name;
            return;
        }
        throw new ArgumentException("Not a member expression");
    }

    private readonly string _name;

    public void Set(TModel model, TProperty value) =>
        typeof(TModel)
            .GetField($"<{_name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(model, value);
}

internal static class PropertyAccessorExtensions
{
    extension<TModel>(TModel model)
    {
        public void Set<TProperty>(Expression<Func<TModel, TProperty>> expr, TProperty value)
        {
            var accessor = new PropertyAccessor<TModel, TProperty>(expr);
            accessor.Set(model, value);
        }
    }
}
