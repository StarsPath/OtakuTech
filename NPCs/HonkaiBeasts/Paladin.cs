using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.NPCs.HonkaiBeasts
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
            get => npc.ai[AI_State_Slot];
            set => npc.ai[AI_State_Slot] = value;
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
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 80;
            npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            npc.damage = 100;
            npc.defense = 10;
            npc.lifeMax = 600;
            npc.knockBackResist = 0.5f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.scale = 1f;
            npc.stepSpeed = 2.5f;
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
        public override void AI()
        {
            int targetDir = Main.player[npc.target].position.X > npc.position.X ? -1 : 1;
            npc.direction = npc.velocity.X > 0 ? -1 : 1;

            Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY);

            if (!npc.HasValidTarget)
            {
                AI_State = State_idle;
            }

            if (AI_State == State_idle)
            {
                npc.TargetClosest(true);
                if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < agroDist)
                {
                    AI_State = State_walk;
                }
                else
                {
                    npc.velocity.X = 0;
                    AI_State = State_idle;
                }
            }
            else if(AI_State == State_walk)
            {
                npc.velocity.X += -targetDir * 0.1f;
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -movementSpeed, movementSpeed);
            }
            if(Math.Abs(npc.velocity.X) > chargeThreashold)
            {
                charge = true;
                npc.knockBackResist = 0f;
                npc.damage = 160;
                npc.defense = 30;
                frames = 4;
            }
            else
            {
                charge = false;
                npc.knockBackResist = 0.5f;
                npc.damage = 100;
                npc.defense = 30;
                frames = 10;
            }
        }

        private int[] idleFrame = { 0 };
        private int[] walkFrame = { 0, 1, 2, 3 };
        private int[] chargeFrame = { 4, 5, 6, 7 };

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            switch (AI_State)
            {
                case State_idle:
                    {
                        if (npc.frameCounter >= frames * idleFrame.Length)
                            npc.frameCounter = 0;
                        npc.frame.Y = idleFrame[(int)npc.frameCounter / frames] * frameHeight;
                    }
                    break;
                case State_walk:
                    {
                        if (charge)
                        {
                            if (npc.frameCounter >= frames * chargeFrame.Length)
                                npc.frameCounter = 0;
                            npc.frame.Y = chargeFrame[(int)npc.frameCounter / frames] * frameHeight;
                        }
                        else
                        {
                            if (npc.frameCounter >= frames * walkFrame.Length)
                                npc.frameCounter = 0;
                            npc.frame.Y = walkFrame[(int)npc.frameCounter / frames] * frameHeight;
                        }
                    }
                    break;
            }
        }
    }
}
