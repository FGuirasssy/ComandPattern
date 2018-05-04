using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ComandPattern.models;
using System.IO;
using System.Data.SQLite;


namespace ComandPattern
{
    public class DatabaseManager
    {
        private static DatabaseManager instance;

        private MySqlConnection mySqlConnection;

        private static SQLiteConnection sQLiteConnection;

        private const string DATABASE_FILE = "databaseFile.db";

        private const string DATABASE_SOURCE = "data source=" + DATABASE_FILE;


        private DatabaseManager(){}

        public static DatabaseManager GetInstance() {
            if(instance == null) {
                instance = new DatabaseManager();
            }

            return instance;
        }
    
    
        public void CreateTable() {

            var queryText = @"CREATE TABLE IF NOT EXISTS sample
            ( [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            [content] VARCHAR(2048) NULL )";


            if (!File.Exists(DATABASE_FILE))
            {
                SQLiteConnection.CreateFile(DATABASE_FILE);
            }

            try{
                
                sQLiteConnection = new SQLiteConnection(DATABASE_SOURCE);

                sQLiteConnection.Open();
                var deleteCommand = new SQLiteCommand("DROP TABLE IF EXISTS sample", sQLiteConnection);
                deleteCommand.ExecuteNonQuery();
                sQLiteConnection.Close();

            }catch(SQLiteException e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void AddAMessage(Message message) {

            try {

                sQLiteConnection.Open();
                var command = new SQLiteCommand("INSERT INTO sample(content) VALUES (@content)",sQLiteConnection);
                command.Prepare();

                command.Parameters.AddWithValue("@content", message.GetContent());

                command.ExecuteNonQuery();

                //sQLiteConnection.Close();

            }catch(SQLiteException excep) {
                Console.WriteLine(excep.StackTrace);
            }

        }

        public void PrintMessages() {

            try{

                var command = new SQLiteCommand(sQLiteConnection);
                command.CommandText = "SELECT * FROM SAMPLE";
                var reader = command.ExecuteReader();

                while(reader.Read()) {

                    Console.WriteLine("id : {0} \t content : {1}", reader["id"], reader["content"]);
                }

                sQLiteConnection.Close();


            }catch(SQLiteException ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void Connect()
        {
            var connectionString =
            "server=localhost;database=test;uid=toto;pwd=toto";

            try {

                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                Console.WriteLine("Database connection Opened()");
            
            }catch(Exception e){
                
                Console.WriteLine(e.ToString());
            }
        }

        
        public void Close(){
            try
            {
                mySqlConnection.Close();
                Console.WriteLine("Database connection Closed()");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void Insert(Message msg)
        {
            var cmdText = "INSERT INTO sample (message) VALUES (?message)";

            try{
                
                var command = mySqlConnection.CreateCommand();
                command.CommandText = cmdText;
                command.Parameters.AddWithValue("?message", msg.GetContent());
                var result = command.ExecuteNonQuery();

                Console.WriteLine("Save reuslt is {0}\n", result);

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

        }

        public List<Message> getLastInsertID(int limit) {

            var results = new List<Message>();

            var commandText = "SELECT * FROM sample ORDER BY ID DESC LIMIT " + limit;

            try{

                var command = mySqlConnection.CreateCommand();
                command.CommandText = commandText;

                var reader = command.ExecuteReader();

                while(reader.Read()) {
                    
                    var message = new Message();
                    message.SetId(Int32.Parse(reader["id"].ToString()));
                    message.SetContent(reader["message"].ToString());
                    results.Add(message);
                }

                return results;

            }catch(MySqlException e) {
             
                Console.WriteLine(e.ToString());
            }

            Close();
            return null;
        }

        public void UndoSave(Message message) {
            var queryText = "DELETE FROM sample WHERE id=@id";

            try{
            
                Connect();
                var command = mySqlConnection.CreateCommand();
                command.CommandText = queryText;
                command.Parameters.AddWithValue("@id", message.GetId());
                command.ExecuteNonQuery();

            }catch(MySqlException ex){
                Console.WriteLine(ex.ToString());
            }

            Close();
        }

        public List<Message> GetAll()
        {

            var cmdText = "SELECT * FROM sample";
            var models = new List<Message>();

            try
            {
                Connect();

                var command = mySqlConnection.CreateCommand();
                command.CommandText = cmdText;

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var message = new Message();
                    message.SetId(Int32.Parse(reader["id"].ToString()));
                    message.SetContent(reader["message"].ToString());
                    models.Add(message);
                }

                return models;

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}
