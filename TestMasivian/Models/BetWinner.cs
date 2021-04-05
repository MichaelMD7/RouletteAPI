using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMasivian.Models
{
    [Serializable]
    public class BetWinner
    {
        public int PositionWinner { get; set; }       
        public string ColorWinner { get; set; }
        public double MoneyForPosition { get; set; }
        public double MoneyForColor { get; set; }
        public double MoneyTotal { get; set; }

    }
}
