using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.NPCs.HonkaiBeasts
{
    public class Cassiel: ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 64;
            npc.height = 60;
            npc.aiStyle = 44; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            npc.damage = 50;
            npc.defense = 10;
            npc.lifeMax = 300;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            //npc.alpha = 175;
            //npc.color = new Color(0, 80, 255, 100);
            npc.value = 25f;
        }
        /*public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // we would like this npc to spawn in the overworld.
            return SpawnCondition.OverworldDay.Chance * 0.25f;
        }*/

        public override void NPCLoot()
        {
            Item.NewItem(npc.position, ModContent.ItemType<Items.Materials.HonkaiShard>());
        }

        private const int frames = 10;

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= frames * Main.npcFrameCount[npc.type])
                npc.frameCounter = 0;
            npc.frame.Y = (int)(npc.frameCounter / frames) * frameHeight;
        }
    }
}
