namespace SelXPressApi.Models;

public class SellerProduct
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Product Product { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int ProductId { get; set; }
    
}