using System;
using System.Collections.Generic;
using ComandPattern.models;


namespace ComandPattern
{
    class Program
    {
        static void Main(string[] args)
        {

            Receiver receiver = new Receiver();
            Command command = new ConcreteCommand(receiver);

            Invoker invoker = new Invoker();

            invoker.SetCommand(command);

            var messages = new List<Message>{ new Message("Hello World"),
                new Message("Design Pattern"), new Message("Hit me up"), 
                new Message("You'd really call"),new Message("Hello! how are you?"),
                new Message("It's a pain")};

            messages.ForEach(invoker.ExecuteCommand);

            invoker.UndoCommand(2);

        }
    }
}
