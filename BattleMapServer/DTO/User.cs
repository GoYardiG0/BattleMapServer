using BattleMapServer.Models;

namespace BattleMapServer.DTO
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public virtual ICollection<Models.Character> Characters { get; set; } = new List<Models.Character>();

        public virtual ICollection<Models.Monster> Monsters { get; set; } = new List<Models.Monster>();
        public User(Models.User modelUser)
        {
            this.UserId = modelUser.UserId;
            this.UserName = modelUser.UserName;           
            this.UserEmail = modelUser.UserEmail;
            this.UserPassword = modelUser.UserPassword;
            this.Characters = modelUser.Characters;
            foreach (var character in modelUser.Characters) 
            {
                this.Characters.Add(new Character(character));
            }

            this.Monsters = modelUser.Monsters;
            this.UserTasks = new List<UserTask>();
            foreach (var task in modelUser.UserTasks)
            {
                this.UserTasks.Add(new UserTask(task));
            }
        }
    }
}
