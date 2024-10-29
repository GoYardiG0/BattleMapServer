using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace BattleMapServer.Models
{
   
        public partial class BattleMapDbContext
        {
            public User? GetUser(string email)
            {
                return this.Users.Where(u => u.UserEmail == email)
                                    .FirstOrDefault();
            }
        }
    
}
