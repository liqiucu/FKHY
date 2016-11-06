using FKHY.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKHY.Models.CustomerModels
{
    public class BuyViewModel
    {
        public int OrderId { get; set; }

        public int StudentId { get; set; }

        public int TeacherId { get; set; }

        public decimal RequestCost { get; set; }

        public string PackageType { get; set; }

        public string Title { get; set; }

        public Constants.TradeType TradeType { get; set; }
    }
}
