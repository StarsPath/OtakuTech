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
	public class RoseFinch2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("RoseFinch2");
		}

		public override void SetDefaults() {
			projectile.width = 128;
			projectile.height = 128;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 40;
			projectile.tileCollide = false;
			//projectile.alpha = 150;
			//projectile.scale = 0.75f;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Player player = Main.player[projectile.owner];
			ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
			if (modplayer.eightFormations)
			{
				player.ManaEffect(3);
				player.statMana += 3;
			}
			base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI() {
			Player player = Main.player[projectile.owner];

			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			//projectile.rotation += 0.4f;

			projectile.rotation += (float)projectile.direction;

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

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);

			float num284 = MathHelper.Clamp(projectile.timeLeft / 120f, 0f, 1f);

			Color value71 = new Color(145, 250, 255);
			Color color72 = Color.Lerp(Color.Transparent, value71, 200f);
			color72.A = (byte)((float)(int)color72.A * 0.5f);
			color72 *= num284;

			for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                //Color color = value71 * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color72, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
		/*        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
				{
					Main.PlaySound(SoundID.Item105, projectile.position);
					base.OnHitNPC(target, damage, knockback, crit);
				}*/
	}
}
