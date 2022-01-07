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
	public class TestProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("TestProjectile");
			//ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults() {
			projectile.width = 48;
			projectile.height = 48;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 15;
			projectile.tileCollide = false;

			drawOriginOffsetY = -96;
		}
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
			int precision = 2;
			for (int j = 1; j <= 2 * precision; j++)
			{
				Rectangle rectangle = projHitbox;
				Vector2 vector5 = projectile.velocity.SafeNormalize(Vector2.Zero) * projectile.width / precision * j;
				rectangle.Offset((int)vector5.X, (int)vector5.Y);
				if (rectangle.Intersects(targetHitbox))
				{
					return true;
				}
			}
			return false;
			//return base.Colliding(projHitbox, targetHitbox);
        }

        public override void AI() {
			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			//projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			//if (projectile.velocity.Y > 16f)
			//{
			//	projectile.velocity.Y = 16f;
			//}
			//// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			//if (projectile.spriteDirection == -1)
			//	projectile.rotation += MathHelper.Pi;

			//Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.IceTorch, -projectile.velocity.X, -projectile.velocity.Y, Scale: 3f);
			//dust.noGravity = true;
			//Lighting.AddLight(projectile.Center, 1f, 0.97f, 0.40f);
		}

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
	}
}

