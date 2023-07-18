using SelXPressApi.Data;
using SelXPressApi.Models;

namespace SelXPressApi;

public class Seed
{
    private readonly DataContext dataContext;

    public Seed(DataContext context)
    {
        dataContext = context;
    }

    public void GenerateData()
    {
        
    }
    
}