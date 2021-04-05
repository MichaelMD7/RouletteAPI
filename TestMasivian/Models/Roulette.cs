using System;
using System.Collections.Generic;


namespace TestMasivian.Models
{
    [Serializable]
    public class Roulette
    {

        public string Id { get; set; }

        public bool Open { get; set; } = false;
  
        public virtual List<BetRequest> Bet  { get; set; }

        public virtual BetWinner BetWinner { get; set; }


    }
}