using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;

namespace Virakal.FiveM.PlayerBlips
{
    public class PlayerBlips : BaseScript
    {
        private IDictionary<Player, int> blips { get; } = new Dictionary<Player, int>(PlayerList.MaxPlayers);

        public PlayerBlips()
        {
            Tick += UpdateBlips;
        }

        private async Task UpdateBlips()
        {
            var playerList = new PlayerList();

            // Don't think this is needed anymore
            foreach (KeyValuePair<Player, int> entry in blips)
            {
                var player = entry.Key;
                var blip = entry.Value;

                if (!player.Character.Exists())
                {
                    RemoveBlip(ref blip);
                }
            }

            foreach (var player in playerList)
            {
                int blip = -1;

                // Don't show blips for the current player
                if (player == Game.Player)
                {
                    continue;
                }

                var playerPed = player.Character;
                var playerExists = playerPed.Exists();

                // Don't show blips for missing players
                if (blips.ContainsKey(player))
                {
                    blip = blips[player];
                }

                // Don't recreate
                if (playerExists && (blip == -1 || !DoesBlipExist(blip)))
                {
                    blip = AddBlipForEntity(playerPed.Handle);
                    blips[player] = blip;
                    SetBlipNameToPlayerName(blip, player.Handle);
                    SetBlipScale(blip, 0.9f);
                    SetBlipColour(blip, player.Handle + 11);
                    SetBlipCategory(blip, 2);
                }

                // If there's a blip and the player no longer exists, destroy it
                if (!playerExists && blip != -1)
                {
                    RemoveBlip(ref blip);
                }

                // Add player name above head
                // Function.Call((Hash)0xBFEFE3321A3F5015, playerPed.Handle, player.Name, false, false, "", false);
            }

            await Delay(1000);
        }
    }
}
