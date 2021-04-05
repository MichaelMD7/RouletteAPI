using System.Collections.Generic;
using TestMasivian.Models;

namespace TestMasivian.Data
{
    public interface IRouletteData 
    {
        public Roulette GetById(string Id);

        public List<Roulette> GetAll();
        
        public Roulette Update(Roulette roulette);
        
        public Roulette Save(Roulette roulette);
    }
}