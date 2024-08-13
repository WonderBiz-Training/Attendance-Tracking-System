using Microsoft.AspNetCore.SignalR;
using System;

namespace MyProject.Hubs
{
    public class AtsHub : Hub
    {
        public async Task SendItemUpdate(long userId, DateTime AttendanceLogTime, string CheckType)
        {
            await Clients.All.SendAsync("ReceiveItemUpdate", userId, AttendanceLogTime, CheckType);
        }
    }
}