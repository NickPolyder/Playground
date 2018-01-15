using System;
using System.Collections.Generic;
using System.Text;

namespace Playground.NETCORE.Tests.Recursion
{
    public class RecursiveExceptionTests : ITestCase
    {
        /// <inheritdoc />
        public bool Enabled { get; } = false;

        /// <inheritdoc />
        public string Name { get; } = "Recursive Exception Reading";

        /// <inheritdoc />
        public void Run()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Start Recursion Test.")
                   .AppendLine();
            var exception = new SystemException("This is a Message",
                new InvalidOperationException("I dont know ths exception",
                    new FieldAccessException("I Cant Access your Field",
                        new Exception("I Dont have time to check for obseletes"))));

            Recursive(builder, exception);

            Console.WriteLine(builder.ToString());
        }

        private void Recursive(StringBuilder builder, Exception ex)
        {
            builder.AppendLine($"Ex Type {ex.GetType().Name}, Message: {ex.Message}");

            if (ex.InnerException != null)
            {
                builder.AppendLine($"\tInner Exception Stack: ");
                builder.Append("\t\t");
                Recursive(builder, ex.InnerException);
            }

        }
    }
}
