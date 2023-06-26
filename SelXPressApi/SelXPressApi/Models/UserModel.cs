namespace SelXPressApi.Models;

public class UserModel 
{
    /// <summary>
    /// Id of the User (int)
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Username of the user (varchar(150), not null)
    /// </summary>
    public string username { get; set; }
    
    /// <summary>
    /// Firstname of the user (varchar(100))
    /// </summary>
    public string firstname { get; set; }
    
    /// <summary>
    /// Lastname of the user (varchar(100))
    /// </summary>
    public string lastname { get; set; }
    
    /// <summary>
    /// Crypted password of the user (varchar(200), not null)
    /// </summary>
    public string password { get; set; }
    
    /// <summary>
    /// Email of the user (varchar(200), not null)
    /// </summary>
    public string email { get; set; }
    
    /// <summary>
    /// Id of the role of the user (int, References Roles(id))
    /// </summary>
    public int roleId { get; set; }
    
}