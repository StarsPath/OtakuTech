using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.NPCs.Benares
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
        private int number_of_rush = 3;

        private float fireball_time = 3 * 60;
        private float fireball_CD = 40;
        private float fireball_interval_time = 1f * 60;
        private float fireball_shootSpeed = 40f;
        private float idle_time = 1 * 60;

        private float lightningball_time = 1 * 60;
        private float lightningball_CD = 40;
        private float lightningball_interval_time = 1f * 10;
        private float lightningball_shootSpeed = 10f;
        private float lightningball_count = 5;

        private float iceShard_time = 2 * 60;
        private float iceShard_CD = 40;
        private float iceShard_interval_time = 60;
        private float iceShard_shootSpeed = 20f;
        private float iceShard_count = 7f;

        private const int State_idle = 0;
        private const int State_Rush = 1; // Charges at the player
        private const int State_Fireball = 2;
        private const int State_Ice = 3;
        private const int State_Lightning = 4;

        private const int AI_State_Slot = 0;
        private const int AI_Timer_Slot = 1;
        private const int AI_Rage_Slot = 2;

        private int currentFrame = 0;

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

        public float AI_Rage
        {
            get => NPC.ai[AI_Rage_Slot];
            set => NPC.ai[AI_Rage_Slot] = value;
        }

        private Vector2 targetLocation;
        private Vector2 dir = Vector2.Zero;

        public override string HeadTexture => "OtakuTech/NPCs/Benares/Benares_Head_Boss";
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            DisplayName.SetDefault("Benares");
            NPCID.Sets.MustAlwaysDraw[NPC.type] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 240;
            NPC.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
            NPC.damage = 150;
            NPC.defense = 100;
            NPC.lifeMax = 50000;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.netAlways = true;
            //npc.hide = true;
            //npc.scale = 1.25f;
            //npc.alpha = 175;
            //npc.color = new Color(0, 80, 255, 100);
            //npc.value = 25f;
            DrawOffsetY = 30;

            NPC.buffImmune[BuffID.Confused] = true;
        }
        /*public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // we would like this npc to spawn in the overworld.
            return SpawnCondition.OverworldDay.Chance * 0.25f;
        }*/
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            bossLifeScale = 1 + (0.2f * numPlayers);
            base.ScaleExpertStats(numPlayers, bossLifeScale);
        }

        public override void OnKill()
        {
            //Item.NewItem(npc.position, ModContent.ItemType<Items.Materials.HonkaiShard>());
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            base.OnHitByItem(player, item, damage, knockback, crit);
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            base.OnHitByProjectile(projectile, damage, knockback, crit);
        }

        public override void AI()
        {
            //Main.NewText(npc.damage);
            NPC.TargetClosest();
            if ((Main.player[NPC.target].Center - NPC.Center).X > 0f)
            {
                NPC.spriteDirection = (NPC.direction = -1);
            }
            else if ((Main.player[NPC.target].Center - NPC.Center).X < 0f)
            {
                NPC.spriteDirection = (NPC.direction = 1);
            }

            //npc.rotation += 0.01f;

            //Main.NewText(Main.player[npc.target].Center - npc.Center);


            if (++AI_Timer >= 60 * 20)
            {
                AI_Timer = 0;
            }


            if (!NPC.HasValidTarget)
            {
                AI_State = State_idle;
                //NPC.velocity = Vector2.Zero;
                NPC.velocity = Vector2.Lerp(NPC.velocity, new Vector2(0, -20), 0.05f);
                NPC.EncourageDespawn(180);
            }
            else
            {
                if(AI_State == State_idle)
                    AI_State = State_Rush;
            }

            if (AI_State == State_Rush)
            {
                if (AI_Timer <= 0)
                {
                    targetLocation = Main.player[NPC.target].Center;
                    dir = (targetLocation - NPC.Center).SafeNormalize(Vector2.Zero);
                    NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.05f);
                    NPC.Center = new Vector2(NPC.Center.X, MathHelper.Lerp(NPC.Center.Y, targetLocation.Y, 0.05f));

                    float distance = Vector2.Distance(NPC.Center, targetLocation);
                    rush_speed_mult = rush_speed_mult_base + distance / 1600;
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                }
                else
                {
                    NPC.velocity = dir * rush_speed * rush_speed_mult;
                }

                if (AI_Timer >= rush_time)
                {
                    rush_count++;
                    AI_Timer = -(rush_CD);
                    if(rush_count >= number_of_rush)
                    {
                        rush_count = 0;
                        if (Main.rand.NextBool())
                            AI_State = State_Fireball;
                        else
                            AI_State = State_Ice;
                    }
                }
            }

            if (AI_State == State_Fireball)
            {
                if (AI_Timer <= 0)
                {
                    NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.05f);
                    NPC.Center = Vector2.Lerp(NPC.Center, targetLocation + new Vector2(0, -400f), 0.05f);
                }
                else if(AI_Timer < fireball_time + idle_time)
                {
                    targetLocation = Main.player[NPC.target].Center;
                    dir = (targetLocation - NPC.Top).SafeNormalize(Vector2.Zero);
                    if (AI_Timer % fireball_interval_time == 0)
                    {
                        if (dir != Vector2.Zero)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, dir * fireball_shootSpeed, ModContent.ProjectileType<Projectiles.NPCs.Fireball>(), (int)(NPC.damage * 0.2f), 0.1f, Main.myPlayer);
                            SoundEngine.PlaySound(SoundID.Item113, NPC.Center);
                        }
                    }
                }

                if (AI_Timer >= fireball_time + idle_time)
                {
                    AI_Timer = -(fireball_CD);
                    AI_State = State_Lightning;
                }
            }

            if (AI_State == State_Ice)
            {
                if (AI_Timer <= 0)
                {
                    NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.05f);
                    NPC.Center = Vector2.Lerp(NPC.Center, targetLocation + new Vector2(0, -400f), 0.05f);
                }
                else if (AI_Timer < iceShard_time + idle_time)
                {
                    targetLocation = Main.player[NPC.target].Center;
                    dir = (targetLocation - NPC.Top).SafeNormalize(Vector2.Zero);
                    if (AI_Timer % iceShard_interval_time == 0)
                    {
                        if (dir != Vector2.Zero)
                        {
                            //Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, dir * iceShard_shootSpeed, ModContent.ProjectileType<Projectiles.NPCs.IceShard>(), (int)(NPC.damage * 0.1f), 0.1f, Main.myPlayer);
                            //SoundEngine.PlaySound(SoundID.Item9, NPC.Center);

                            int spread = 20; //degrees
                            //float numberProjectiles = 5;
                            float rotation = MathHelper.ToRadians(spread);
                            //npc.Top += dir * lightningball_shootSpeed * spread;
                            for (int i = 0; i < iceShard_count; i++)
                            {
                                float s = (float)Math.Sin(Math.PI / iceShard_count * i) * 1f + 0.6f;
                                Vector2 perturbedSpeed = (dir * iceShard_shootSpeed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (iceShard_count - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                                //Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage / 2, knockBack, player.whoAmI);
                                Projectile p = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Top, perturbedSpeed, ModContent.ProjectileType<Projectiles.NPCs.IceShard>(), (int)(NPC.damage * 0.1f), 0.1f, Main.myPlayer);
                                p.scale = s;
                                SoundEngine.PlaySound(SoundID.Item115, NPC.Center);
                            }
                        }
                    }
                }

                if (AI_Timer >= iceShard_time + idle_time)
                {
                    AI_Timer = -(iceShard_CD);
                    AI_State = State_Lightning;
                }
            }

            if (AI_State == State_Lightning)
            {
                if (AI_Timer <= 0)
                {
                    NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.05f);
                    NPC.Center = Vector2.Lerp(NPC.Center, targetLocation + new Vector2(0, -300f), 0.05f);
                }
                else if (AI_Timer < lightningball_time + idle_time)
                {
                    targetLocation = Main.player[NPC.target].Center;
                    dir = (targetLocation - NPC.Top).SafeNormalize(Vector2.Zero);
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
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top.X, NPC.Top.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Projectiles.NPCs.LightningBall>(), (int)(NPC.damage* 0.08f), 0.1f, Main.myPlayer);
                                SoundEngine.PlaySound(SoundID.Item115, NPC.Center);
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
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter >= frameRate)
            {
                NPC.frameCounter = 0;
                if (++currentFrame >= Main.npcFrameCount[NPC.type])
                {
                    currentFrame = 0;
                }

                NPC.frame.Y = currentFrame * frameHeight;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            float extraDrawY = Main.NPCAddHeight(NPC);
            float drawOffsetX = -30 * NPC.spriteDirection;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Vector2 origin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width / 2, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2);
            Vector2 bodyPosition = new Vector2(NPC.position.X - Main.screenPosition.X + NPC.width / 2 - (float)TextureAssets.Npc[NPC.type].Value.Width * NPC.scale / 2f + origin.X * NPC.scale - drawOffsetX,
                NPC.position.Y - Main.screenPosition.Y + NPC.height - TextureAssets.Npc[NPC.type].Value.Height * NPC.scale / Main.npcFrameCount[NPC.type] + 4f + extraDrawY + origin.Y * NPC.scale + NPC.gfxOffY);

            // Draw Wings

            //Texture2D wingR = Mod.GetTexture("NPCs/Benares/Benares_WingR");
            Texture2D wingR = ModContent.Request<Texture2D>("OtakuTech/Content/NPCs/Benares/Benares_WingR").Value;
            //Texture2D wingL = Mod.GetTexture("NPCs/Benares/Benares_WingL");
            Texture2D wingL = ModContent.Request<Texture2D>("OtakuTech/Content/NPCs/Benares/Benares_WingL").Value;

            Vector2 wingRPos = bodyPosition;
            //wingRPos.X -= 140 * npc.spriteDirection;
            Vector2 wingLPos = bodyPosition;
            //wingLPos.X += 250 * npc.spriteDirection;

            wingRPos.X = NPC.spriteDirection == -1 ? wingLPos.X + 140 : wingLPos.X - 220;
            wingLPos.X = NPC.spriteDirection == -1 ? wingLPos.X - 250 : wingLPos.X + 110;

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
                NPC.GetAlpha(drawColor), NPC.rotation, origin, NPC.scale, spriteEffects, 0f);


            Rectangle wingLFrame = new Rectangle(0, 0, 394, 336);
            wingLFrame.Y = currentFrame * wingLFrame.Height;

            Main.spriteBatch.Draw(wingL,
                wingLPos,
                wingLFrame,
                NPC.GetAlpha(drawColor), NPC.rotation, origin, NPC.scale, spriteEffects, 0f);



            // Draw Body
            Main.spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value,
                bodyPosition,
                NPC.frame,
                NPC.GetAlpha(drawColor), NPC.rotation, origin, NPC.scale, spriteEffects, 0f);
            if (NPC.color != default(Color))
            {
                Main.spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, new Vector2(NPC.position.X - Main.screenPosition.X + NPC.width / 2 - TextureAssets.Npc[NPC.type].Value.Width * NPC.scale / 2f + origin.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + NPC.height - TextureAssets.Npc[NPC.type].Value.Height * NPC.scale / Main.npcFrameCount[NPC.type] + 4f + extraDrawY + origin.Y * NPC.scale + NPC.gfxOffY), NPC.frame, NPC.GetColor(drawColor), NPC.rotation, origin, NPC.scale, spriteEffects, 0f);
            }

            return false;
            //return base.PreDraw(spriteBatch, drawColor);
        }
    }
}
