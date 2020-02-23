using System.IO.Enumeration;
using Microsoft.Extensions.Logging;

namespace Playground.StateMachine.States.Register
{
	public class InitialRegisterCommand : IStateCommand<RegisterForm, RegisterFormTrigger>
	{
		private readonly ILogger<InitialRegisterCommand> _logger;

		public InitialRegisterCommand(ILogger<InitialRegisterCommand> logger)
		{
			_logger = logger;
		}
		public bool CanExecute(RegisterForm form, RegisterFormTrigger trigger)
		{
			return form.State == RegisterStatus.Submitted
				   && trigger == RegisterFormTrigger.InitialSubmission
				   && !form.IsEmailConfirmed;
		}

		public void Execute(RegisterForm form)
		{
			_logger.LogDebug("Sending E-mail");
			_logger.LogDebug("Saving to Db");
			
		}
	}
}