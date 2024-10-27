using System;
using System.Collections.Generic;
using System.Text;

namespace Playground.NETCORE.Tests.Span
{
    struct MutableStruct { public int Value { get; set; } }
    public class SpanTests : ITestCase
    {
        /// <inheritdoc />
        public bool Enabled { get; } = false;

        /// <inheritdoc />
        public string Name { get; } = "Spans";

        /// <inheritdoc />
        public void Run()
        {
            Span<MutableStruct> spanOfStructs = new MutableStruct[1];
            spanOfStructs[0].Value = 42;

        }
    }
}
