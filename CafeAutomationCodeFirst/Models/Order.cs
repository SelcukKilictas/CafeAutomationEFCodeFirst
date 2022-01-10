using CafeAutomationCodeFirst.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.Models
{
    public class Order : BaseEntity, IKey<int>
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        //public decimal SubTotal { get => Quantity * Price; }

        public decimal SubTotal { get; set; }

        public bool OrderStatus { get; set; }

        public DateTime DateTime { get; set; }

        public string DateTimeDay { get; set; }

        public string DateTimeHour { get; set; }

        public int TableId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(TableId))]
        public Table Table { get; set; }
        
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public override string ToString()
        {
            return $"{Product.ProductName} - {SubTotal:c2}";
        }

    }
}
