﻿using BattleMapServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace BattleMapServer.Hubs
{
    public class BattleMapHub: Hub
    {
        private BattleMapDbContext context;
        public BattleMapHub(BattleMapDbContext context)
        {
            this.context = context;
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
             
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
