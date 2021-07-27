using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMidTask2
{
    abstract class Product
    {
        protected int productId;
        protected string productame;
        protected DateTime entryDate;
        protected bool productSold;
        protected bool productAvailable;

        public Product()
        {
            productId = 0;
            productame = "Unknown";
            entryDate = DateTime.Now.Date;
            productSold = false;
            productAvailable = false;
        }
        public int ProductId
        {
            set { productId = value; }
            get { return productId; }
        }
        public string Productame
        {
            set { productame = value; }
            get { return productame; }
        }
        public DateTime EntryDate
        {
            set { entryDate = value; }
            get { return entryDate; }
        }
        public bool ProductSold
        {
            set { productSold = value; }
            get { return productSold; }
        }
        public bool ProductAvailable
        {
            set { productAvailable = value; }
            get { return productAvailable; }
        }
    }
}
