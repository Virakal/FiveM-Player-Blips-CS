using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Virakal.FiveM.PlayerBlips
{
    public class PlayerBlips : BaseScript
    {
        private IDictionary<Player, Blip> blips { get; } = new Dictionary<Player, Blip>(PlayerList.MaxPlayers);

        public PlayerBlips()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            var playerList = new PlayerList();

            foreach (var player in playerList)
            {

            }

            await Delay(100);
        }
    }
}
