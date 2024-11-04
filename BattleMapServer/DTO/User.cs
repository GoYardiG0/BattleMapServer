using BattleMapServer.Models;

namespace BattleMapServer.DTO
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

        public virtual ICollection<Monster> Monsters { get; set; } = new List<Monster>();
        public User(Models.User modelUser)
        {
            this.UserId = modelUser.UserId;
            this.UserName = modelUser.UserName;           
            this.UserEmail = modelUser.UserEmail;
            this.UserPassword = modelUser.UserPassword;
            this.Characters = new List<Character>();
            foreach (var character in modelUser.Characters) 
            {
                this.Characters.Add(new Character(character));
            }

            this.Monsters = new List<Monster>();
            foreach (var monster in modelUser.Monsters)
            {
                this.Monsters.Add(new Monster(monster));
            }

        }
    }
}
