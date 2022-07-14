using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OtakuTech.Content.Projectiles
{
    public class FeatherBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FeatherBlade");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 7 * 60;
            Projectile.ignoreWater = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        public override void AI()
        {

            if(Projectile.velocity.X == 0 || Projectile.velocity.Y == 0)
            {
                Projectile.velocity = default;
            }
            Player player = Main.player[Projectile.owner];
            if(Projectile.velocity != default)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(225f);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, default, 0.05f);
            }
                

            if (Projectile.ai[0] == 1)
            {
                Vector2 dir = -(Projectile.Center - player.Center);
                dir.Normalize();
                Projectile.velocity = dir * 20f;
                //if(projectile.Hitbox)
                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    Projectile.Kill();
                }
                //projectile.Center = Vector2.Lerp(projectile.Center, player.Center, 0.5f);
            }

            int d = Dust.NewDust(Projectile.Center, 0, 0, DustID.RedTorch, Scale: 2f);
            Main.dust[d].noGravity = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //projectile.velocity = default;
            return false;
            //return base.OnTileCollide(oldVelocity);
        }

        //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        //{
        //	target.immune[projectile.owner] = 5;
        //	base.OnHitNPC(target, damage, knockback, crit);
        //}

        //public override void AI() {
        //	//Player player = Main.player[projectile.owner];

        //	projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

        //	Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.IceTorch, Scale:2f);
        //	dust.noGravity = true;
        //	dust.fadeIn = 2f;
        //	//Main.dust[dust].scale = 1f;
        //	//Main.dust[dust].noGravity = true;
        //	Lighting.AddLight(projectile.position, 1f, 0.97f, 0.40f);
        //}

        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //	//Redraw the projectile with the color not influenced by light
        //	Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
        //	for (int k = 0; k < projectile.oldPos.Length; k++)
        //	{
        //		Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
        //		Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
        //		spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
        //	}
        //	return true;
        //}
        /*        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
				{
					Main.PlaySound(SoundID.Item105, projectile.position);
					base.OnHitNPC(target, damage, knockback, crit);
				}*/
    }
}
