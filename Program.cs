using System;
using ComandPattern.models;

namespace ComandPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Receiver receiver = new Receiver();
            Command command = new ConcreteCommand(receiver);

            Invoker invoker = new Invoker();

            invoker.SetCommand(command);
            invoker.ExecuteCommand(new Message("Hello World"));

        }
    }
}
