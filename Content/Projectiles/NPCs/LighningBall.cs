using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles.NPCs
{
	public class LightningBall : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Lightning Ball");
		}

		public override void SetDefaults() {
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.hostile = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 5 * 60;
			//projectile.tileCollide = false;
			Projectile.alpha = 70;
			//projectile.scale = 0.5f;
			//ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void AI() {
			//Main.NewText("projDMG "+projectile.damage);
			Projectile.rotation += 0.4f;

			for(int i = 0; i < 2; i++)
            {
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Scale: 0.4f);
				Dust d = Main.dust[dust];
				d.noGravity = true;
				//d.fadeIn = 1f;

			}
			Lighting.AddLight(Projectile.position, 1f, 0.97f, 0.40f);
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 5; i++)
            {
				//int gore = Gore.NewGore(projectile.Center, default(Vector2), Main.rand.Next(61, 64), 5f);
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Scale: 2f);
				dust.noGravity = true;
				//dust.fadeIn = 2f;
            }
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
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
