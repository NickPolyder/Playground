using Playground.StateMachine.States.Register;
using Stateless;

namespace Playground.StateMachine.States
{
	public abstract class BaseFormWorkFlow<TForm, TState, TTrigger> : IWorkflow<TForm, TState>
	{
		protected TForm Form { get; set; }
		public abstract TState State { get; set; }
	
		protected StateMachine<TState, TTrigger> StateMachine { get; }

		protected BaseFormWorkFlow()
		{
			StateMachine = CreateStateMachine();
		}

		protected virtual StateMachine<TState, TTrigger> CreateStateMachine()
		{

			return new StateMachine<TState, TTrigger>(() => State, (state) => State = state);
		}
		
		public virtual void Execute(TForm form)
		{
			Form = form;
		}
	}
}