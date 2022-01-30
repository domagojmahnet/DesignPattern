using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;

namespace dmahnet_zadaca_3.LoadData.ChainOfResponsibility
{
    public abstract class Handler
    {
        protected Handler successor;
        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }
        public abstract Event HandleEvent(string[] columns, EventType type);
    }
}
