using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Hubs
{
    public class AtsHubs : Hub
    {
        public async Task SendItemUpdate(long userId, DateTime AttendanceLogTime, string CheckType)
        {
            await Clients.All.SendAsync("ReceiveItemUpdate", userId, AttendanceLogTime, CheckType);
        }

    }
}
