using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BattleMapServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Collections.ObjectModel;
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
        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.LoginInfo loginDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Get model user class from DB with matching email. 
                Models.User? modelsUser = context.GetUser(loginDto.UserName);

                //Check if user exist for this email and if password match, if not return Access Denied (Error 403) 
                if (modelsUser == null || modelsUser.UserPassword != loginDto.Password)
                {
                    return Unauthorized();
                }

                //Login suceed! now mark login in session memory!
                HttpContext.Session.SetString("loggedInUser", modelsUser.UserEmail);

                DTO.User dtoUser = new DTO.User(modelsUser);               
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] DTO.User userDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Create model user class
                Models.User modelsUser = userDto.GetModels();

                context.Users.Add(modelsUser);
                context.SaveChanges();

                //User was added!
                DTO.User dtoUser = new DTO.User(modelsUser);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getMonsters")]
        public IActionResult GetMonsters(int userID)
        {
            try
            {
                ObservableCollection<DTO.Monster> dtoMonsters = new ObservableCollection<DTO.Monster>();
                ObservableCollection<Monster> modelMonsters = new ObservableCollection<Monster>( context.Monsters.Where(m => m.UserId == userID || m.UserId == 1).ToList());
                foreach (Monster monster in modelMonsters)
                {
                    dtoMonsters.Add(new DTO.Monster(monster));
                }
                return Ok(dtoMonsters);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCharacters")]
        public IActionResult GetCharacters(int userID)
        {
            try
            {
                ObservableCollection<DTO.Character> dtoCharacters = new ObservableCollection<DTO.Character>();
                ObservableCollection<Character> modelCharacters = new ObservableCollection<Character>(context.Characters.Where(m => m.UserId == userID).ToList());
                foreach (Character character in modelCharacters)
                {
                    dtoCharacters.Add(new DTO.Character(character));
                }
                return Ok(dtoCharacters);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
