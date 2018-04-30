using System;
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

    }
}
