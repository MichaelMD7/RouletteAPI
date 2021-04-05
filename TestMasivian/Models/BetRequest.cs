using System;
using System.ComponentModel.DataAnnotations;

namespace TestMasivian.Models
{
    [Serializable]
    public class BetRequest
    {
        
        /// <summary>
        /// position 0-36, and 37=> red, 38 => black
        /// </summary>
        [Range(0, 36)]
        public int Position { get; set; }
        
        /// <summary>
        /// Money to bet
        /// </summary>
        [Range(0.5d, maximum:10000)]

        public double Money { get; set; }

        public string Color { get; set; }
    }
}