using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.NPCs.HonkaiBeasts
{
    public class Cassiel: ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 64;
            NPC.height = 60;
            NPC.aiStyle = 44; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            NPC.damage = 50;
            NPC.defense = 10;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            //npc.alpha = 175;
            //npc.color = new Color(0, 80, 255, 100);
            NPC.value = 25f;
        }
        /*public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // we would like this npc to spawn in the overworld.
            return SpawnCondition.OverworldDay.Chance * 0.25f;
        }*/

        public override void OnKill()
        {
            Item.NewItem(new EntitySource_Loot(NPC), NPC.position,0 , 0, ModContent.ItemType<Items.Materials.HonkaiShard>());
        }

        private const int frames = 10;

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter >= frames * Main.npcFrameCount[NPC.type])
                NPC.frameCounter = 0;
            NPC.frame.Y = (int)(NPC.frameCounter / frames) * frameHeight;
        }
    }
}
