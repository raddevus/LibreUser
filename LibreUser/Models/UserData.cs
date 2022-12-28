namespace LibreUser.Models;

public class UserData{

    private IPersistable dataPersistor;

    private User User;
    public UserData(IPersistable dataPersistor, User user)
    {
        this.dataPersistor = dataPersistor;
        this.User = user;
    }

    public int Configure(){
        if (dataPersistor != null)
        {
            SqliteUserProvider sqliteUserProvider = dataPersistor as SqliteUserProvider;
            
            sqliteUserProvider.Command.CommandText = @"INSERT into User (guid)values($guid)";
            sqliteUserProvider.Command.Parameters.AddWithValue("$guid",User.Guid);
            return 0;
        }
        return 1;
    }

    public int ConfigureInsert(){
        if (dataPersistor != null)
        {
            SqliteUserProvider sqliteUserProvider = dataPersistor as SqliteUserProvider;
            String sqlCommand = @"insert into User (guid)  
                    select $guid
                    where not exists 
                    (select guid from User where guid=$guid);
                     select * from User where guid=$guid and active=1";
            
            sqliteUserProvider.Command.CommandText = sqlCommand;
            sqliteUserProvider.Command.Parameters.AddWithValue("$guid",User.Guid);
            return 0;
            
        }
        return 2;
    }

    public int ConfigureSelect(){
        SqliteUserProvider sqliteUserProvider = dataPersistor as SqliteUserProvider;
        String sqlCommand = @"select id from User
                where guid = $guid and active=1";
        
        sqliteUserProvider.Command.CommandText = sqlCommand;
        sqliteUserProvider.Command.Parameters.AddWithValue("$guid",User.Guid);
        return 0;
    }

    public int ConfigureNameInsert(){
        SqliteUserProvider sqliteUserProvider = dataPersistor as SqliteUserProvider;
        String sqlCommand = @"update User set ScreenName = $screenName where guid=$guid";
        
        sqliteUserProvider.Command.CommandText = sqlCommand;
        sqliteUserProvider.Command.Parameters.AddWithValue("$screenName", User.ScreenName);
        sqliteUserProvider.Command.Parameters.AddWithValue("$guid",User.Guid);
        return 0;
    }
}