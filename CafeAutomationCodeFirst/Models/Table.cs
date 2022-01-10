using CafeAutomationCodeFirst.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.Models
{
    public class Table : BaseEntity, IKey<int>
    {
        public int Id { get; set; }

        public string TableName { get; set; }

        public int TableOrder { get; set; }

        public bool TableStatus { get; set; }

        public int FloorId { get; set; }

        [ForeignKey(nameof(FloorId))]
        public Floor Floor { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
