using BattleMapServer.Classes_and_Objects;
using BattleMapServer.Models;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace BattleMapServer.Hubs
{
    public class BattleMapHub: Hub
    {
        private BattleMapDbContext context;
        private MapDetails mapDetails;

        public BattleMapHub(BattleMapDbContext context)
        {
            this.context = context;
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            
            if (mapDetails != null)
                await Clients.Client(Context.ConnectionId).SendAsync("UpdateMap", mapDetails);
        }

        public async Task UpdateMapDetails(MapDetails details, string groupName)
        {
            mapDetails = details;

            await Clients.Group(groupName).SendAsync("UpdateMap", details);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
