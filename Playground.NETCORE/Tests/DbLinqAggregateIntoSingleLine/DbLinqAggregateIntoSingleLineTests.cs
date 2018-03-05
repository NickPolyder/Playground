using System;
using System.Linq;
using Playground.NETCORE.Models;

namespace Playground.NETCORE.Tests.DbLinqAggregateIntoSingleLine
{
    public class DbLinqAggregateIntoSingleLineTests : ITestCase
    {
        /// <inheritdoc />
        public bool Enabled { get; } = false;

        /// <inheritdoc />
        public string Name { get; } = "Linq: Aggregate Into Single Line (flatten)";

        private DbCoreContext _db;
        /// <inheritdoc />
        public void Run()
        {
            Console.WriteLine("Testing for: https://stackoverflow.com/questions/48726624/linq-aggregate-into-single-line-flatten");

            Logger.Logger.Instance.SetStrategy(Logger.LoggerType.Debug);

            _db = new DbCoreContext();
            ClearDb();
            DbCoreContext.Seed(_db).GetAwaiter().GetResult();

            Console.WriteLine();
            TestCase1();
            Console.WriteLine();
            TestCase2();
            Console.WriteLine();
            TestCase3();
            Console.WriteLine();
            TestCase4();
            Console.WriteLine();

        }

        private void ClearDb()
        {
            _db.Users.RemoveRange(_db.Users);
            _db.SaveChanges();
        }


        public void TestCase1()
        {
            Console.WriteLine($"Starting: {nameof(TestCase1)}");
            var user = User.Create("Pol", "nick@mail.com");
            Console.WriteLine($"Trying to add: {user}");

            AddUser(user);

            Console.WriteLine($"Ending: {nameof(TestCase1)}");

        }

        public void TestCase2()
        {
            Console.WriteLine($"Starting: {nameof(TestCase2)}");
            var user = User.Create("NickPol", "nickpol@mail.com");
            Console.WriteLine($"Trying to add: {user}");

            AddUser(user);

            Console.WriteLine($"Ending: {nameof(TestCase2)}");

        }

        public void TestCase3()
        {
            Console.WriteLine($"Starting: {nameof(TestCase3)}");
            var user = User.Create("User 1", "user1@mail.com");
            Console.WriteLine($"Trying to add: {user}");

            AddUser(user);

            Console.WriteLine($"Ending: {nameof(TestCase3)}");

        }

        public void TestCase4()
        {
            Console.WriteLine($"Starting: {nameof(TestCase4)}");
            var user = User.Create("User 1", "user1@mail.com");
            Console.WriteLine($"Trying to add: {user}");

            AddUser(user);

            Console.WriteLine($"Ending: {nameof(TestCase4)}");

        }

        private void AddUser(User user)
        {
            var outerSelect = (from selection in _db.Users
                               group selection by new
                               {
                                   IsDuplicateEmail = selection.Email == user.Email,
                                   IsDuplicateUsername = selection.Username == user.Username
                               }
                                into grouping
                               where grouping.Key.IsDuplicateEmail || grouping.Key.IsDuplicateUsername
                               select grouping.Key);

            var result = outerSelect.FirstOrDefault() ?? new { IsDuplicateEmail = false, IsDuplicateUsername = false };
            Console.WriteLine($"IsDuplicateEmail: {result.IsDuplicateEmail}, IsDuplicateUsername: {result.IsDuplicateUsername} ");
            Console.WriteLine($"The user: {user} \n");
            if (!result.IsDuplicateEmail && !result.IsDuplicateUsername)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                Console.WriteLine($"Has been inserted succesfully to the database");
            }
            else
            {
                Console.WriteLine($"Is a duplicate.");
            }
        }
    }
}