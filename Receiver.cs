using System;
using ComandPattern.models;

namespace ComandPattern
{
    public class Receiver
    {

        private DatabaseManager databaseManager = DatabaseManager.GetInstance();
        public Receiver(){}

        public void SaveMessage(Message message) {
            Console.WriteLine("Receiver.Action() called");

            databaseManager.Connect();
            databaseManager.Insert(message);
            databaseManager.Close();
               
        }
    }
}
