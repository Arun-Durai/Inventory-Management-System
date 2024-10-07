using InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.ProductManagement
{
    public class Price
    {
        public double ItemPrice {  get; set; }

        public Currency Currency { get; set; }

        public override string ToString()
        {
            return $"{ItemPrice} {Currency}";
        }

        public Price(double price, Currency currency)
        {
            ItemPrice = price;
            Currency = currency;
        }

        public Price()
        {
        }
    }
}
