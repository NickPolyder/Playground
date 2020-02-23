using System;
using System.Collections.Generic;
using Stateless;


namespace Playground.StateMachine.States.Register
{
	public class RegisterFormWorkFlow : BaseFormWorkFlow<RegisterForm, RegisterStatus, RegisterFormTrigger>
	{
		private readonly IStateCommandFactory<RegisterForm, RegisterStatus, RegisterFormTrigger> _factory;
		private Dictionary<RegisterStatus, RegisterFormTrigger> _parameters;

		public RegisterFormWorkFlow(IStateCommandFactory<RegisterForm, RegisterStatus, RegisterFormTrigger> factory)
		{
			_factory = factory;
			_parameters = new Dictionary<RegisterStatus, RegisterFormTrigger>();

			ConfigureStateMachine();
		}

		public override RegisterStatus State
		{
			get => Form.State;
			set => Form.State = value;
		}

		public override void Execute(RegisterForm form)
		{
			base.Execute(form);

			var trigger = _parameters[State];
			if (StateMachine.CanFire(trigger))
			{
				StateMachine.Fire(trigger);
				return;
			}

			throw new ArgumentException("Cannot do the step");
		}

		protected void ConfigureStateMachine()
		{
			ConfigureSubmittedStep();

			ConfigurePendingEmailConfirmation();
		}

		private void ConfigureSubmittedStep()
		{
			var trigger = RegisterFormTrigger.InitialSubmission;

			var submitCommand = _factory.Create(RegisterStatus.Submitted, trigger);

			StateMachine.Configure(RegisterStatus.Submitted)
				.PermitIf(trigger, RegisterStatus.PendingEmailConfirmation,
					() => submitCommand.CanExecute(Form, trigger))
				.OnExit(() => submitCommand.Execute(Form));

			_parameters.Add(RegisterStatus.Submitted, trigger);
		}

		private void ConfigurePendingEmailConfirmation()
		{
			var trigger = RegisterFormTrigger.EmailConfirmation;

			var submitCommand = _factory.Create(RegisterStatus.PendingEmailConfirmation, trigger);

			StateMachine.Configure(RegisterStatus.PendingEmailConfirmation)
				.OnExit(() => submitCommand.Execute(Form))
				.PermitIf(trigger, RegisterStatus.Completed,
					() => submitCommand.CanExecute(Form, trigger));

			_parameters.Add(RegisterStatus.PendingEmailConfirmation, trigger);
		}
	}
}