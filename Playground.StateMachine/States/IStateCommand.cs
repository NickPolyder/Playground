namespace Playground.StateMachine.States
{
	public interface IStateCommand<TForm, TTrigger>
	{
		bool CanExecute(TForm form, TTrigger trigger);

		void Execute(TForm form);

	}
}