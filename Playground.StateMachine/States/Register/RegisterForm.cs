namespace Playground.StateMachine.States.Register
{
	public enum RegisterStatus
	{
		Submitted,
		PendingEmailConfirmation,
		Completed,
	}
	public class RegisterForm
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public bool IsEmailConfirmed { get; set; }
		
		public RegisterStatus State { get; set; }
	}
}