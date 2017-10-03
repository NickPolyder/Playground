using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Playground.NETCORE.Tests.Expressions
{
    public class ExpressionTests : ITestCase
    {
        public bool Enabled { get; } = true;
        public string Name { get; } = "Expressions Testing";
        public void Run()
        {
            Console.WriteLine("Simple Expression");
            Console.WriteLine(_firstExpresion()?.Invoke());
            Console.WriteLine(_secondExpressions()?.Invoke());
        }

        private Func<string> _firstExpresion()
        {
            var str = Expression.Constant("Im a simple Expression", typeof(string));

            var func = Expression.Lambda<Func<string>>(str);
            Console.WriteLine($"{func}");
            return func.Compile();
        }

        private Func<string> _secondExpressions()
        {
            var label = Expression.Parameter(typeof(string), "label");
            var labelStr = Expression.Constant("Label: ");
            var assigned = Expression.Assign(label, labelStr);
            var label2 = Expression.Parameter(typeof(string), "mainStr");
            var labelStr2 = Expression.Constant("Im a simple Expression", typeof(string));
            var assigned2 = Expression.Assign(label2, labelStr2);
            var concat = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });

            var concExp = Expression.Call(concat, label, label2);
            var block = Expression.Block(new[] { label,label2 }, assigned,assigned2,concExp);
            var func = Expression.Lambda<Func<string>>(block);

            Console.WriteLine($"{func}");
            return func.Compile();
        }

    }
}
