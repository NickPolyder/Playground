using System;
using System.Linq.Expressions;
using Playground.NETFRAMEWORK.Models;

namespace Playground.NETFRAMEWORK.Tests
{
    public class ExpressionTests
    {
        public static void Run()
        {
            Console.WriteLine("Simple Expression");
            Console.WriteLine(_firstExpresion()?.Invoke());
            Console.WriteLine(_secondExpressions()?.Invoke());
            Console.WriteLine(_construct<Blog>().ToString());
            Console.WriteLine(_construct<ClassWithPrivateConstructor>().ToString());

        }

        private static Func<string> _firstExpresion()
        {
            var str = Expression.Constant("Im a simple Expression", typeof(string));

            var func = Expression.Lambda<Func<string>>(str);
            Console.WriteLine($"{func}");
            return func.Compile();
        }

        private static Func<string> _secondExpressions()
        {
            var label = Expression.Parameter(typeof(string), "label");
            var labelStr = Expression.Constant("Label: ");
            var assigned = Expression.Assign(label, labelStr);
            var label2 = Expression.Parameter(typeof(string), "mainStr");
            var labelStr2 = Expression.Constant("Im a simple Expression", typeof(string));
            var assigned2 = Expression.Assign(label2, labelStr2);
            var concat = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });

            var concExp = Expression.Call(concat, label, label2);
            var block = Expression.Block(new[] { label, label2 }, assigned, assigned2, concExp);
            var func = Expression.Lambda<Func<string>>(block);

            Console.WriteLine($"{func}");
            return func.Compile();
        }

        private static T _construct<T>()
        {
            var typeT = typeof(T);
            var newExp = Expression.New(typeT);

            var func = Expression.Lambda<Func<T>>(newExp);
            Console.WriteLine($"{func}");
            return func.Compile().Invoke();
        }

    }
}