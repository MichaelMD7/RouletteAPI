using System.Collections.Generic;
using TestMasivian.Models;

namespace TestMasivian.Services
{
    public interface IRouletteService 
    {
        public Roulette Create();

        public Roulette Find(string Id);

        public Roulette Open(string Id);
        public Roulette Close(string Id);

        public Roulette Bet(string Id, string UserId, BetRequest betRequest);

        public List<Roulette> GetAll();

        public List<Roulette> ClosingBets(string Id);

        
    }
}