using System;
using ComandPattern.models;

namespace ComandPattern
{
    public class Receiver
    {

        private SQLiteController sQLiteController = new SQLiteController();

        public Receiver(){}

        public void SaveMessage(Message message) {
            sQLiteController.AddAMessage(message);
        }

        public void DeleteMessage(int level) {
            
            sQLiteController
                .GetMessagesWithLimit(level)
                .ForEach(sQLiteController.DeleteMessage);


            sQLiteController.PrintMessages();
        }
    }
}
