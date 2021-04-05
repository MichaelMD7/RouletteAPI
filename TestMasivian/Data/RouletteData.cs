using System;
using System.Collections.Generic;
using System.Linq;
using EasyCaching.Core;
using TestMasivian.Models;
using StackExchange.Redis;
using Newtonsoft.Json;


namespace TestMasivian.Data
{
    public class RouletteData : IRouletteData
    {
        private IEasyCachingProviderFactory cachingProviderFactory;

        private IEasyCachingProvider cachingProvider;

        private const string KEY = "ROULETTE_ID";
        
        public RouletteData(IEasyCachingProviderFactory cachingProviderFactory)
        {
             this.cachingProviderFactory = cachingProviderFactory;
             this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("roulette");          
        }

        public Roulette GetById(string Id)
        {
            var obj = this.cachingProvider.Get<Roulette>(KEY + Id);
            if (!obj.HasValue)
            {
                return null;
            }

            return obj.Value;
        }

        public List<Roulette> GetAll()

        {           
            var output = cachingProvider.GetByPrefix<Roulette>(KEY);
            if (output.Values.Count == 0)
            {
                return new List<Roulette>();
            }

            return new List<Roulette>(output.Select(x => x.Value.Value));
        }
       
    

        public Roulette Update(Roulette roulette)
        {
            roulette.Id = roulette.Id;

            return Save(roulette);
        }

        public Roulette Save(Roulette roulette)
        {
            cachingProvider.Set(KEY + roulette.Id , roulette, TimeSpan.FromDays(365));

            return roulette;
        }
    }
}