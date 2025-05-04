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

        public async Task<string?> AddToGroup(string groupName, int userId, bool isCreator)
        {
            try
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

                //if (hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Details != null)
                //    await Clients.Client(Context.ConnectionId).SendAsync("UpdateMap", hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Details);
                
                User modelsUser = context.Users.Where(u => u.UserId == userId).FirstOrDefault();
                DTO.User dtoUser = new DTO.User(modelsUser);
                hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Users.Add(dtoUser);
                await Clients.Group(groupName).SendAsync("UpdateUsers", hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Users);

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        public async Task UpdateMapDetails(MapDetails details, string groupName)
        {
            lock(hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Details)
            {
                hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Details = details;
            }            
            await Clients.Group(groupName).SendAsync("UpdateMap", details);
        }

        public async Task<MapDetails> GetMapDetails(string groupName)
        {
            MapDetails details = hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Details;
            return details;
        }

        public async Task RemoveFromGroup(string groupName, int userId)
        {
            List<DTO.User> currentGroupUsers = hubGroups.Where(g => g.Name == groupName).FirstOrDefault().Users;
            currentGroupUsers.Remove(currentGroupUsers.Where(u => u.UserId == userId).FirstOrDefault());
            if (currentGroupUsers.Count == 0) hubGroups.Remove(hubGroups.Where(g => g.Name == groupName).FirstOrDefault());
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
