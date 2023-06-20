namespace SelXPressApi.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Product Model
        /// Id is the id of the product PK not null
        /// ProductName is the name of the product varchar(200) not null
        /// ProductDescription is the description of the product varchar(1000) not null
        /// ProductPrice is the price of the product float not null
        /// ProductCategoryId is the id of the category FK references Categories(id) not null
        /// ProductPicture is the picture of the product varchar(500) not null
        /// CreatedAt is the date of creation of the product datetime not null
        /// sellPeopleId is the id of the seller FK references SellPeople(id) not null
        /// </summary>
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
        public string ProductCategoryId { get; set; }
        public string ProductPicture { get; set; }
        public DateTime CreatedAt { get; set; }
        public int sellPeopleId { get; set; }
    }
}
