using System.IO.Enumeration;
using Microsoft.Extensions.Logging;

namespace Playground.StateMachine.States.Register
{
	public class ConfirmEmailCommand : IStateCommand<RegisterForm, RegisterFormTrigger>
	{
		private readonly ILogger<ConfirmEmailCommand> _logger;

		public ConfirmEmailCommand(ILogger<ConfirmEmailCommand> logger)
		{
			_logger = logger;
		}
		public bool CanExecute(RegisterForm form, RegisterFormTrigger trigger)
		{
			return form.State == RegisterStatus.PendingEmailConfirmation
				   && trigger == RegisterFormTrigger.EmailConfirmation
				   && !form.IsEmailConfirmed;
		}

		public void Execute(RegisterForm form)
		{
			_logger.LogDebug("Confirming E-mail");

			form.IsEmailConfirmed = true;
			
			_logger.LogDebug("Saving to Db");
		}
	}
}