using Microsoft.Data.Sqlite;
using LibreUser.Models;

public class SqliteUserProvider : SqliteProvider, IPersistable{

    public User GetOrInsert(){
        try{
            Console.WriteLine("GetOrInsert...");
            connection.Open();
            Console.WriteLine("Opening...");
            using (var reader = Command.ExecuteReader())
            {
                reader.Read();
                var id = reader.GetInt32(0);
                var guid = reader.GetString(1);
                var roleId = reader.GetInt32(2);
                var screenName = "";
                if (!reader.IsDBNull(3)){
                    screenName = reader.GetString(3);
                }
                var pwdHash = "";
                if (!reader.IsDBNull(4)){
                    pwdHash = reader.GetString(4);
                }
                var email = "";
                if (!reader.IsDBNull(5)){
                    email = reader.GetString(5);
                }
                var created = "";
                if (!reader.IsDBNull(6)){
                    created = reader.GetString(6);
                }
                var updated = "";
                if (!reader.IsDBNull(7)){
                    updated = reader.GetString(7);
                }
                var active = reader.GetBoolean(8);

                Console.WriteLine($"GetOrInsert() id: {id}");
                reader.Close();
                return new User(id,guid,roleId,screenName,pwdHash,email,created,updated,active);
            }
        }
        catch(Exception ex){
            Console.WriteLine($"Error: {ex.Message}");
            return new User("0");
        }
        finally{
            if (connection != null){
                connection.Close();
            }
        }
    }
    public Int64 Update(){
        try{
            Console.WriteLine("Updating...");
            connection.Open();
            Console.WriteLine("Opened.");
            // id should be last id inserted into table
            var row_count = Convert.ToInt64(Command.ExecuteNonQuery());
            Console.WriteLine($"row_count : {row_count}");
            Console.WriteLine("attempting update...");
            return row_count;
        }
        catch(Exception ex){
            Console.WriteLine($"Error: {ex.Message}");
            return 0;
        }
        finally{
            if (connection != null){
                connection.Close();
            }
        }
    }
}