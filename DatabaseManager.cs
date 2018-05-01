using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ComandPattern.models;


namespace ComandPattern
{
    public class DatabaseManager
    {
        private static DatabaseManager instance;

        private MySqlConnection mySqlConnection;

        private DatabaseManager(){}

        public static DatabaseManager GetInstance() {
            if(instance == null) {
                instance = new DatabaseManager();
            }
            return instance;
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
