using System;
using System.Collections.Generic;
using Stateless;


namespace Playground.StateMachine.States.Register
{
    public interface IRegisterFormWorkFlow : IWorkflow<RegisterForm, RegisterStatus>
    {
        void InitialSubmission(RegisterForm form);
        void EmailConfirmation(RegisterForm form);
    }
    public class RegisterFormWorkFlow : BaseFormWorkFlow<RegisterForm, RegisterStatus, RegisterFormTrigger>
    {
        private readonly IStateCommandFactory<RegisterForm, RegisterStatus, RegisterFormTrigger> _factory;

        public RegisterFormWorkFlow(IStateCommandFactory<RegisterForm, RegisterStatus, RegisterFormTrigger> factory)
        {
            _factory = factory;

        }

        /// <inheritdoc />
        protected override StateMachine<RegisterStatus, RegisterFormTrigger> CreateStateMachineFor(RegisterForm form)
        {
            var stateMachine = new StateMachine<RegisterStatus, RegisterFormTrigger>(() => form.State, (state) => form.State = state);
            
            ConfigureSubmittedStep(stateMachine, form);

            ConfigurePendingEmailConfirmation(stateMachine, form);

            return stateMachine;
        }


        private void ConfigureSubmittedStep(StateMachine<RegisterStatus, RegisterFormTrigger> stateMachine, RegisterForm form)
        {
            var trigger = RegisterFormTrigger.InitialSubmission;

            var submitCommand = _factory.Create(RegisterStatus.Submitted, trigger);

            stateMachine.Configure(RegisterStatus.Submitted)
                .PermitIf(trigger, RegisterStatus.PendingEmailConfirmation,
                    () => submitCommand.CanExecute(form, trigger))
                .OnExit(() => submitCommand.Execute(form));


        }

        private void ConfigurePendingEmailConfirmation(StateMachine<RegisterStatus, RegisterFormTrigger> stateMachine, RegisterForm form)
        {
            var trigger = RegisterFormTrigger.EmailConfirmation;

            var submitCommand = _factory.Create(RegisterStatus.PendingEmailConfirmation, trigger);

            stateMachine.Configure(RegisterStatus.PendingEmailConfirmation)
                .OnExit(() => submitCommand.Execute(form))
                .PermitIf(trigger, RegisterStatus.Completed,
                    () => submitCommand.CanExecute(form, trigger));

        }
    }
}