using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BattleMapServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
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

        #region registers
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


        [HttpPost("AddMonster")]
        public IActionResult AddMonster([FromBody] DTO.Monster monsterDto)
        {
            try
            {
                Models.Monster modelsMonster = monsterDto.GetModels();
                List<Monster> modelMonsters = new List<Monster>(context.Monsters.Where(m => m.UserId == monsterDto.UserId).ToList());
                int nameCount = 0;
                foreach (Monster m in modelMonsters)
                {
                    if (m.MonsterName.Contains(monsterDto.MonsterName))
                        nameCount++;
                }
                if (nameCount > 0)
                    modelsMonster.MonsterName += $"({nameCount})";

                context.Monsters.Add(modelsMonster);
                context.SaveChanges();

                //User was added!
                DTO.Monster dtoMonster = new DTO.Monster(modelsMonster);
                return Ok(dtoMonster);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("AddCharacter")]
        public IActionResult AddCharacter([FromBody] DTO.Character characterDto)
        {
            try
            {
                Models.Character modelsCharacter = characterDto.GetModels();

                List<Character> modelCharacters = new List<Character>(context.Characters.Where(c => c.UserId == characterDto.UserId).ToList());
                int nameCount = 0;
                foreach (Character c in modelCharacters)
                {
                    if (c.CharacterName.Contains(characterDto.CharacterName))
                        nameCount++;
                }
                if (nameCount > 0)
                    modelsCharacter.CharacterName += $"({nameCount})";

                context.Characters.Add(modelsCharacter);
                context.SaveChanges();

                //User was added!
                DTO.Character dtoCharacter = new DTO.Character(modelsCharacter);
                //dtoCharacter.CharacterPic = GetProfileImageVirtualPath(dtoCharacter.CharacterId);
                return Ok(dtoCharacter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        #endregion

        #region updaters

        [HttpPost("updateMonster")]
        public IActionResult UpdateMonster([FromBody] DTO.Monster monsterDto)
        {

            try
            {
                Monster? monster = context.Monsters.Where(m => m.MonsterId == monsterDto.MonsterId).FirstOrDefault();
                monster.ReSetMonster(monsterDto);
                context.SaveChanges();
                return Ok(monster);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("updateCharacter")]
        public IActionResult UpdateCharacter([FromBody] DTO.Character characterDto)
        {

            try
            {
                Character? character = context.Characters.Where(m => m.CharacterId == characterDto.CharacterId).FirstOrDefault();
                character.ReSetCharacter(characterDto);
                context.SaveChanges();
                return Ok(character);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Get things
        [HttpGet("getMonsters")]
        public IActionResult GetMonsters()
        {
            try
            {
                ObservableCollection<DTO.Monster> dtoMonsters = new ObservableCollection<DTO.Monster>();
                ObservableCollection<Monster> modelMonsters = new ObservableCollection<Monster>(context.Monsters.ToList());
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
        public IActionResult GetCharacters()
        {
            try
            {
                ObservableCollection<DTO.Character> dtoCharacters = new ObservableCollection<DTO.Character>();
                ObservableCollection<Character> modelCharacters = new ObservableCollection<Character>(context.Characters.ToList());
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
        #endregion
        //Helper functions
        #region Backup / Restore
        [HttpGet("Backup")]
        public async Task<IActionResult> Backup()
        {
            string path = $"{this.webHostEnvironment.WebRootPath}\\..\\DBScripts\\backup.bak";

            bool success = await BackupDatabaseAsync(path);
            if (success)
            {
                return Ok("Backup was successful");
            }
            else
            {
                return BadRequest("Backup failed");
            }
        }

        [HttpGet("Restore")]
        public async Task<IActionResult> Restore()
        {
            string path = $"{this.webHostEnvironment.WebRootPath}\\..\\DBScripts\\backup.bak";

            bool success = await RestoreDatabaseAsync(path);
            if (success)
            {
                return Ok("Restore was successful");
            }
            else
            {
                return BadRequest("Restore failed");
            }
        }
        //this function backup the database to a specified path
        private async Task<bool> BackupDatabaseAsync(string path)
        {
            try
            {

                //Get the connection string
                string? connectionString = context.Database.GetConnectionString();
                //Get the database name
                string databaseName = context.Database.GetDbConnection().Database;
                //Build the backup command
                string command = $"BACKUP DATABASE {databaseName} TO DISK = '{path}'";
                //Create a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the connection
                    await connection.OpenAsync();
                    //Create a command
                    using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                    {
                        //Execute the command
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //THis function restore the database from a backup in a certain path
        private async Task<bool> RestoreDatabaseAsync(string path)
        {
            try
            {
                //Get the connection string
                string? connectionString = context.Database.GetConnectionString();
                //Get the database name
                string databaseName = context.Database.GetDbConnection().Database;
                //Build the restore command
                string command = $@"
                USE master;
                ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                RESTORE DATABASE {databaseName} FROM DISK = '{path}' WITH REPLACE;
                ALTER DATABASE {databaseName} SET MULTI_USER;";

                //Create a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the connection
                    await connection.OpenAsync();
                    //Create a command
                    using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                    {
                        //Execute the command
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region images
        //this function gets a file stream and check if it is an image
        private static bool IsImage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            List<string> jpg = new List<string> { "FF", "D8" };
            List<string> bmp = new List<string> { "42", "4D" };
            List<string> gif = new List<string> { "47", "49", "46" };
            List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }

        //this function check which profile image exist and return the virtual path of it.
        //if it does not exist it returns the default profile image virtual path

        private string GetMonsterImageVirtualPath(int userId, string monsterName)
        {
            string virtualPath = $"/monsterImages/{userId}";
            string path = $"{this.webHostEnvironment.WebRootPath}\\monsterImages\\{userId}_{monsterName}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{this.webHostEnvironment.WebRootPath}\\monsterImages\\{userId}_{monsterName}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/monsterImages/default.png";
                }
            }

            return virtualPath;
        }

        //THis function gets a userId and a profile image file and save the image in the server
        //The function return the full path of the file saved
        [HttpPost("uploadMonsterImage")]
        public async Task<IActionResult> UploadMonsterImage(IFormFile file, [FromQuery] string monsterName, [FromQuery] int userId)
        {
            //Read all files sent
            long imagesSize = 0;
            try
            {
                if (file.Length > 0)
                {
                    //Check the file extention!
                    string[] allowedExtentions = { ".png", ".jpg" };
                    string extention = "";
                    if (file.FileName.LastIndexOf(".") > 0)
                    {
                        extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                    }
                    if (!allowedExtentions.Where(e => e == extention).Any())
                    {
                        //Extention is not supported
                        return BadRequest("File sent with non supported extention");
                    }

                    //Build path in the web root (better to a specific folder under the web root
                    string filePath = $"{this.webHostEnvironment.WebRootPath}\\monsterImages\\{userId}_{monsterName}{extention}";
                    string virtualFilePath = $"/monsterImages/{userId}_{monsterName}{extention}";

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        if (IsImage(stream))
                        {
                            imagesSize += stream.Length;
                        }
                        else
                        {
                            //Delete the file if it is not supported!
                            System.IO.File.Delete(filePath);
                            return BadRequest("File sent is not an image");
                        }

                    }
                    Monster modelMonster = context.Monsters.Where(m => m.MonsterName == monsterName && m.MonsterId == userId).FirstOrDefault();
                    if (modelMonster.MonsterPic != virtualFilePath)
                    {
                        modelMonster.MonsterPic = virtualFilePath;
                        context.SaveChanges();
                    }


                    return Ok(modelMonster);

                }

                return BadRequest("File in size 0");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //this function check which profile image exist and return the virtual path of it.
        //if it does not exist it returns the default profile image virtual path

        private string GetCharacterImageVirtualPath(int userId, string characterName)
        {
            string virtualPath = $"/characterImages/{userId}";
            string path = $"{this.webHostEnvironment.WebRootPath}\\characterImages\\{userId}_{characterName}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{this.webHostEnvironment.WebRootPath}\\characterImages\\{userId}_{characterName}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/characterImages/deafult_character.png";
                }
            }

            return virtualPath;
        }

        //THis function gets a userId and a profile image file and save the image in the server
        //The function return the full path of the file saved
        [HttpPost("uploadCharacterImage")]
        public async Task<IActionResult> UploadCharacterImage(IFormFile file, [FromQuery] string characterName, [FromQuery] int userId)
        {
            //Read all files sent
            long imagesSize = 0;
            try
            {
                if (file.Length > 0)
                {
                    //Check the file extention!
                    string[] allowedExtentions = { ".png", ".jpg" };
                    string extention = "";
                    if (file.FileName.LastIndexOf(".") > 0)
                    {
                        extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                    }
                    if (!allowedExtentions.Where(e => e == extention).Any())
                    {
                        //Extention is not supported
                        return BadRequest("File sent with non supported extention");
                    }

                    //Build path in the web root (better to a specific folder under the web root
                    string filePath = $"{this.webHostEnvironment.WebRootPath}\\characterImages\\{userId}_{characterName}{extention}";
                    string virtualFilePath = $"/characterImages/{userId}_{characterName}{extention}";

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        if (IsImage(stream))
                        {
                            imagesSize += stream.Length;
                        }
                        else
                        {
                            //Delete the file if it is not supported!
                            System.IO.File.Delete(filePath);
                            return BadRequest("File sent is not an image");
                        }

                    }

                    Character modelCharacter = context.Characters.Where(m => m.CharacterName == characterName && m.CharacterId == userId).FirstOrDefault();
                    if (modelCharacter.CharacterPic != virtualFilePath)
                    {
                        modelCharacter.CharacterPic = virtualFilePath;
                        context.SaveChanges();
                    }

                    return Ok(modelCharacter);

                }

                return BadRequest("File in size 0");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //THis function gets a userId and a profile image file and save the image in the server
        //The function return the full path of the file saved
        private async Task<string> SaveProfileImageAsync(int userId, IFormFile file)
        {
            //Read all files sent
            long imagesSize = 0;

            if (file.Length > 0)
            {
                //Check the file extention!
                string[] allowedExtentions = { ".png", ".jpg" };
                string extention = "";
                if (file.FileName.LastIndexOf(".") > 0)
                {
                    extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                }
                if (!allowedExtentions.Where(e => e == extention).Any())
                {
                    //Extention is not supported
                    throw new Exception("File sent with non supported extention");
                }

                //Build path in the web root (better to a specific folder under the web root
                string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}{extention}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);

                    if (IsImage(stream))
                    {
                        imagesSize += stream.Length;
                    }
                    else
                    {
                        //Delete the file if it is not supported!
                        System.IO.File.Delete(filePath);
                        throw new Exception("File sent is not an image");
                    }

                }

                return filePath;

            }

            throw new Exception("File in size 0");
        }
        #endregion
    }
}

