using System.Collections.Generic;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ActionManager<T> where T : ICommand
    {
        private Stack<T> actionHistory = new Stack<T>();

        public void ExecuteAction(T action)
        {
            action.Execute();
            actionHistory.Push(action);
        }

        public void UndoAction()
        {
            if (actionHistory.Count > 0)
            {
                T action = actionHistory.Pop();
                action.Undo();
            }
        }
    }
}
