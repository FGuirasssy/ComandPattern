using System;
using ComandPattern.models;
namespace ComandPattern
{
    public class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver aReceiver): base(aReceiver)
        {
            
        }

        public override void ExecuteSaveMsg(Message message)
		{
            receiver.SaveMessage(message);
        }

		public override void UndoSaveMsg(int level)
		{
            receiver.DeleteMessage(level);
        }

	}
}
