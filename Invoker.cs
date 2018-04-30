using System;
using ComandPattern.models;

namespace ComandPattern
{
    public class Invoker
    {
        private Command _command;

        public void SetCommand(Command aCommand) {
            _command = aCommand;
        }

        public void ExecuteCommand(Message message){
            _command.ExecuteSaveMsg(message);
        }
    }

}
