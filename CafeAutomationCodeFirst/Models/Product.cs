using CafeAutomationCodeFirst.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.Models
{
    public class Product : BaseEntity, IKey<int>
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public byte[]? ProductPicture { get; set; }

        public int CategoryId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }


        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public override string ToString()
        {
            return ProductName;
        }
    }
}
