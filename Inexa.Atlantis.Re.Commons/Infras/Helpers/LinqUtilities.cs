using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Inexa.Atlantis.Re.Commons.Infras.Helpers
{
    public static class PredicateBuilder
    {
        internal class SubstExpressionVisitor : ExpressionVisitor
        {
            public Dictionary<Expression, Expression> subst = new Dictionary<Expression, Expression>();

            protected override Expression VisitParameter(ParameterExpression node)
            {
                Expression newValue;
                if (subst.TryGetValue(node, out newValue))
                {
                    return newValue;
                }
                return node;
            }
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {
            var p = a.Parameters[0];

            var visitor = new SubstExpressionVisitor();
            visitor.subst[b.Parameters[0]] = p;

            var body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {
            var p = a.Parameters[0];

            var visitor = new SubstExpressionVisitor();
            visitor.subst[b.Parameters[0]] = p;

            var body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        /// <summary>
        /// Supports sorting of a given member as an expression when type is not known. It solves problem with LINQ to Entities unable to
        /// cast different types as 'System.DateTime', 'System.DateTime?' to type 'System.Object'.
        /// LINQ to Entities only supports casting Entity Data Model primitive types.
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <param name="query">query to apply sorting on.</param>
        /// <param name="expression">the member expression to apply</param>
        /// <param name="sortOrder">the sort order to apply</param>
        /// <returns>Query with sorting applied as IOrderedQueryable of type T</returns>
        public static IOrderedQueryable<T> OrderByMember<T>(
        this IQueryable<T> query,
            Expression<Func<T, object>> expression,
            SortOrder sortOrder)
        {
            var body = expression.Body as UnaryExpression;

            if (body == null)
            {
                return sortOrder == SortOrder.Ascending
                    ? query.OrderBy(expression)
                    : query.OrderByDescending(expression);
            }
            var memberExpression = body.Operand as MemberExpression;

            return memberExpression != null
                ? (IOrderedQueryable<T>)
                    query.Provider.CreateQuery(
                        Expression.Call(
                            typeof(Queryable),
                            sortOrder == SortOrder.Ascending ? "OrderBy" : "OrderByDescending",
                            new [] { typeof(T), memberExpression.Type },
                            query.Expression,
                            Expression.Lambda(memberExpression, expression.Parameters)))
                : (sortOrder == SortOrder.Ascending ? query.OrderBy(expression) : query.OrderByDescending(expression));
        }

        /// <summary>
        /// Supports sorting of a given member as an expression when type is not known. It solves problem with LINQ to Entities unable to
        /// cast different types as 'System.DateTime', 'System.DateTime?' to type 'System.Object'.
        /// LINQ to Entities only supports casting Entity Data Model primitive types.
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <param name="query">query to apply sorting on.</param>
        /// <param name="expression">the member expression to apply</param>
        /// <param name="sortOrder">the sort order to apply</param>
        /// <returns>Query with sorting applied as IOrderedQueryable of type T</returns>
        public static IOrderedQueryable<T> ThenByMember<T>(
        this IQueryable<T> query,
            Expression<Func<T, object>> expression,
            SortOrder sortOrder)
        {
            return ((IOrderedQueryable<T>)query).ThenByMember(expression, sortOrder);
        }

        /// <summary>
        /// Supports sorting of a given member as an expression when type is not known. It solves problem with LINQ to Entities unable to
        /// cast different types as 'System.DateTime', 'System.DateTime?' to type 'System.Object'.
        /// LINQ to Entities only supports casting Entity Data Model primitive types.
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <param name="query">query to apply sorting on.</param>
        /// <param name="expression">the member expression to apply</param>
        /// <param name="sortOrder">the sort order to apply</param>
        /// <returns>Query with sorting applied as IOrderedQueryable of type T</returns>
        public static IOrderedQueryable<T> ThenByMember<T>(
        this IOrderedQueryable<T> query,
            Expression<Func<T, object>> expression,
            SortOrder sortOrder)
        {
            var body = expression.Body as UnaryExpression;

            if (body != null)
            {
                var memberExpression = body.Operand as MemberExpression;

                if (memberExpression != null)
                {
                    return
                        (IOrderedQueryable<T>)
                        query.Provider.CreateQuery(
                            Expression.Call(
                                typeof(Queryable),
                                sortOrder == SortOrder.Ascending ? "ThenBy" : "ThenByDescending",
                                new [] { typeof(T), memberExpression.Type },
                                query.Expression,
                                Expression.Lambda(memberExpression, expression.Parameters)));
                }
            }

            return sortOrder == SortOrder.Ascending ? query.ThenBy(expression) : query.ThenByDescending(expression);
        }
    }
}
