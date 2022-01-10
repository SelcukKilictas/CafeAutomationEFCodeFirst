using CafeAutomationCodeFirst.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.Models
{
    public class Floor : BaseEntity, IKey<int>
    {
        public int Id { get; set; }

        public string FloorName { get; set; }

        public int FloorOrder { get; set; }

        public string Word { get; set; }

        public int TableCount { get; set; }

        public ICollection<Table> Tables { get; set; } = new HashSet<Table>();


        public override string ToString()
        {
            return $"{FloorName} - {FloorOrder}";
        }
    }
}
