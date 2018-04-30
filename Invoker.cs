using System;
namespace ComandPattern
{
    public class Invoker
    {
        private Command _command;

        public void SetCommand(Command aCommand) {
            _command = aCommand;
        }

        public void ExecuteCommand(){
            _command.Execute();
        }
    }

}
