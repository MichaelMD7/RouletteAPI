using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestMasivian.Models;
using TestMasivian.Services;

namespace TestMasivian.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouletteController : Controller
    {
        private IRouletteService rouletteService;

        public RouletteController(IRouletteService rouletteService)
        {
            this.rouletteService = rouletteService;
        }
        /// <summary>
        /// Create a  new rulette
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NewRulette()
        {
            Roulette roulette = rouletteService.Create();

            return Ok(roulette);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(rouletteService.GetAll());
        }
        
        /// <summary>
        /// Open the rulette id
        /// </summary>
        /// <param name="id">rulette id</param>
        /// <returns></returns>
        [HttpPut("{id}/open")]
        public IActionResult Open([FromRoute(Name = "id")] string id)
        {
            try
            {
               Roulette roulette = rouletteService.Open(id);

                return Ok(roulette);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return StatusCode(405);
            }
        }
        
        /// <summary>
        /// Closes bets on a rulette
        /// </summary>
        /// <param name="id"> rulette id</param>
        /// <returns></returns>
        [HttpPut("{id}/close")]
        public IActionResult Close([FromRoute(Name = "id")] string id)
        {
            try
            {
                List<Roulette> roulettesWinner = rouletteService.ClosingBets(id);
                return Ok(roulettesWinner);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return StatusCode(405);
            }
        }
        
        /// <summary>
        /// It lets make a bet between [0.5 and 10000, red or black]
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <param name="id"> roulette id</param>
        /// <param name="request">piece number, [0,36] number [red, black] </param>
        /// <returns></returns>
        [HttpPost("{id}/bet")]
        public IActionResult Bet([FromHeader(Name = "user-id")] string UserId, [FromRoute(Name = "id")] string id,
            [FromBody] BetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "Bad Request"
                });
            }

            try
            {
                Roulette roulette = rouletteService.Bet(id, UserId, request);

                return Ok(roulette);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return StatusCode(405);
            }
            
        }
    }
}