﻿using System;
using System.Collections.Generic;
using SelXPressApi.DTO.CategoryDTO; // Add using directive for CategoryDTO
using SelXPressApi.DTO.ProductAttributeDTO; // Add using directive for ProductAttributeDTO
using SelXPressApi.DTO.CommentDTO; // Add using directive for CommentDTO
using SelXPressApi.DTO.CartDTO; // Add using directive for CartDTO
using SelXPressApi.DTO.OrderDTOProductDTO; // Add using directive for OrderProductDTO
using Newtonsoft.Json;

namespace SelXPressApi.DTO.ProductDTO
{
	/// <summary>
	/// Data Transfer Object for representing Product information. 
	/// Here you can access to model <see cref="Models.Product"/>. 
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
	public class ProductDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL of the product picture.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the available stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the product.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public CategoryDTO.CategoryDTO Category { get; set; }

        /// <summary>
        /// Gets or sets the attributes associated with the product.
        /// </summary>
        public ICollection<ProductAttributeDTO.ProductAttributeDTO> ProductAttributes { get; set; }

        /// <summary>
        /// Gets or sets the comments related to the product.
        /// </summary>
        public ICollection<CommentDTO.CommentDTO> Comments { get; set; }

        /// <summary>
        /// Gets or sets the carts containing the product.
        /// </summary>
        public ICollection<CartDto> Carts { get; set; }

        /// <summary>
        /// Gets or sets the order products containing the product.
        /// </summary>
        public ICollection<OrderProductDTO> OrderProducts { get; set; }
	}
}
