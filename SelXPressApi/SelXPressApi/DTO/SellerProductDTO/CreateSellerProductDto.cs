namespace SelXPressApi.DTO.SellerProductDTO;

public class CreateSellerProductDto
{
    /// <summary>
    /// Id of the product
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Id of the user
    /// </summary>
    public int UserId { get; set; }
}