using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.NPCs.HonkaiBeasts
{
    public class Paladin: ModNPC
    {
        private float agroDist = 700f;

        private float movementSpeed = 8f;
        private float chargeThreashold = 3f;

        private bool charge;

        private int frames = 10;

        private const int State_idle = 0;
        private const int State_walk = 1;

        private const int AI_State_Slot = 0;
        private const int AI_Timer_Slot = 1;
        private const int AI_Stuck_Timer_Slot = 2;

        public float AI_State
        {
            get => NPC.ai[AI_State_Slot];
            set => NPC.ai[AI_State_Slot] = value;
        }
/*        public float AI_Timer
        {
            get => npc.ai[AI_Timer_Slot];
            set => npc.ai[AI_Timer_Slot] = value;
        }
        public float Stuck_Timer
        {
            get => npc.ai[AI_Stuck_Timer_Slot];
            set => npc.ai[AI_Stuck_Timer_Slot] = value;
        }*/

        private int fpt = 4;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 80;
            NPC.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            NPC.damage = 100;
            NPC.defense = 10;
            NPC.lifeMax = 600;
            NPC.knockBackResist = 0.5f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.scale = 1f;
            NPC.stepSpeed = 2.5f;
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
            if (Main.rand.NextFloat() < 0.025f)
                Item.NewItem(new EntitySource_Loot(NPC), NPC.position, 0, 0, ModContent.ItemType<Items.Materials.HonkaiShard>());
        }
        public override void AI()
        {
            int targetDir = Main.player[NPC.target].position.X > NPC.position.X ? -1 : 1;
            NPC.direction = NPC.velocity.X > 0 ? -1 : 1;

            Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);

            if (!NPC.HasValidTarget)
            {
                AI_State = State_idle;
            }

            if (AI_State == State_idle)
            {
                NPC.TargetClosest(true);
                if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < agroDist)
                {
                    AI_State = State_walk;
                }
                else
                {
                    NPC.velocity.X = 0;
                    AI_State = State_idle;
                }
            }
            else if(AI_State == State_walk)
            {
                NPC.velocity.X += -targetDir * 0.1f;
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -movementSpeed, movementSpeed);
            }
            if(Math.Abs(NPC.velocity.X) > chargeThreashold)
            {
                charge = true;
                NPC.knockBackResist = 0f;
                NPC.damage = 160;
                NPC.defense = 30;
                frames = 4;
            }
            else
            {
                charge = false;
                NPC.knockBackResist = 0.5f;
                NPC.damage = 100;
                NPC.defense = 30;
                frames = 10;
            }
        }

        private int[] idleFrame = { 0 };
        private int[] walkFrame = { 0, 1, 2, 3 };
        private int[] chargeFrame = { 4, 5, 6, 7 };

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            switch (AI_State)
            {
                case State_idle:
                    {
                        if (NPC.frameCounter >= frames * idleFrame.Length)
                            NPC.frameCounter = 0;
                        NPC.frame.Y = idleFrame[(int)NPC.frameCounter / frames] * frameHeight;
                    }
                    break;
                case State_walk:
                    {
                        if (charge)
                        {
                            if (NPC.frameCounter >= frames * chargeFrame.Length)
                                NPC.frameCounter = 0;
                            NPC.frame.Y = chargeFrame[(int)NPC.frameCounter / frames] * frameHeight;
                        }
                        else
                        {
                            if (NPC.frameCounter >= frames * walkFrame.Length)
                                NPC.frameCounter = 0;
                            NPC.frame.Y = walkFrame[(int)NPC.frameCounter / frames] * frameHeight;
                        }
                    }
                    break;
            }
        }
    }
}
