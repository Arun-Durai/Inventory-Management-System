using InventoryManagement.Domain.General;
using InventoryManagement.Domain.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string description;

        private int maxItemsInStock = 0;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value.Length > 50 ? value.Substring(0, 50) : value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value == null) 
                { 
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value.Substring(0, 250) : value;
                }
                
            }
        }
         
        public UnitType UnitType { get; set; }
        public int AmountInStock { get; private set; }  
        public bool IsBelowStockThreshold { get; private set; }
        public Price Price { get; set; }

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, string description, Price price, UnitType unitType,
            int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            UnitType = unitType;

            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }

        public Product(int v)
        {
            Id = v;
        }

        public void UseProduct(int items)
        {
            if(items <= AmountInStock)
            {
                AmountInStock -= items;
                UpdateLowStock();

                Console.WriteLine($"Amount in stock Updated. Now {AmountInStock} items in Stock.");
            }
            else
            {
                Console.WriteLine($"Not Enough items on stock for {CreateSimpleProductRepresentation()}." +
                    $"{AmountInStock} available but {items} requested");
            }
        }

        public void IncreasStock()
        {
            AmountInStock++;
        }


        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product: {id} ({name})";
        }

        private void decreasStock(int items, string reason)
        {
            if(items <= AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock() ;
            Log(reason);
        }


        public string DisplayDetailsShort()
        {
            return $"{id} . { name} \n{ AmountInStock } maxIteminStock in stock";
        }


        public string DisplayDetailsFull()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{id} {name} \n{description}\n{Price}\n{AmountInStock} item(s) in stock");

            sb.Append("/n !! low stock !!");

            return sb.ToString();
            //return DisplayDetailsFull(""); Alternative
        }

        public string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{id} {name} \n{description}\n{Price}\n{AmountInStock} item(s) in stock");

            sb.Append(extraDetails);

            if(IsBelowStockThreshold)
            {
                sb.Append("\n !! STOCK LOW !!");
            }

            return sb.ToString();
        }
    }
}
