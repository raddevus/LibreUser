namespace LibreUser.Models;

public class User{
    public int Id{get;set;}
    public int RoleId{get;set;}
    public String Guid{get; set;}
    public String ScreenName{get; set;}
    public String? PwdHash{get;set;}
    public String Email{get; set;}
    public DateTime? Created {get;set;}
    public DateTime? Updated {get;set;}
    public bool Active{get;set;}

    public User(string guid, String pwdHash=null)
    {
        Guid = guid;
        PwdHash = pwdHash;
        Created = DateTime.Now;
        Active = true;
    }

    public User(int id, String guid, int roleId, String screenName, String pwdHash, 
    String email, String created, String updated, bool active)
    {
        Id = id;
        Guid = guid;
        roleId = RoleId;
        ScreenName = screenName;
        PwdHash = pwdHash;
        Email = email;
        Created = created != String.Empty ? DateTime.Parse(created) : null;
        Updated = updated != String.Empty ? DateTime.Parse(updated) : null;
        Active = true;
    }
}