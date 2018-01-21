using System;

namespace Playground.NETCORE.Tests.DispatchProxy
{
    public interface IDispatchInformation
    {
        void Display();
        void DisplayThrowException();
    }

    public class DispatchInformation : IDispatchInformation
    {
        public string InformationType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public void Display()
        {
            var informationType = $"{InformationType}{CreateIntent(20)}";
            int headerLength = informationType.Length;

            Console.WriteLine(informationType);

            var titleLength = Title.Length;

            Console.WriteLine($"{Title}{CreateIntent(headerLength - titleLength)}");
            Console.WriteLine(CreateIntent(headerLength));
            Console.WriteLine($"{Description}");

        }

        public void DisplayThrowException()
        {
            Console.WriteLine($"{InformationType}{CreateIntent(20)}");

            throw new InvalidOperationException(Description);
        }

        private static string CreateIntent(int intentLength)
        {
            return new String('_', intentLength);
        }
    }
}