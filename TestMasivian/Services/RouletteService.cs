using System;
using System.Collections.Generic;
using TestMasivian.Models;
using TestMasivian.Data;
using System.Linq;

namespace TestMasivian.Services
{
    public class RouletteService : IRouletteService
    {
        private IRouletteData rouletteData;

        public RouletteService(IRouletteData rouletteData)
        {
            this.rouletteData = rouletteData;
        }

        public Roulette Create()
        {
            Roulette roulette = new Roulette()
            {
                Id = Guid.NewGuid().ToString(),
                Open = false
            };
            rouletteData.Save(roulette);

            return roulette;
        }

        public Roulette Find(string Id)
        {
            return rouletteData.GetById(Id);
        }

        public Roulette Open(string Id)
        {
            Roulette roulette = rouletteData.GetById(Id);
            if (roulette == null)
            {
                throw new Exception();
            }
            roulette.Open = true;

            return rouletteData.Update(roulette);
        }

        public Roulette Close(string Id)
        {
            Roulette roulette = rouletteData.GetById(Id);
            if (roulette == null)
            {
                throw new Exception();
            }
            roulette.Open = false;

            return rouletteData.Update(roulette);
        }

        public Roulette Bet(string Id, string UserId, BetRequest betRequest)
        {
            Roulette roulette = rouletteData.GetById(Id);
            if (roulette == null || roulette.Open == false)
            {
                throw new Exception();
            }
           if (roulette.Bet == null)
            {
                roulette.Bet = new List<BetRequest>();
                roulette.Bet.Add(betRequest);
            }else
            {
                roulette.Bet.Add(betRequest);
            }
                                        
            return rouletteData.Update(roulette);
        }

        public List<Roulette> GetAll()
        {
            return rouletteData.GetAll();
        }

        public List<Roulette> ClosingBets(string id)
        {            
            var item = rouletteData.GetById(id);         
                if (item.Open)
                {
                    item.Open = false;                   
                    rouletteData.Save(item);                    
                }


            return WinningRoulette(item);
        }

        private List<Roulette> WinningRoulette(Roulette roulettes)
        {
            Random random = new Random();
            int numberWinner = 4;//random.Next(0, 36);
            string colorWinner = Color(numberWinner);
            List<Roulette> RouletteWinners = new List<Roulette>();
            double colorMoney = 0;
            double positionMoney = 0;
            foreach (BetRequest item in roulettes.Bet)
            {                
                roulettes.BetWinner = new BetWinner();
                   if (item.Color.ToLower() == colorWinner)
                 {
                    colorMoney = colorMoney + CalculateMoney(item.Money, true);                   
                 }
                   if (item.Position == numberWinner)
                 {
                    positionMoney = positionMoney + CalculateMoney(item.Money, false);
                 }                  
            }
            roulettes.BetWinner.PositionWinner = numberWinner;
            roulettes.BetWinner.ColorWinner = colorWinner;
            roulettes.BetWinner.MoneyForColor = colorMoney;
            roulettes.BetWinner.MoneyForPosition = positionMoney;
            roulettes.BetWinner.MoneyTotal = colorMoney + positionMoney;
            RouletteWinners.Add(roulettes);
            
            return RouletteWinners;
        }
                       

        private  double CalculateMoney(double money, bool color)
            {          
                double
                    moneyForPosition = money * 5,
                    moneyForColor = money * 1.8;
            if (color)
                return moneyForColor;
            else
                return moneyForPosition;
        }

        private string Color(int number)
        {
            return (number % 2 == 0 ? "red" : "black");
        }
    }
}