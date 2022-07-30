using OtakuTech.Common.Eruption;
using OtakuTech.Content.NPCs.Benares;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
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
                int[] invaders = Eruption.invaders_LV1;
                if (NPC.downedMechBossAny)
                {
                    invaders = invaders.Concat(Eruption.invaders_LV2).ToArray();
                }
                
                pool.Clear();
                foreach (int i in invaders)
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
                spawnRate = 50; //Higher the number, the more spawns
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
            if (Main.rand.NextFloat() < 0.01f)
                Item.NewItem(new EntitySource_Loot(npc), npc.Center, 0, 0, ModContent.ItemType<Content.Items.Materials.HonkaiShard>());

            if (HonkaiWorld.honkaiInvasionActive && (Main.invasionX == (double)Main.spawnTileX))
            {
                if (Eruption.invaders_LV1.Contains(npc.type))
                    Main.invasionSize -= 1;
                if (Eruption.invaders_LV2.Contains(npc.type))
                    Main.invasionSize -= 2;

                if(((float)Main.invasionProgress / Main.invasionProgressMax) >= 0.75f && !HonkaiWorld.benaresActive && NPC.downedMechBossAny)
                {
                    HonkaiWorld.benaresActive = true;
                    Player player = Main.player[Main.myPlayer];
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        // If the player is not in multiplayer, spawn directly
                        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Benares>());
                    }
                    else
                    {
                        // If the player is in multiplayer, request a spawn
                        // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                        NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: ModContent.NPCType<Benares>());
                    }
                }

                //Main.NewText();
            }
            //base.NPCLoot(npc);
        }
    }
}
