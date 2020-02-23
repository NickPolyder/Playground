namespace Playground.StateMachine.States
{
	public interface IWorkflow<TForm, TState>
	{
		TState State { get; set; }

		void Execute(TForm form);
	}
}