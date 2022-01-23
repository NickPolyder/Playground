using System;
using System.Linq.Expressions;
using Playground.NETSTANDARD.Models;

namespace Playground.NETSTANDARD.Tests.Expressions
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
            Console.WriteLine(_construct<Contact>().ToString());
            Console.WriteLine(_construct<ClassWithPrivateConstructor>().ToString());
            Console.WriteLine(_constructWithParams("Nick Pol", "6954444").ToString());

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
            var block = Expression.Block(new[] { label, label2 }, assigned, assigned2, concExp);
            var func = Expression.Lambda<Func<string>>(block);

            Console.WriteLine($"{func}");
            return func.Compile();
        }

        private T _construct<T>()
        {
            var typeT = typeof(T);
            var newExp = Expression.New(typeT);

            var func = Expression.Lambda<Func<T>>(newExp);
            Console.WriteLine($"{func}");
            return func.Compile().Invoke();
        }

        private Contact _constructWithParams(string fullname, string phone)
        {
            var typeT = typeof(Contact);
            var newExp = Expression.New(typeT);
            var fullNameProp = typeT.GetProperty(nameof(Contact.FullName));
            var phoneProp = typeT.GetProperty(nameof(Contact.Phone));
            var memberInit = Expression.MemberInit(newExp,
                Expression.Bind(fullNameProp, Expression.Constant(fullname)),
                Expression.Bind(phoneProp, Expression.Constant(phone)));
            var func = Expression.Lambda<Func<Contact>>(memberInit);
            Console.WriteLine($"{func}");
            return func.Compile().Invoke();

        }

    }
}
