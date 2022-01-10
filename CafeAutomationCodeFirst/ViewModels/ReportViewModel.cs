using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.ViewModels
{
    public class ReportViewModel
    {
        //public int OrderId { get; set; }

        //public int ProductId { get; set; }

        public string TableName { get; set; }

        public string ProductName { get; set; }

        public decimal SubTotal { get; set; }

        //public int TableId { get; set; }

        public bool OrderStatus { get; set; }

        public DateTime DateTime { get; set; }

        public string DateTimeDay { get; set; }

        public string DateTimeHour { get; set; }
    }
}
