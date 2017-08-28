using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Playground.NETFRAMEWORK.Models;

namespace Playground.NETFRAMEWORK.Tests
{
    public static class SqlQueries
    {

        public static async Task DynamicSqlQuery()
        {
            using (var db = new BlogContext())
            {
                var data = await db.Database.Select<Blog>(db.Blogs.Where(tt => tt.Id != null), "Title,Author");
                data?.ForEach(Console.WriteLine);
            }
        }
    }

    public static class SqlExtensions
    {
        public static async Task<List<object>> Select<T>(this Database db, IQueryable<T> query, string select)
        {

            var selQuery = query.SelectQuery(select);
            Type genericType = Type.GetType("System.ValueTuple`" + selQuery.properties.Count);
            Type[] typeArgs = selQuery.properties.Select(tt => tt.ReturnType).ToArray();
            Type specificType = genericType.MakeGenericType(typeArgs);
            var data = await db.SqlQuery(specificType, selQuery.sql).ToListAsync();
            Console.WriteLine($"{data[0].GetType()}");
            return data;
        }

        private static (string sql, List<(string Name, Type ReturnType)> properties) SelectQuery<T>(this IQueryable<T> query, string select)
        {
            var queryBuilder = new StringBuilder();
            var sql = query.ToString();

            var splitSql = sql.ToUpper().Split(new string[] { "FROM" }, StringSplitOptions.RemoveEmptyEntries);
            var from = splitSql.Length > 1 ? splitSql[1] : "";
            if (string.IsNullOrEmpty(from)) throw new InvalidOperationException("from statement missing.");

            var selectProps = select.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var properties = new List<(string Name, Type ReturnType)>();
            foreach (var item in GetSelectProperty<T>(selectProps))
            {
                if (properties.Contains(item)) continue;
                properties.Add(item);
            }
            int counter = 1;
            queryBuilder.Append("SELECT ");
            queryBuilder.Append(string.Join(", ", properties.Select(tt => tt.Name + $" AS Item{counter++}")));
            queryBuilder.Append(" FROM " + from);

            return (queryBuilder.ToString(), properties);
        }

        private static IEnumerable<(string Name, Type ReturnType)> GetSelectProperty<T>(string[] selectProps)
        {
            foreach (var type in typeof(T).GetProperties())
            {
                var customAttr = type.GetCustomAttributes(true)
                    .FirstOrDefault(tt => tt is ColumnAttribute) as ColumnAttribute;

                if (selectProps.Any(ss => ss.Equals(type.Name, StringComparison.InvariantCultureIgnoreCase) ||
                (customAttr != null && customAttr.Name.Equals(ss, StringComparison.InvariantCultureIgnoreCase))))
                {
                    yield return (customAttr?.Name ?? type.Name, type.PropertyType);
                }
            }
        }
    }
}
