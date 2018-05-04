using System;
using System.Collections.Generic;
using ComandPattern.models;
using System.IO;
using System.Data.SQLite;


namespace ComandPattern
{
    public class SQLiteController
    {
        private SQLiteConnection sQLiteConnection;

        private const string DATABASE_FILE = "databaseFile.db";
        private const string DATABASE_SOURCE = "data source=" + DATABASE_FILE;


        public SQLiteController(){
            CreateTable();
        }

        private void CreateTable() {
            
            var queryText = @"CREATE TABLE IF NOT EXISTS sample
            ( [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            [content] VARCHAR(2048) NULL )";


            if (!File.Exists(DATABASE_FILE))
            {
                SQLiteConnection.CreateFile(DATABASE_FILE);
            }

            try {
                
                sQLiteConnection = new SQLiteConnection(DATABASE_SOURCE);

                sQLiteConnection.Open();
                var deleteCommand = new SQLiteCommand("DROP TABLE IF EXISTS sample", sQLiteConnection);
                deleteCommand.ExecuteNonQuery();


                var command = new SQLiteCommand(queryText, sQLiteConnection);
                command.ExecuteNonQuery();


                sQLiteConnection.Close();


            }catch(SQLiteException excep) {
                Console.WriteLine(excep.StackTrace);
            }

        }

        public void AddAMessage(Message message)
        {

            try
            {
                
                var command = new SQLiteCommand("INSERT INTO sample(content) VALUES (@content)", sQLiteConnection);
                sQLiteConnection.Open();
                command.Prepare();

                command.Parameters.AddWithValue("@content", message.GetContent());

                command.ExecuteNonQuery();

                sQLiteConnection.Close();

            }
            catch (SQLiteException excep)
            {
                Console.WriteLine(excep.Message);
            }

        }

        public void PrintMessages()
        {

            try
            {
                var command = new SQLiteCommand(sQLiteConnection);
                sQLiteConnection.Open();
                command.CommandText = "SELECT * FROM SAMPLE";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Console.WriteLine("id : {0} \t content : {1}", reader["id"], reader["content"]);
                }

                sQLiteConnection.Close();

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        
        }

        public List<Message> GetMessagesWithLimit(int limit)
        {
            var messages = new List<Message>();

            var query = "SELECT * FROM sample ORDER BY ID DESC LIMIT " + limit;

            try
            {
                var command = new SQLiteCommand(query, sQLiteConnection);
                sQLiteConnection.Open();

                var reader = command.ExecuteReader();

                while(reader.Read()) {

                    var message = new Message();
                    message.SetId(Int32.Parse(reader["id"].ToString()));
                    message.SetContent(reader["content"].ToString());
                    messages.Add(message);
                }

                sQLiteConnection.Close();
                return messages;
            
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public void DeleteMessage(Message message) {

            var queryText = "DELETE FROM sample WHERE id=@id";

            try
            {
                var command = new SQLiteCommand(queryText, sQLiteConnection);
                sQLiteConnection.Open();

                command.Parameters.AddWithValue("@id", message.GetId());
                command.ExecuteNonQuery();

                sQLiteConnection.Close();

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
