using System;
namespace DataTier
{
    public class Product
    {
        //Data Members
        protected int productId;
        protected string productname;
        protected string entryDate;
        protected string productSold;
        protected string productAvailable;

        public Product()
        {
            productId = 0;
            productname = "Unknown";
            entryDate = "Unknown";
            productSold = "";
            productAvailable = "";
        }
        //Get & Set Properties
        public int ProductId
        {
            set { productId = value; }
            get { return productId; }
        }
        public string Productame
        {
            set { productname = value; }
            get { return productname; }
        }
        public string EntryDate
        {
            set { entryDate = value; }
            get { return entryDate; }
        }
        public string ProductSold
        {
            set { productSold = value; }
            get { return productSold; }
        }
        public string ProductAvailable
        {
            set { productAvailable = value; }
            get { return productAvailable; }
        }
    }
}
