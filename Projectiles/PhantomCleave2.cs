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
	public class PhantomCleave2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("PhantomCleave1");
		}

		public override void SetDefaults() {
			projectile.width = 144;
			projectile.height = 56;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 15;
			projectile.tileCollide = false;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void AI() {
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.IceTorch, -projectile.velocity.X, -projectile.velocity.Y, Scale:3f);
			dust.noGravity = true;
			Lighting.AddLight(projectile.Center, 1f, 0.97f, 0.40f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		/*        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
				{
					Main.PlaySound(SoundID.Item105, projectile.position);
					base.OnHitNPC(target, damage, knockback, crit);
				}*/
	}
}
