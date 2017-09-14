using System.Text;

namespace Playground.NETCORE.Tests.DI
{
    public class KindOfString
    {
        StringBuilder _builder;

        public KindOfString(StringBuilder builder)
        {
            _builder = builder;
        }

        public void Write(string message)
        {
            _builder.Append(message);
        }
    }
}
