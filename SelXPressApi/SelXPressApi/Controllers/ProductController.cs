﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// Crud operations for products
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		/// <summary>
		/// GET: api/<ProductController>
		/// Get all products
		/// </summary>
		/// <returns>Return an Array of all product</returns>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		/// <summary>
		/// GET api/<ProductController>/5
		/// Get a specific product
		/// </summary>
		/// <param name="id"></param>
		/// <returns>information of a specific product</returns>
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "product";
		}

		/// <summary>
		/// POST api/<ProductController>
		/// Create a new product
		/// </summary>
		/// <param name="value"></param>
		[HttpPost("addProduct")]
		public void Post([FromBody] string value)
		{

		}

		/// <summary>
		/// PUT api/<ProductController>/5
		/// Modify a product
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("updateProduct/{id}")]
		public void Put(int id, [FromBody] string value)
		{

		}

		/// <summary>
		/// DELETE api/<ProductController>/5
		/// Delete a product
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("deleteProduct/{id}")]
		public void Delete(int id)
		{
		}
	}
}