using BattleMapServer.Classes_and_Objects;
using BattleMapServer.Models;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace BattleMapServer.Hubs
{
    public class BattleMapHub: Hub
    {
        private BattleMapDbContext context;
        private static List<HubGroup> hubGroups = new List<HubGroup>();

        public BattleMapHub(BattleMapDbContext context)
        {
            this.context = context;
        }

        public async Task<string> AddToGroup(string groupName, int userId, bool isCreator)
        {
            if (hubGroups.Where(g => g.Name == groupName).IsNullOrEmpty())
            {
                if (isCreator)
                {
                    hubGroups.Add(new HubGroup(groupName));
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                }
                else
                {
                    return "group doesnt exist";
                }
            }
            else if (isCreator)
            {
                return "group already exists";
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            }            

            if (hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Details != null)
                await Clients.Client(Context.ConnectionId).SendAsync("UpdateMap", hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Details);
            User modelsUser = context.Users.Where(u => u.UserId == userId).FirstOrDefault();
            DTO.User dtoUser = new DTO.User(modelsUser);
            await Clients.Group(groupName).SendAsync("UpdateUsers", dtoUser);

            return "";
        }

        public async Task UpdateMapDetails(MapDetails details, string groupName)
        {
            lock(hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Details)
            {
                hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Details = details;
            }            
            await Clients.Group(groupName).SendAsync("UpdateMap", details);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
