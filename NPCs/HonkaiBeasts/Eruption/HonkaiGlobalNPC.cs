using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.NPCs.HonkaiBeasts.Eruption
{
    public class HonkaiGlobalNPC: GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if(HonkaiWorld.honkaiInvasionActive && (Main.invasionX == (double)Main.spawnTileX))
            {
                pool.Clear();
                foreach(int i in Eruption.invaders)
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
                npc.timeLeft = 1000;
            }
            //base.PostAI(npc);
        }

        public override void NPCLoot(NPC npc)
        {
            if (HonkaiWorld.honkaiInvasionActive && (Main.invasionX == (double)Main.spawnTileX))
            {
                if (Eruption.invaders.Contains(npc.type))
                    Main.invasionSize -= 1;
            }
            //base.NPCLoot(npc);
        }
    }
}
