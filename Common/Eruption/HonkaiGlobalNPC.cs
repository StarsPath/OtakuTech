using OtakuTech.Common.Eruption;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.Common.Eruption
{
    public class HonkaiGlobalNPC: GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if(HonkaiWorld.honkaiInvasionActive && (Main.invasionX == (double)Main.spawnTileX))
            {
                Player player = spawnInfo.Player;
                pool.Clear();
                foreach (int i in Eruption.invaders_LV1)
                {
                    pool.Add(i, 1f);
                }
            }
            //base.EditSpawnPool(pool, spawnInfo);
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (HonkaiWorld.honkaiInvasionActive && (Main.invasionX == (double)Main.spawnTileX))
            {
                spawnRate = 100; //Higher the number, the more spawns
                maxSpawns = 10000; //Max spawns of NPCs depending on NPC value
            }
            //base.EditSpawnRate(player, ref spawnRate, ref maxSpawns);
        }

        public override void PostAI(NPC npc)
        {
            if (HonkaiWorld.honkaiInvasionActive && (Main.invasionX == (double)Main.spawnTileX))
            {
                npc.timeLeft = 60 * 60;
            }
            //base.PostAI(npc);
        }

        public override void OnKill(NPC npc)
        {
            if (HonkaiWorld.honkaiInvasionActive && (Main.invasionX == (double)Main.spawnTileX))
            {
                if (Eruption.invaders_LV1.Contains(npc.type))
                    Main.invasionSize -= 1;
                if (Eruption.invaders_LV2.Contains(npc.type))
                    Main.invasionSize -= 2;
            }
            //base.NPCLoot(npc);
        }
    }
}
