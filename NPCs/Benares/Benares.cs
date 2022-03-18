using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.NPCs.Benares
{
    [AutoloadBossHead]
    public class Benares : ModNPC
    {

        private float rush_speed = 25f;
        private float rush_time = 0.6f * 60;
        private float rush_CD = 1.2f * 60;
        private float rush_speed_mult_base = 1f;
        private float rush_speed_mult = 0f;
        private int rush_count = 0;

        private float fireball_time = 3 * 60;
        private float fireball_CD = 1.5f * 60;
        private float fireball_interval_time = 1f * 60;
        private float fireball_shootSpeed = 40f;
        private float idle_time = 1 * 60;

        private float lightningball_time = 1 * 60;
        private float lightningball_CD = 1.5f * 60;
        private float lightningball_interval_time = 1f * 60;
        private float lightningball_shootSpeed = 10f;
        private float lightningball_count = 5;

        private const int State_idle = 0;
        private const int State_Rush = 1; // Charges at the player
        private const int State_Fireball = 2;
        private const int State_Tornado = 3;
        private const int State_Lightning = 4;

        private const int AI_State_Slot = 0;
        private const int AI_Timer_Slot = 1;

        private int currentFrame = 0;

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

        private Vector2 targetLocation;
        private Vector2 dir = Vector2.Zero;


        private int fpt = 4;

        public override string HeadTexture => "OtakuTech/NPCs/Benares/Benares_Head_Boss";
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 5;
            DisplayName.SetDefault("Benares");
            NPCID.Sets.MustAlwaysDraw[npc.type] = true;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 240;
            npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            npc.damage = 80;
            npc.defense = 20;
            npc.lifeMax = 500000;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            npc.netAlways = true;
            //npc.hide = true;
            //npc.scale = 1.25f;
            //npc.alpha = 175;
            //npc.color = new Color(0, 80, 255, 100);
            //npc.value = 25f;
            drawOffsetY = 30;
        }
        /*public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // we would like this npc to spawn in the overworld.
            return SpawnCondition.OverworldDay.Chance * 0.25f;
        }*/

        public override void NPCLoot()
        {
            //Item.NewItem(npc.position, ModContent.ItemType<Items.Materials.HonkaiShard>());
        }
        public override void AI()
        {
            //Main.NewText(npc.damage);
            npc.TargetClosest();
            if ((Main.player[npc.target].Center - npc.Center).X > 0f)
            {
                npc.spriteDirection = (npc.direction = -1);
            }
            else if ((Main.player[npc.target].Center - npc.Center).X < 0f)
            {
                npc.spriteDirection = (npc.direction = 1);
            }

            //npc.rotation += 0.01f;

            //Main.NewText(Main.player[npc.target].Center - npc.Center);


            if (++AI_Timer >= 60 * 20)
            {
                AI_Timer = 0;
            }


            if (!npc.HasValidTarget)
            {
                AI_State = State_idle;
                npc.velocity = Vector2.Zero;
            }
            else
            {
                if(AI_State == State_idle)
                    AI_State = State_Lightning;
            }

            if (AI_State == State_Rush)
            {
                if (AI_Timer <= 0)
                {
                    targetLocation = Main.player[npc.target].Center;
                    dir = (targetLocation - npc.Center).SafeNormalize(Vector2.Zero);
                    npc.velocity = Vector2.Lerp(npc.velocity, Vector2.Zero, 0.05f);
                    npc.Center = new Vector2(npc.Center.X, MathHelper.Lerp(npc.Center.Y, targetLocation.Y, 0.05f));

                    float distance = Vector2.Distance(npc.Center, targetLocation);
                    rush_speed_mult = rush_speed_mult_base + distance / 1600;
                }
                else
                {
                    npc.velocity = dir * rush_speed * rush_speed_mult;
                }

                if (AI_Timer >= rush_time)
                {
                    rush_count++;
                    AI_Timer = -(rush_CD);
                    if(rush_count >= 3)
                    {
                        rush_count = 0;
                        AI_State = State_Fireball;
                    }
                }
            }

            if (AI_State == State_Fireball)
            {
                if (AI_Timer <= 0)
                {
                    npc.velocity = Vector2.Lerp(npc.velocity, Vector2.Zero, 0.05f);
                    npc.Center = Vector2.Lerp(npc.Center, targetLocation + new Vector2(0, -400f), 0.05f);
                }
                else if(AI_Timer < fireball_time + idle_time)
                {
                    targetLocation = Main.player[npc.target].Center;
                    dir = (targetLocation - npc.Center).SafeNormalize(Vector2.Zero);
                    if (AI_Timer % fireball_interval_time == 0)
                    {
                        if(dir != Vector2.Zero)
                            Projectile.NewProjectile(npc.Top, dir * fireball_shootSpeed, ModContent.ProjectileType<Projectiles.NPCs.Fireball>(), npc.damage, 0.1f, npc.whoAmI);
                    }
                }

                if (AI_Timer >= fireball_time + idle_time)
                {
                    AI_Timer = -(fireball_CD);
                    AI_State = State_Lightning;
                }
            }

            if (AI_State == State_Lightning)
            {
                if (AI_Timer <= 0)
                {
                    npc.velocity = Vector2.Lerp(npc.velocity, Vector2.Zero, 0.05f);
                    npc.Center = Vector2.Lerp(npc.Center, targetLocation + new Vector2(0, -300f), 0.05f);
                }
                else if (AI_Timer < lightningball_time + idle_time)
                {
                    targetLocation = Main.player[npc.target].Center;
                    dir = (targetLocation - npc.Center).SafeNormalize(Vector2.Zero);
                    if (AI_Timer % lightningball_interval_time == 0)
                    {
                        if (dir != Vector2.Zero)
                        {
                            int spread = 30; //degrees
                            //float numberProjectiles = 5;
                            float rotation = MathHelper.ToRadians(spread);
                            //npc.Top += dir * lightningball_shootSpeed * spread;
                            for (int i = 0; i < lightningball_count; i++)
                            {
                                Vector2 perturbedSpeed = (dir * lightningball_shootSpeed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (lightningball_count - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                                //Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage / 2, knockBack, player.whoAmI);
                                Projectile.NewProjectile(npc.Top.X, npc.Top.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Projectiles.NPCs.LightningBall>(), (int)(npc.damage/ lightningball_count), 0.1f, npc.whoAmI);
                            }
                        }
                            //Projectile.NewProjectile(npc.Top, dir * lightningball_shootSpeed, ModContent.ProjectileType<Projectiles.NPCs.LightningBall>(), npc.damage, 0.1f, npc.whoAmI);
                    }
                }

                if (AI_Timer >= lightningball_time + idle_time)
                {
                    AI_Timer = -(lightningball_CD);
                    AI_State = State_Rush;
                }
            }
        }

        private const int frameRate = 6;

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= frameRate)
            {
                npc.frameCounter = 0;
                if (++currentFrame >= Main.npcFrameCount[npc.type])
                {
                    currentFrame = 0;
                }

                npc.frame.Y = currentFrame * frameHeight;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            float extraDrawY = Main.NPCAddHeight(npc.whoAmI);
            float drawOffsetX = -30 * npc.spriteDirection;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Vector2 origin = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
            Vector2 bodyPosition = new Vector2(npc.position.X - Main.screenPosition.X + npc.width / 2 - (float)Main.npcTexture[npc.type].Width * npc.scale / 2f + origin.X * npc.scale - drawOffsetX,
                npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY + origin.Y * npc.scale + npc.gfxOffY);

            // Draw Wings

            Texture2D wingR = mod.GetTexture("NPCs/Benares/Benares_WingR");
            Texture2D wingL = mod.GetTexture("NPCs/Benares/Benares_WingL");

            Vector2 wingRPos = bodyPosition;
            //wingRPos.X -= 140 * npc.spriteDirection;
            Vector2 wingLPos = bodyPosition;
            //wingLPos.X += 250 * npc.spriteDirection;

            wingRPos.X = npc.spriteDirection == -1 ? wingLPos.X + 140 : wingLPos.X - 220;
            wingLPos.X = npc.spriteDirection == -1 ? wingLPos.X - 250 : wingLPos.X + 110;

            wingRPos.Y -= 40;
            wingLPos.Y -= 40;

            //float heightOffset = 40;

            //float raidusR = Vector2.Distance(bodyPosition, wingRPos);
            //float raidusL = Vector2.Distance(bodyPosition, wingLPos);

            //float offsetCircleRadiusR = (float)Math.Sqrt(Math.Pow(raidusR, 2) + Math.Pow(heightOffset, 2));
            //float offsetAngleR = (float)Math.Asin(heightOffset / offsetCircleRadiusR);
            //Vector2 wingROffset = new Vector2((float)(Math.Cos(offsetAngleR) * offsetCircleRadiusR), (float)(Math.Sin(offsetAngleR) * offsetCircleRadiusR));

            //Main.NewText("R" + raidusR);
            //Main.NewText("L" + raidusL);

            //wingRPos = bodyPosition + new Vector2((float)(Math.Cos(npc.rotation) * raidusR), (float)(Math.Sin(npc.rotation + offsetCircleRadiusR) * offsetCircleRadiusR));
            ////wingRPos = bodyPosition + wingROffset;
            //wingLPos = bodyPosition - new Vector2((float)(Math.Cos(npc.rotation) * raidusL), (float)(Math.Sin(npc.rotation) * raidusL));

            //Main.NewText(wingLPos.X);

            Rectangle wingRFrame = new Rectangle(0, 0, 314, 330);
            wingRFrame.Y = currentFrame * wingRFrame.Height;

            Main.spriteBatch.Draw(wingR,
                wingRPos,
                wingRFrame,
                npc.GetAlpha(drawColor), npc.rotation, origin, npc.scale, spriteEffects, 0f);


            Rectangle wingLFrame = new Rectangle(0, 0, 394, 336);
            wingLFrame.Y = currentFrame * wingLFrame.Height;

            Main.spriteBatch.Draw(wingL,
                wingLPos,
                wingLFrame,
                npc.GetAlpha(drawColor), npc.rotation, origin, npc.scale, spriteEffects, 0f);



            // Draw Body
            Main.spriteBatch.Draw(Main.npcTexture[npc.type],
                bodyPosition,
                npc.frame,
                npc.GetAlpha(drawColor), npc.rotation, origin, npc.scale, spriteEffects, 0f);
            if (npc.color != default(Color))
            {
                Main.spriteBatch.Draw(Main.npcTexture[npc.type], new Vector2(npc.position.X - Main.screenPosition.X + npc.width / 2 - Main.npcTexture[npc.type].Width * npc.scale / 2f + origin.X * npc.scale, npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY + origin.Y * npc.scale + npc.gfxOffY), npc.frame, npc.GetColor(drawColor), npc.rotation, origin, npc.scale, spriteEffects, 0f);
            }

            return false;
            //return base.PreDraw(spriteBatch, drawColor);
        }
    }
}
