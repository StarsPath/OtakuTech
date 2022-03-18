using OtakuTech.Items.Weapons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OtakuTech.Projectiles.NPCs
{
	public class Fireball : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Fireball");
		}

		public override void SetDefaults() {
			projectile.width = 128;
			projectile.height = 128;
			projectile.hostile = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 5 * 60;
   //         ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void AI() {
			//Main.NewText("projDMG "+projectile.damage);
			projectile.rotation += 0.4f;

			for(int i = 0; i < 5; i++)
            {
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, Scale: 5f);
				Dust d = Main.dust[dust];
				d.noGravity = true;
				d.fadeIn = 2f;

			}
			Lighting.AddLight(projectile.position, 1f, 0.97f, 0.40f);
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			for (int i = 0; i < 5; i++)
			{
				int gore = Gore.NewGore(projectile.Center, default(Vector2), Main.rand.Next(61, 64), 5f);
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Smoke, Scale: 5f);
				Dust d = Main.dust[dust];
				d.noGravity = true;
				d.fadeIn = 2f;
			}
			return base.OnTileCollide(oldVelocity);
        }

  //      public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
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
