using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace BattleMapServer.Models
{
   
        public partial class BattleMapDbContext
        {
            public User? GetUser(string Name)
            {
                return this.Users.Where(u => u.UserName == Name)
                                    .FirstOrDefault();
            }
        }
    
}
