namespace Playground.StateMachine.States
{
    public interface IWorkflow<TForm, TAction>
    {
        void Execute(TAction action, TForm form);
    }
}