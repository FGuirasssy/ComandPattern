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

        public void DeleteMessage(int level) {

            databaseManager.Connect();
            var messages = databaseManager.getLastInsertID(level);

            messages.ForEach (delegate(Message message) {
                //Console.WriteLine(" id : {0} \t content : {1}\n", message.GetId(), message.GetContent());
                databaseManager.UndoSave(message);
            });

            databaseManager.Close();

        }
    }
}
