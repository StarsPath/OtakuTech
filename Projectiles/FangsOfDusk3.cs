using OtakuTech.Items.Weapons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OtakuTech.Projectiles
{
    public class FangsOfDusk3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FangsOfDusk");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 112;
            projectile.height = 112;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 30;
            projectile.tileCollide = false;
            projectile.alpha = 50;
            projectile.scale = 1f;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
        }

        public bool toggle = false;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            toggle = true;
            Player player = Main.player[projectile.owner];
            ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
            target.immune[projectile.owner] = 8;
            if (modplayer.enchanced)
                target.immune[projectile.owner] = 2;
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (toggle)
                if (++projectile.frameCounter >= 4)
                {
                    projectile.frameCounter = 0;
                    if (projectile.frame < Main.projFrames[projectile.type]-1)
                    {
                        projectile.frame++;
                    }
                }

            //projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            //projectile.rotation += 0.4f;

            projectile.rotation += (float)projectile.direction;


            if (projectile.ai[1] == 1)
            {
                projectile.Center = Vector2.Lerp(projectile.Center, player.Center, 0.5f);
                //Vector2 dir = projectile.DirectionTo(player.Center);
                //dir.Normalize();
                //projectile.velocity = dir * 18f;
            }


            if (projectile.ai[0] >= 0)
            {
                projectile.ai[0]++;
                Vector2 dir = player.position - Main.MouseWorld;
                dir.Normalize();
                Vector2 position = player.position - (dir * 120f);
                projectile.Center = Vector2.Lerp(projectile.Center, position, 0.2f);
            }

            if (projectile.ai[0] >= 24)
            {
                //projectile.velocity.X *= -1;
                //projectile.velocity.Y *= -1;
                projectile.ai[0] = -1;
                projectile.ai[1] = 1;
            }

            //int numParticle = 16;
            //int radius = 28;
            //double angle = 2 * Math.PI / numParticle;

            //for (int i = 0; i < numParticle; i++)
            //{
            //	Dust d = Dust.NewDustPerfect(projectile.Center + new Vector2(radius * (float)Math.Cos(angle*i), radius * (float)Math.Sin(angle * i)), DustID.PurpleTorch);
            //	d.noGravity = true;
            //}

            //Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.IceTorch, Scale:2f);
            //dust.noGravity = true;
            //Main.dust[dust].scale = 1f;
            //Main.dust[dust].noGravity = true;
            //Lighting.AddLight(projectile.position, 1f, 0.97f, 0.40f);
        }

        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //    //Redraw the projectile with the color not influenced by light
        //    Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height/3 * 0.5f);
        //    for (int k = 0; k < projectile.oldPos.Length; k++)
        //    {
        //        Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
        //        Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
        //        spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
        //    }
        //    return true;
        //}
        /*        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
				{
					Main.PlaySound(SoundID.Item105, projectile.position);
					base.OnHitNPC(target, damage, knockback, crit);
				}*/
    }
}
