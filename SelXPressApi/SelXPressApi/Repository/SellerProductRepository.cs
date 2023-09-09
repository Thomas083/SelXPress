using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.SellerProductDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository;

public class SellerProductRepository : ISellerProductRepository
{
    private readonly DataContext _context;
    private readonly CommonMethods _commonMethods;

    public SellerProductRepository(DataContext context, CommonMethods commonMethods)
    {
        _context = context;
        _commonMethods = commonMethods;
    }
    
    public async Task<List<SellerProduct>> GetAllSellerProduct()
    {
        return await _context.SellerProducts.OrderBy(s => s.Id).ToListAsync();
    }

    public async Task<SellerProduct> GetSellerProductById(int id)
    {
        return await _context.SellerProducts.Where(s => s.Id == id).FirstAsync();
    }

    public async Task<List<SellerProduct>> GetSellerProductByUser(int userId)
    {
        return await _context.SellerProducts.Where(s => s.UserId == userId).ToListAsync();
    }

    public async Task<List<SellerProduct>> GetSellerProductByProduct(int productId)
    {
        return await _context.SellerProducts.Where(s => s.ProductId == productId).ToListAsync();
    }

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

    public async Task<bool> DeleteSellerProduct(int id)
    {
        await _context.SellerProducts.Where(sp => sp.Id == id).ExecuteDeleteAsync();
        return await _commonMethods.Save();
    }

    public async Task<bool> SellerProductExists(int id)
    {
        return await _context.SellerProducts.Where(s => s.Id == id).AnyAsync();
    }
}