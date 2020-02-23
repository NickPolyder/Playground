namespace Playground.StateMachine.States
{
	public interface IStateCommandFactory<TForm, TState, TTrigger>
	{
		IStateCommand<TForm, TTrigger> Create(TState state, TTrigger trigger);
	}
}