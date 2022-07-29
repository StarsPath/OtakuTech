using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.NPCs.HonkaiBeasts
{
    public class Crab: ModNPC
    {
        private float jumpDist = 700f;
        private float maxJumpHeight = 10f;
        private float maxJumpDist = 500f;
        private float jump_cd = 180; //6 sec
        private float bounceCount = 4;

        private float movementSpeed = 3f;
        //private float max_Xspeed = 2.5f;

        private float stuck_threashold = 300;
        private bool stuckX;

        private float localTimer = 0;
        private float randomNum = 300;

        private const int State_idle = 0;
        private const int State_walk = 1;
        private const int State_jump = 2;
        private const int State_fall = 3;
        private const int State_roll = 4;
        private const int State_rolling = 5;

        private const int AI_State_Slot = 0;
        private const int AI_Timer_Slot = 1;
        private const int AI_Stuck_Timer_Slot = 2;
        private const int AI_Bounce_Counter = 3;

        public float AI_State
        {
            get => NPC.ai[AI_State_Slot];
            set => NPC.ai[AI_State_Slot] = value;
        }
        public float AI_Timer
        {
            get => NPC.ai[AI_Timer_Slot];
            set => NPC.ai[AI_Timer_Slot] = value;
        }
        public float Stuck_Timer
        {
            get => NPC.ai[AI_Stuck_Timer_Slot];
            set => NPC.ai[AI_Stuck_Timer_Slot] = value;
        }

        public float Bounce_Counter
        {
            get => NPC.ai[AI_Bounce_Counter];
            set => NPC.ai[AI_Bounce_Counter] = value;
        }

        private int fpt = 4;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 36;
            NPC.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            NPC.damage = 80;
            NPC.defense = 60;
            NPC.lifeMax = 800;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.scale = 1.25f;
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
            Item.NewItem(new EntitySource_Loot(NPC), NPC.position, 0, 0, ModContent.ItemType<Items.Materials.HonkaiShard>());
        }
        public override void AI()
        {
            NPC.direction = Main.player[NPC.target].position.X > NPC.position.X ? 1 : -1;
            Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);

            if (AI_Timer > 0)
                AI_Timer--;

            if (!NPC.HasValidTarget)
            {
                AI_State = State_idle;
            }

            if (AI_State == State_idle)
            {
                NPC.TargetClosest(true);
                NPC.rotation = 0;
                NPC.defense = 60;
                //Main.NewText(Main.player[npc.target].Distance(npc.Center));

                if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) > jumpDist)
                {
                    AI_State = State_jump;
                }
                else if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < jumpDist)
                {
                    AI_State = State_walk;
                }
                //AI_Timer = 0f;
            }
            else if (AI_State == State_jump)
            {
                NPC.defense = 240;
                if (NPC.collideY)
                {
                    //Main.NewText(npc.position);
                    //Main.NewText(Main.player[npc.target].position);

                    float xdist = Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) / 16;
                    float ydist = Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) / 16;

                    float initX = (xdist) * 0.25f;
                    float initY = maxJumpHeight + (ydist * 0.25f);
                    if (Main.player[NPC.target].Distance(NPC.Center) > maxJumpDist)
                        initX = ((maxJumpDist / 16) - 1.75f) * 0.25f;

                    Vector2 vel = new Vector2(NPC.direction * initX, -initY);
                    //Vector2 vel = new Vector2(x_Speed, 2 * x_Speed - Math.Abs(npc.position.X - Main.player[npc.target].position.X) * x_Speed);
                    NPC.velocity = vel;
                    AI_State = State_fall;
                }
                else
                {
                    AI_State = State_fall;
                }
            }
            else if(AI_State == State_fall)
            {
                NPC.defense = 240;
                if (NPC.collideY)
                {
                    if (Bounce_Counter < bounceCount)
                    {
                        AI_State = State_jump;
                        Bounce_Counter++;
                    }
                    else
                    {
                        AI_State = State_walk;
                        Bounce_Counter = 0;
                    }
                    //NPC.velocity.X = 0;
                    //AI_State = State_idle;
                }
                float xdist = Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) / 16;
                float initX = (xdist) * 0.25f;
                if (Main.player[NPC.target].Distance(NPC.Center) > maxJumpDist)
                    initX = ((maxJumpDist / 16) - 1.75f) * 0.25f;

                NPC.velocity.X = NPC.direction * initX;
                NPC.rotation += NPC.direction * 0.4f;
            }
            else if(AI_State == State_walk)
            {
                NPC.defense = 60;
                NPC.rotation = 0;
                if (AI_Timer <= 0)
                {
                    AI_Timer = jump_cd;
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        AI_State = State_roll;
                        AI_Timer = 120;
                        return;
                    }
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        AI_State = State_jump;
                        return;
                    }
                }

                NPC.velocity.X += NPC.direction * 0.2f;
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -movementSpeed, movementSpeed);
                //NPC.knockBackResist = 0.5f;
                //NPC.defense = 20;
                //NPC.damage = 80;
            }
            else if(AI_State == State_roll)
            {
                //float ydist = Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) / 16;
                //if (Stuck_Timer >= 120 || ydist > 8f)
                //{
                //    AI_State = State_idle;
                //    NPC.rotation = 0;
                //    return;
                //}
                NPC.defense = 240;
                if (AI_Timer <= 0)
                {
                    //NPC.velocity = Vector2.Normalize(NPC.DirectionTo(Main.player[NPC.target].Center)) * 8f;
                    NPC.velocity.X = NPC.direction * movementSpeed * 8f;
                    //NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -movementSpeed * 4, movementSpeed * 4);
                    NPC.rotation += NPC.direction * 0.8f;
                    AI_State = State_rolling;
                    AI_Timer = 90;
                }
                else
                {
                    NPC.velocity.X = 0;
                    NPC.rotation += NPC.direction * 0.8f;
                }
            }
            else if(AI_State == State_rolling)
            {
                NPC.defense = 240;
                if (AI_Timer <= 0)
                {
                    AI_State = State_idle;
                    NPC.rotation = 0;
                }
                NPC.rotation += NPC.direction * 0.8f;
            }

            if (NPC.collideX)
            {
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
                    NPC.velocity.Y += -maxJumpHeight;
                    NPC.velocity.X = NPC.direction * movementSpeed;
                }
                Stuck_Timer = 0;
            }

            int tileX = (int)NPC.Bottom.X / 16;
            int tileY = (int)NPC.Bottom.Y / 16;

            if (Main.tileSolidTop[Main.tile[tileX, tileY].TileType])
            {
                localTimer++;

                if (localTimer >= randomNum)
                {
                    NPC.noTileCollide = true;
                    NPC.velocity.Y = 2;
                    randomNum = Main.rand.NextFloat(60, 600);
                    localTimer = 0;
                }
            }
            else
            {
                NPC.noTileCollide = false;
            }
        }

        private int[] idleFrame = { 0 };
        private int[] walkFrame = { 0, 1, 2, 3 };
        private int[] rollFrame = { 4 };

        private const int frames = 10;

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
                        if (NPC.frameCounter >= frames * walkFrame.Length)
                            NPC.frameCounter = 0;
                        NPC.frame.Y = walkFrame[(int)NPC.frameCounter / frames] * frameHeight;
                    }
                    break;
                case State_jump:
                case State_fall:
                case State_roll:
                case State_rolling:
                    {
                        if (NPC.frameCounter >= frames * rollFrame.Length)
                            NPC.frameCounter = 0;
                        NPC.frame.Y = rollFrame[(int)NPC.frameCounter / frames] * frameHeight;
                    }
                    break;
            }
        }
    }
}
