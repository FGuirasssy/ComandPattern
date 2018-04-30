using System;
namespace ComandPattern
{
    public class Receiver
    {
        public Receiver(){}

        public void Action() {
            Console.WriteLine("Receiver.Action() called");

            var DB = DatabaseManager.GetInstance();
            DB.Connect();
            DB.Close();

        }
    }
}
