using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.NPCs.HonkaiBeasts
{
    public class AlloyChariot: ModNPC
    {
        private float jumpDist = 700f;
        private float maxJumpHeight = 10f;
        private float maxJumpDist = 500f;
        private float jump_cd = 180; //6 sec

        private float movementSpeed = 3f;
        //private float max_Xspeed = 2.5f;

        //private float stuck_threashold = 300;
        //private bool stuckX;

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
            get => NPC.ai[AI_State_Slot];
            set => NPC.ai[AI_State_Slot] = value;
        }
        public float AI_Timer
        {
            get => NPC.ai[AI_Timer_Slot];
            set => NPC.ai[AI_Timer_Slot] = value;
        }
        //public float Stuck_Timer
        //{
        //    get => NPC.ai[AI_Stuck_Timer_Slot];
        //    set => NPC.ai[AI_Stuck_Timer_Slot] = value;
        //}

        private int fpt = 4;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
        }
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 38;
            NPC.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            NPC.damage = 80;
            NPC.defense = 20;
            NPC.lifeMax = 600;
            NPC.knockBackResist = 0.5f;
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
            if(Main.rand.NextFloat() < 0.025f)
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
                if (NPC.collideY)
                {
                    //Main.NewText(npc.position);
                    //Main.NewText(Main.player[npc.target].position);

                    float xdist = Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X)/16;
                    float ydist = Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y)/16;

                    float initX = (xdist) * 0.25f;
                    float initY = maxJumpHeight + (ydist * 0.25f);
                    if (Main.player[NPC.target].Distance(NPC.Center) > maxJumpDist)
                        initX = ((maxJumpDist / 16) - 1.75f) * 0.25f;
/*
                    Main.NewText(initX);
                    Main.NewText(ydist);
                    Main.NewText(initY);*/
                    Vector2 vel = new Vector2(NPC.direction * initX , -initY);
                    //Vector2 vel = new Vector2(x_Speed, 2 * x_Speed - Math.Abs(npc.position.X - Main.player[npc.target].position.X) * x_Speed);
                    NPC.velocity = vel;
                    AI_State = State_fall;
                    NPC.knockBackResist = 0f;
                    NPC.defense = 40;
                    NPC.damage = 120;
                }
                else
                {
                    AI_State = State_fall;
                }
            }
            else if(AI_State == State_fall)
            {
                if (NPC.collideY)
                {
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

                NPC.velocity.X += NPC.direction * 0.2f;
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -movementSpeed, movementSpeed);
                NPC.knockBackResist = 0.5f;
                NPC.defense = 20;
                NPC.damage = 80;
            }
            //if (NPC.collideX)
            //{
            //    Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
            //    stuckX = true;
            //    Stuck_Timer++;
            //}
            //else
            //{
            //    stuckX = false;
            //}
                


            //if(Stuck_Timer >= stuck_threashold)
            //{
            //    if(stuckX)
            //    {
            //        NPC.velocity.Y += -maxJumpHeight;
            //        NPC.velocity.X = NPC.direction * movementSpeed;
            //    }
            //    Stuck_Timer = 0;
            //}

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
        private int[] jumpFrame = { 4 };
        private int[] fallFrame = { 5 };

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
                    {
                        if (NPC.frameCounter >= frames * jumpFrame.Length)
                            NPC.frameCounter = 0;
                        NPC.frame.Y = jumpFrame[(int)NPC.frameCounter / frames] * frameHeight;
                    }
                    break;
                case State_fall:
                    {
                        if (NPC.frameCounter >= frames * fallFrame.Length)
                            NPC.frameCounter = 0;
                        NPC.frame.Y = fallFrame[(int)NPC.frameCounter / frames] * frameHeight;
                    }
                    break;
            }
        }
    }
}
