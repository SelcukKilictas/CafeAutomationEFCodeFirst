using CafeAutomationCodeFirst.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.Models
{
    public class Category : BaseEntity, IKey<int>
    {
        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[]? CategoryPicture { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();


        public override string ToString()
        {
            return CategoryName;
        }

    }
}
