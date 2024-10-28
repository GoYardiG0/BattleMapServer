using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BattleMapServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
namespace BattleMapServer.Controllers
{
    [Route("api")]
    [ApiController]
    public class BattleMapAPIController : ControllerBase
    {
        //a variable to hold a reference to the db context!
        private BattleMapDbContext context;
        //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
        private IWebHostEnvironment webHostEnvironment;
        //Use dependency injection to get the db context and web host into the constructor
        public BattleMapAPIController(BattleMapDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.webHostEnvironment = env;
        }



        // Get api/check
        //This method is only used for testing the server. It tests if the server recognizes a logged in user
        [HttpGet("check")]
        public IActionResult Check()
        {
            try
            {
                return Ok("Works!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

