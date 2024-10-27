using System;
using Autofac;

namespace Playground.StateMachine.States.Register
{
	public class RegisterFormCommandFactory : IStateCommandFactory<RegisterForm, RegisterStatus, RegisterFormTrigger>
	{
		private readonly IComponentContext _container;

		public RegisterFormCommandFactory(IComponentContext container)
		{
			_container = container;
		}
		public IStateCommand<RegisterForm, RegisterFormTrigger> Create(RegisterStatus state, RegisterFormTrigger trigger)
		{
			switch (state)
			{
				case RegisterStatus.Submitted when trigger == RegisterFormTrigger.InitialSubmission:
					return _container.Resolve<InitialRegisterCommand>();
				case RegisterStatus.PendingEmailConfirmation when trigger == RegisterFormTrigger.EmailConfirmation:
					return _container.Resolve<ConfirmEmailCommand>();
				default:
					throw new ArgumentOutOfRangeException(nameof(state));
			}
		}
	}
}