using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.NPCs.HonkaiBeasts
{
    public class AlloyChariot: ModNPC
    {
        private float jumpDist = 700f;
        private float maxJumpHeight = 10f;
        private float maxJumpDist = 500f;
        private float jump_cd = 180; //6 sec

        private float movementSpeed = 2.5f;
        //private float max_Xspeed = 2.5f;

        private float stuck_threashold = 300;
        private bool stuckX;

        private float localTimer = 0;
        private float randomNum = 300;

        private const int State_idle = 0;
        private const int State_walk = 1;
        private const int State_jump = 2;
        private const int State_fall = 3;

        private const int AI_State_Slot = 0;
        private const int AI_Timer_Slot = 1;
        private const int AI_Stuck_Timer_Slot = 2;

        public float AI_State
        {
            get => npc.ai[AI_State_Slot];
            set => npc.ai[AI_State_Slot] = value;
        }
        public float AI_Timer
        {
            get => npc.ai[AI_Timer_Slot];
            set => npc.ai[AI_Timer_Slot] = value;
        }
        public float Stuck_Timer
        {
            get => npc.ai[AI_Stuck_Timer_Slot];
            set => npc.ai[AI_Stuck_Timer_Slot] = value;
        }

        private int fpt = 4;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 46;
            npc.height = 38;
            npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            npc.damage = 80;
            npc.defense = 20;
            npc.lifeMax = 600;
            npc.knockBackResist = 0.5f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.scale = 1.25f;
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
            npc.direction = Main.player[npc.target].position.X > npc.position.X ? 1 : -1;

            if (AI_Timer > 0)
                AI_Timer--;

            if (!npc.HasValidTarget)
            {
                AI_State = State_idle;
            }

            if (AI_State == State_idle)
            {
                npc.TargetClosest(true);
                //Main.NewText(Main.player[npc.target].Distance(npc.Center));

                if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) > jumpDist)
                {
                    AI_State = State_jump;
                }
                else if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < jumpDist)
                {
                    AI_State = State_walk;
                }
                //AI_Timer = 0f;
            }
            else if (AI_State == State_jump)
            {
                if (npc.collideY)
                {
                    //Main.NewText(npc.position);
                    //Main.NewText(Main.player[npc.target].position);

                    float xdist = Math.Abs(npc.Center.X - Main.player[npc.target].Center.X)/16;
                    float ydist = Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y)/16;

                    float initX = (xdist) * 0.25f;
                    float initY = maxJumpHeight + (ydist * 0.25f);
                    if (Main.player[npc.target].Distance(npc.Center) > maxJumpDist)
                        initX = ((maxJumpDist / 16) - 1.75f) * 0.25f;
/*
                    Main.NewText(initX);
                    Main.NewText(ydist);
                    Main.NewText(initY);*/
                    Vector2 vel = new Vector2(npc.direction * initX , -initY);
                    //Vector2 vel = new Vector2(x_Speed, 2 * x_Speed - Math.Abs(npc.position.X - Main.player[npc.target].position.X) * x_Speed);
                    npc.velocity = vel;
                    AI_State = State_fall;
                    npc.knockBackResist = 0f;
                    npc.defense = 40;
                    npc.damage = 120;
                }
                else
                {
                    AI_State = State_fall;
                }
            }
            else if(AI_State == State_fall)
            {
                if (npc.collideY)
                {
                    npc.velocity.X = 0;
                    AI_State = State_idle;
                }
            }
            else if(AI_State == State_walk)
            {
                if(AI_Timer <= 0)
                {
                    if(Main.rand.NextFloat() < 0.5f)
                        AI_State = State_jump;
                    AI_Timer = jump_cd;
                }

                npc.velocity.X += npc.direction * 0.2f;
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -movementSpeed, movementSpeed);
                npc.knockBackResist = 0.5f;
                npc.defense = 20;
                npc.damage = 80;
            }
            if (npc.collideX)
            {
                Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY);
                stuckX = true;
                Stuck_Timer++;
            }
            else
            {
                stuckX = false;
            }
                


            if(Stuck_Timer >= stuck_threashold)
            {
                if(stuckX)
                {
                    npc.velocity.Y += -maxJumpHeight;
                    npc.velocity.X = npc.direction * movementSpeed;
                }
                Stuck_Timer = 0;
            }

            int tileX = (int)npc.Bottom.X / 16;
            int tileY = (int)npc.Bottom.Y / 16;

            if (Main.tileSolidTop[Main.tile[tileX, tileY].type])
            {
                localTimer++;

                if (localTimer >= randomNum)
                {
                    npc.noTileCollide = true;
                    npc.velocity.Y = 2;
                    randomNum = Main.rand.NextFloat(60, 600);
                    localTimer = 0;
                }
            }
            else
            {
                npc.noTileCollide = false;
            }
        }

        private int[] idleFrame = { 0 };
        private int[] walkFrame = { 0, 1, 2, 3 };
        private int[] jumpFrame = { 4 };
        private int[] fallFrame = { 5 };

        private const int frames = 10;

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
                        if (npc.frameCounter >= frames * walkFrame.Length)
                            npc.frameCounter = 0;
                        npc.frame.Y = walkFrame[(int)npc.frameCounter / frames] * frameHeight;
                    }
                    break;
                case State_jump:
                    {
                        if (npc.frameCounter >= frames * jumpFrame.Length)
                            npc.frameCounter = 0;
                        npc.frame.Y = jumpFrame[(int)npc.frameCounter / frames] * frameHeight;
                    }
                    break;
                case State_fall:
                    {
                        if (npc.frameCounter >= frames * fallFrame.Length)
                            npc.frameCounter = 0;
                        npc.frame.Y = fallFrame[(int)npc.frameCounter / frames] * frameHeight;
                    }
                    break;
            }
        }
    }
}
