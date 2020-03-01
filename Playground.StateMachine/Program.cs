using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Playground.StateMachine.States;
using Playground.StateMachine.States.Register;
using Stateless;

namespace Playground.StateMachine
{
    class Program
    {
        private static IServiceProvider _container;
        static void Main(string[] args)
        {
            RegisterServices(args);
            Console.WriteLine("Please include your details: ");
            var form = new RegisterForm
            {
                Username = "Nick", // RequireUserInput("Username"),
                Password = "Pol", // RequireUserInput("Password"),
                State = RegisterStatus.Submitted
            };
            var stateMachine = new StateMachine<RegisterStatus, RegisterCustomAction>(
                () => form.State,
                (state) => form.State = state);

            stateMachine.Configure(RegisterStatus.Submitted)
                .InternalTransition(new RegisterCustomAction(RegisterFormTrigger.Validation, 1), () => Console.WriteLine("Validation: 1"))
                .InternalTransition(new RegisterCustomAction(RegisterFormTrigger.Validation, 2), () => Console.WriteLine("Validation: 2"))
                .InternalTransition(new RegisterCustomAction(RegisterFormTrigger.Validation, 3), () => Console.WriteLine("Validation: 3"));

           stateMachine.Fire(new RegisterCustomAction(RegisterFormTrigger.Validation, 2));
            // OldWorkflow();
            DisposeServices();
        }

        private static void OldWorkflow()
        {
            var workFlow = _container.GetService<IWorkflow<RegisterForm, RegisterFormTrigger>>();

            var stopWatch = Stopwatch.StartNew();
            foreach (var i in Enumerable.Repeat(1, 50000))
            {
                var registerForm = new RegisterForm
                {
                    Username = "Nick", // RequireUserInput("Username"),
                    Password = "Pol", // RequireUserInput("Password"),
                    State = RegisterStatus.Submitted
                };
                workFlow.Execute(RegisterFormTrigger.InitialSubmission, registerForm);
            }

            stopWatch.Stop();
            System.Diagnostics.Debug.WriteLine($"Watch: {stopWatch.ElapsedMilliseconds}ms");
        }

        private static void RegisterServices(string[] args)
        {

            var environment = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");

            var configuration = RegisterConfiguration(environment, args);

            var collection = new ServiceCollection();

            collection.AddSingleton<IConfiguration>(configuration);

            collection.AddLogging(logBuilder =>
                {
                    logBuilder.SetMinimumLevel(configuration.GetValue<LogLevel>("Logging:LogLevel:Default"));
                    logBuilder.AddConsole((opts) =>
                        {
                            opts.IncludeScopes = configuration.GetValue<bool>("Logging:IncludeScopes");
                            opts.LogToStandardErrorThreshold =
                                configuration.GetValue<LogLevel>("Logging:LogLevel:Default");
                        });
                })
                .AddOptions();

            var builder = new ContainerBuilder();

            builder.Populate(collection);

            builder.RegisterType<InitialRegisterCommand>();
            builder.RegisterType<ConfirmEmailCommand>();
            builder.RegisterType<RegisterFormCommandFactory>().AsImplementedInterfaces();
            builder.RegisterType<RegisterFormWorkFlow>().AsImplementedInterfaces();

            _container = new AutofacServiceProvider(builder.Build());
        }

        private static IConfiguration RegisterConfiguration(string environment, string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            return configurationBuilder.Build();
        }

        private static void DisposeServices()
        {
            if (_container == null)
            {
                return;
            }
            if (_container is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }


        private static string RequireUserInput(string label)
        {
            Console.Write(label + ": ");
            return Console.ReadLine();
        }
    }
}
