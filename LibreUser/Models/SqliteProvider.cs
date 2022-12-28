using Microsoft.Data.Sqlite;
using LibreUser.Models;
public class SqliteProvider : IPersistable{

    public SqliteCommand Command{
        get{return this.command;}
        set{this.command = value;}
    }

    protected SqliteConnection connection;
    private SqliteCommand command;
    
    private String [] allTableCreation = {
                @"CREATE TABLE IF NOT EXISTS User
                ( [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                [GUID] NVARCHAR(36)  NOT NULL UNIQUE check(length(GUID) <= 36),
                [RoleID] INTEGER NOT NULL default 0,
                [ScreenName] NVARCHAR (100) check(length(ScreenName) <= 100),
                [PwdHash] NVARCHAR (128) check(length(PwdHash) <= 128),
                [Email] NVARCHAR (200) check(length(Email) <= 200),
                [Created] NVARCHAR(30) default (datetime('now','localtime')) check(length(Created) <= 30),
                [Updated] NVARCHAR(30) check(length(Updated) <= 30),
                [Active] BOOLEAN default (1)
                )",
        };

    public SqliteProvider()
    {
        try{
                connection = new SqliteConnection("Data Source=libreuser.db");
                // ########### FYI THE DB is created when it is OPENED ########
                connection.Open();
                command = connection.CreateCommand();
                FileInfo fi = new FileInfo("libreuser.db");
                if (fi.Length == 0){
                    foreach (String tableCreate in allTableCreation){
                        command.CommandText = tableCreate;
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine(connection.DataSource);
        }
        finally{
            if (connection != null){
                connection.Close();
            }
        }
    }
    // public int GetOrInsert(){
    //     try{
    //         Console.WriteLine("GetOrInsert...");
    //         connection.Open();
    //         Console.WriteLine("Opening...");
    //         using (var reader = command.ExecuteReader())
    //         {
    //             reader.Read();
    //             var id = reader.GetInt32(0);
    //             Console.WriteLine($"GetOrInsert() id: {id}");
    //             reader.Close();
    //             return id;
    //         }
    //     }
    //     catch(Exception ex){
    //         Console.WriteLine($"Error: {ex.Message}");
    //         return 0;
    //     }
    //     finally{
    //         if (connection != null){
    //             connection.Close();
    //         }
    //     }
    // }

    // public Bucket GetBucket(){
    //     try{
    //         Console.WriteLine("GetBucket...");
    //         connection.Open();
    //         Console.WriteLine("Opening...");
    //         using (var reader = command.ExecuteReader())
    //         {
    //             reader.Read();
    //             var id = reader.GetInt64(0);
    //             var mainTokenId = reader.GetInt64(1);
    //             var data = reader.GetString(2);
    //             var created = "";
    //             if (!reader.IsDBNull(3)){
    //                 created = reader.GetString(3);
    //             }
    //             var updated = "";
    //             if (!reader.IsDBNull(4)){
    //                 updated = reader.GetString(4);
    //             }
    //             var active = reader.GetBoolean(5);
    //             Bucket b = new Bucket(id,mainTokenId,data,created,updated,active);
    //             Console.WriteLine($"GetBucket() id: {b.Id}");
    //             reader.Close();
    //             return b;
    //         }
    //     }
    //     catch(Exception ex){
    //         Console.WriteLine($"Error: {ex.Message}");
    //         return new Bucket(0,0);
    //     }
    //     finally{
    //         if (connection != null){
    //             connection.Close();
    //         }
    //     }
    // }
    
    public Int64 Save(){
        
        try{
            Console.WriteLine("Saving...");
            connection.Open();
            Console.WriteLine("Opened.");
            // id should be last id inserted into table
            var id = Convert.ToInt64(command.ExecuteScalar());
            Console.WriteLine("inserted.");
            return id;
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