using System;
using ComandPattern.models;
namespace ComandPattern
{
    public abstract class Command
    {
        protected Receiver receiver;

        public Command(Receiver aReceiver){
            receiver = aReceiver;
        }

        public abstract void ExecuteSaveMsg(Message message);
    }
}
