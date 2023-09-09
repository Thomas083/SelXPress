namespace SelXPressApi.DTO.SellerProductDTO;

/// <summary>
/// Represents a data transfer object (DTO) for creating a new Seller Product. 
/// Here you can access to model <see cref="Models.SellerProduct"/>. 
/// </summary>
/// <seealso  cref="Models"/>
/// <seealso  cref="DTO"/>
/// <seealso  cref="Controllers"/>
/// <seealso  cref="Repository"/>
/// <seealso  cref="Helper"/>
/// <seealso  cref="DocumentationErrorTemplate"/>
/// <seealso  cref="Exceptions"/>
/// <seealso  cref="Interfaces"/>
/// <seealso  cref="Middleware"/>
/// <seealso  cref="Data"/>
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