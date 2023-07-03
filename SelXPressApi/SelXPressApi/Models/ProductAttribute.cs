using System.Runtime.CompilerServices;

namespace SelXPressApi.Models
{
    /// <summary>
    /// Model of the ProductAttribute table
    /// </summary>
    public class ProductAttribute
    {
        /// <summary>
        /// Id of the ProductAttribute
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product object of the ProductAttribute object
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Id of the Product object of the ProductAttribute object
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Attribute object of the ProductAttribute object
        /// </summary>
        public Attribute Attribute { get; set; }

        /// <summary>
        /// Id of the Attribute object of the ProductAttribute object
        /// </summary>
        public int AttributeId { get; set; }

    }
}
