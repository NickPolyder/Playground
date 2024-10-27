using Playground.StateMachine.States.Register;
using Stateless;

namespace Playground.StateMachine.States
{
    public abstract class BaseFormWorkFlow<TForm, TState, TTrigger> : IWorkflow<TForm, TTrigger>
    {
        protected abstract StateMachine<TState, TTrigger> CreateStateMachineFor(TForm form);
        public void Execute(TTrigger action, TForm form)
        {
            var stateMachine = CreateStateMachineFor(form);
            
            if (stateMachine.CanFire(action))
            {
                stateMachine.Fire(action);
            }
        }
    }
}