using SelXPressApi.Data;
using SelXPressApi.Models;
using System.Linq;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Helper
{
	/// <summary>
	/// Class for seeding initial data into the DataContext.
	/// </summary>
	public class Seed
	{
		private readonly DataContext _context;

		public Seed(DataContext dataContext)
		{
			_context = dataContext;
		}

		/// <summary>
		/// Seeds initial data into the DataContext if users and roles are not present.
		/// </summary>
		public void SeedDataContext()
		{
			if (!_context.Users.Any() && !_context.Roles.Any())
			{
                //Creation of the roles
				Role customerRole = new Role
				{
					Name = "Customer"
				};
				Role sellerRole = new Role
				{
					Name = "Seller"
				};
				Role operatorRole = new Role
				{
					Name = "Operator"
				};
				
				// Creation of the users

				User userCustomer1 = new User
				{
					Username = "LeBirz",
					Email = "ugo.bertrand@epitech.eu",
					Password = "password",
					Role = customerRole
				};

				User userCustomer2 = new User
				{
					Username = "Elsharion",
					Email = "david.vacossin@epitech.eu",
					Password = "password",
					Role = customerRole
				};

				User userSeller = new User
				{
					Username = "Aliak",
					Email = "thomas.debray@epitech.eu",
					Password = "password",
					Role = sellerRole
				};

				User userOperator = new User
				{
					Username = "Maxence_Leroy",
					Email = "maxence.leroy@epitech.eu",
					Password = "password",
					Role = operatorRole
				};

				User userOperator2 = new User
				{
					Username = "Mockingame",
					Email = "julien.lamalle@epitech.eu",
					Password = "password",
					Role = operatorRole
				};
				
				//Creation of the Category
				Category oceanCategory = new Category()
				{
					Name = "Ocean",
				};

				Category sportCategory = new Category()
				{
					Name = "Sport"
				};
				
				//Creation of the tag
				Tag fishingTag = new Tag()
				{
					Category = oceanCategory,
					Name = "Fishing"
				};

				Tag footballTag = new Tag()
				{
					Category = sportCategory,
					Name = "Football"
				};
				
				//Set the tag in a category with the category object
				oceanCategory.Tags = new List<Tag>();
				oceanCategory.Tags.Add(fishingTag);
				sportCategory.Tags = new List<Tag>();
				sportCategory.Tags.Add(footballTag);
				
				//Creation of the attribute data and attribute for the product
				Attribute colorAttribute = new Attribute()
				{
					Name = "color",
					Type = "select",
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				};
                
				AttributeData blueAttributeData = new AttributeData()
				{
					Attribute = colorAttribute,
					Key = "blue",
					Value = "#0000FF"
				};
				AttributeData redAttributeData = new AttributeData()
				{
					Attribute = colorAttribute,
					Key = "red",
					Value = "#FF0000"
				};
				AttributeData greenAttributeData = new AttributeData()
				{
					Attribute = colorAttribute,
					Key = "green",
					Value = "#00FF00"
				};
				
				// set the attribute data in the colorAttriubte
				colorAttribute.AttributeData = new List<AttributeData>();
				colorAttribute.AttributeData.Add(blueAttributeData);
				colorAttribute.AttributeData.Add(redAttributeData);
				colorAttribute.AttributeData.Add(greenAttributeData);
				
				Attribute sizeAttribute = new Attribute()
				{
					Name = "size",
					Type = "select",
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				};

				AttributeData xsAttributeData = new AttributeData()
				{
					Attribute = sizeAttribute,
					Key = "extra_small",
					Value = "xs"
				};
				
				AttributeData sAttributeData = new AttributeData()
				{
					Attribute = sizeAttribute,
					Key = "small",
					Value = "s"
				};
				
				AttributeData mAttributeData = new AttributeData()
				{
					Attribute = sizeAttribute,
					Key = "medium",
					Value = "m"
				};
				
				AttributeData lAttributeData = new AttributeData()
				{
					Attribute = sizeAttribute,
					Key = "large",
					Value = "l"
				};
				
				AttributeData xlAttributeData = new AttributeData()
				{
					Attribute = sizeAttribute,
					Key = "extra_large",
					Value = "xl"
				};
				
				//set the attribute data in the sizeAttribute
				sizeAttribute.AttributeData = new List<AttributeData>();
				sizeAttribute.AttributeData.Add(xsAttributeData);
				sizeAttribute.AttributeData.Add(sAttributeData);
				sizeAttribute.AttributeData.Add(mAttributeData);
				sizeAttribute.AttributeData.Add(lAttributeData);
				sizeAttribute.AttributeData.Add(xlAttributeData);
				
				//Creation of 3 product
				Product product1 = new Product()
				{
					Name = "Mon super produit",
					Price = 150,
					Description = "La description de mon super produit",
					Picture = "https://cfar.org/wp-content/uploads/2020/03/Capture-d%E2%80%99%C3%A9cran-2020-03-22-%C3%A0-14.52.07.png",
					CreatedAt = DateTime.Now,
					Category = oceanCategory,
					Stock = 23,
					Carts = new List<Cart>(),
					OrderProducts = new List<OrderProduct>(),
					ProductAttributes = new List<ProductAttribute>(),
					Comments = new List<Comment>()
				};

				Product product2 = new Product()
				{
					Name = "Mon produit génial",
					Price = 25,
					Description = "La description de mon produit genial",
					Picture =
						"https://st.depositphotos.com/1803622/1397/i/450/depositphotos_13975132-stock-photo-smiley.jpg",
					CreatedAt = DateTime.Now,
					Category = oceanCategory,
					Stock = 50,
					Carts = new List<Cart>(),
					OrderProducts = new List<OrderProduct>(),
					ProductAttributes = new List<ProductAttribute>(),
					Comments = new List<Comment>()
				};

				Product product3 = new Product()
				{
					Name = "Mon produit pas ouf",
					Price = 15,
					Description = "La description de mon produit pas ouf",
					Picture = "https://www.photofunky.net/output/image/a/9/7/8/a97863/photofunky.png",
					CreatedAt = DateTime.Now,
					Category = sportCategory,
					Stock = 89,
					Carts = new List<Cart>(),
					OrderProducts = new List<OrderProduct>(),
					ProductAttributes = new List<ProductAttribute>(),
					Comments = new List<Comment>()
				};
				//Creation of the productAttribute object and set it in the product object
				ProductAttribute productAttribute1 = new ProductAttribute()
				{
					Attribute = colorAttribute,
					Product = product1
				};
				ProductAttribute productAttribute2 = new ProductAttribute()
				{
					Attribute = colorAttribute,
					Product = product2
				};
				ProductAttribute productAttribute3 = new ProductAttribute()
				{
					Attribute = sizeAttribute,
					Product = product3
				};
				
				product1.ProductAttributes.Add(productAttribute1);
				product2.ProductAttributes.Add(productAttribute2);
				product3.ProductAttributes.Add(productAttribute3);
				
				//Creation of 3 comment
				Comment comment1 = new Comment()
				{
					CreatedAt = DateTime.Now,
					Message = "Mon super message pour ce super produit",
					Title = "Titre de mon super commentaire",
					Mark = new Mark()
					{
						rate = 5
					},
					Product = product1,
					User = userCustomer1
				};
				
				Comment comment2 = new Comment()
				{
					CreatedAt = DateTime.Now,
					Message = "Mon message genial pour ce super produit",
					Title = "Titre de mon commentaire genial",
					Mark = new Mark()
					{
						rate = 4
					},
					Product = product1,
					User = userCustomer2
				};
				
				Comment comment3 = new Comment()
				{
					CreatedAt = DateTime.Now,
					Message = "Mon super message pour ce super produit",
					Title = "Titre de mon super commentaire",
					Mark = new Mark()
					{
						rate = 5
					},
					Product = product2,
					User = userCustomer1
				};
				
				Comment comment4 = new Comment()
				{
					CreatedAt = DateTime.Now,
					Message = "Mon message pas ouf pour ce produit pas ouf",
					Title = "Titre de mon commentaire pas ouf",
					Mark = new Mark()
					{
						rate = 1
					},
					Product = product3,
					User = userCustomer2
				};
				
				//Set the comment in the product
				product1.Comments.Add(comment1);
				product1.Comments.Add(comment2);
				product2.Comments.Add(comment3);
				product3.Comments.Add(comment4);
				
                
				_context.Roles.AddRange(customerRole, sellerRole, operatorRole);
				_context.Users.AddRange(userCustomer1, userCustomer2, userSeller, userOperator, userOperator2);
				_context.Categories.AddRange(oceanCategory,sportCategory);
				_context.Tags.AddRange(fishingTag,footballTag);
				_context.Attributes.AddRange(sizeAttribute,colorAttribute);
				_context.AttributesData.AddRange(blueAttributeData,redAttributeData,greenAttributeData,xsAttributeData
					,sAttributeData,mAttributeData,lAttributeData,xlAttributeData);
				_context.Products.AddRange(product1,product2,product3);
				_context.Comments.AddRange(comment1,comment2,comment3,comment4);
				_context.ProductAttributes.AddRange(productAttribute1, productAttribute2, productAttribute3);
				
				_context.SaveChanges();
			}
		}
	}
}
