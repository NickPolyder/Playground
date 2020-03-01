using System;

namespace Playground.StateMachine.States.Register
{
	[Flags]
    public enum IdentityStatus:byte
    {
        Started = 0b0000,
		InProgress = 0b0001,
		ProofOfIdentity = 0b0010,
		Address = 0b0100

	}
	// 0b 1111
	// var status = IdentityStatus.InProgress | IdentityStatus.ProofOfIdentity | Address | ContactDetails 
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