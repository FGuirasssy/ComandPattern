using System;
using MySql.Data.MySqlClient;
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
    }
}
