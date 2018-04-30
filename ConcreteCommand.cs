using System;
namespace ComandPattern
{
    public class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver aReceiver): base(aReceiver)
        {
            
        }

		public override void Execute()
		{
            receiver.Action();
        }

	}
}
