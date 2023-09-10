using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.SellerProductDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository;

/// <summary>
/// Repository class for managing Seller Products.
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
public class SellerProductRepository : ISellerProductRepository
{
    private readonly DataContext _context;
    private readonly CommonMethods _commonMethods;

	/// <summary>
	/// Initializes a new instance of the <see cref="SellerProductRepository"/> class.
	/// </summary>
	/// <param name="context">The data context. <see cref="DataContext"/></param>
	/// <param name="commonMethods">Common methods instance. <see cref="CommonMethods"/></param>
	public SellerProductRepository(DataContext context, CommonMethods commonMethods)
    {
        _context = context;
        _commonMethods = commonMethods;
    }

	/// <summary>
	/// Retrieves a collection of all seller products.
	/// </summary>
	/// <returns>A list of all seller products.</returns>
	public async Task<List<SellerProduct>> GetAllSellerProduct()
    {
        return await _context.SellerProducts.OrderBy(s => s.Id).ToListAsync();
    }

	/// <summary>
	/// Retrieves a seller product by its unique ID.
	/// </summary>
	/// <param name="id">The ID of the seller product to retrieve.</param>
	/// <returns>The seller product with the specified ID.</returns>
	public async Task<SellerProduct> GetSellerProductById(int id)
    {
        return await _context.SellerProducts.Where(s => s.Id == id).FirstAsync();
    }

	/// <summary>
	/// Retrieves a list of seller products associated with a user.
	/// </summary>
	/// <param name="userId">The ID of the user to retrieve seller products for.</param>
	/// <returns>A list of seller products associated with the specified user.</returns>
	public async Task<List<SellerProduct>> GetSellerProductByUser(int userId)
    {
        return await _context.SellerProducts.Where(s => s.UserId == userId).ToListAsync();
    }

	/// <summary>
	/// Retrieves a list of seller products associated with a product.
	/// </summary>
	/// <param name="productId">The ID of the product to retrieve seller products for.</param>
	/// <returns>A list of seller products associated with the specified product.</returns>
	public async Task<List<SellerProduct>> GetSellerProductByProduct(int productId)
    {
        return await _context.SellerProducts.Where(s => s.ProductId == productId).ToListAsync();
    }

	/// <summary>
	/// Creates a new seller product using the provided data.
	/// </summary>
	/// <param name="sellerProduct">Data required to create a new seller product.</param>
	/// <returns>True if the seller product was created successfully; otherwise, false.</returns>
	public async Task<bool> CreateSellerProduct(CreateSellerProductDto sellerProduct)
    {
        var user = await _context.Users.Where(s => s.Id == sellerProduct.UserId).FirstAsync();
        var product = await _context.Products.Where(p => p.Id == sellerProduct.ProductId).FirstAsync();
        SellerProduct newSellerProduct = new SellerProduct()
        {
            ProductId = sellerProduct.ProductId,
            Product = product,
            UserId = sellerProduct.UserId,
            User = user
        };
        _context.SellerProducts.Add(newSellerProduct);
        return await _commonMethods.Save();
    }

	/// <summary>
	/// Updates the product and user associations of a seller product by its unique ID.
	/// </summary>
	/// <param name="sellerProduct">Updated seller product data.</param>
	/// <param name="id">The ID of the seller product to update.</param>
	/// <returns>True if the seller product was updated successfully; otherwise, false.</returns>
	public async Task<bool> UpdateSellerProduct(UpdateSellerProductDto sellerProduct, int id)
    {
        if (await SellerProductExists(id))
            return false;
        SellerProduct sellerProductById = await _context.SellerProducts.Where(s => s.Id == id).FirstAsync();
        if (sellerProduct != null && sellerProduct.ProductId != null)
        {
            await _context.SellerProducts.Where(sp => sp.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.ProductId, x => sellerProduct.ProductId));
        }

        if (sellerProduct != null && sellerProduct.UserId != null)
        {
            await _context.SellerProducts.Where(sp => sp.Id == id)
                .ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.UserId, x => x.UserId));
        }
        return await _commonMethods.Save();
    }

	/// <summary>
	/// Deletes a seller product by its unique ID.
	/// </summary>
	/// <param name="id">The ID of the seller product to delete.</param>
	/// <returns>True if the seller product was deleted successfully; otherwise, false.</returns>
	public async Task<bool> DeleteSellerProduct(int id)
    {
        await _context.SellerProducts.Where(sp => sp.Id == id).ExecuteDeleteAsync();
        return await _commonMethods.Save();
    }

	/// <summary>
	/// Checks if a seller product with the specified ID exists.
	/// </summary>
	/// <param name="id">The ID of the seller product to check.</param>
	/// <returns>True if the seller product exists; otherwise, false.</returns>
	public async Task<bool> SellerProductExists(int id)
    {
        return await _context.SellerProducts.Where(s => s.Id == id).AnyAsync();
    }
}