using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles.NPCs
{
	public class IceShard : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("IceShard");
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 48;
			Projectile.height = 48;
			Projectile.hostile = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 5 * 60;
			Projectile.alpha = 70;
			//projectile.scale = 0.8f;
   //         ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void AI() {
			//Main.NewText("projDMG "+projectile.damage);
			if (Projectile.velocity != default)
			{
				Projectile.frameCounter++;
				Projectile.frame = (Projectile.frameCounter / 3) % Main.projFrames[Projectile.type];
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
				for (int i = 0; i < 5; i++)
				{
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, Scale: 2f);
					Dust d = Main.dust[dust];
					d.noGravity = true;
					d.fadeIn = 2f;

				}
			}

			Lighting.AddLight(Projectile.Center, 0f, 1f, 1f);
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 20);
			target.AddBuff(BuffID.Slow, 120);
			base.OnHitPlayer(target, damage, crit);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
        {
			if (Projectile.ai[0] < 3)
			{
				Projectile.velocity = oldVelocity;
				for (int i = 0; i < 5; i++)
				{
					//int gore = Gore.NewGore(new EntitySource_HitEffect(Projectile), Projectile.Center, default(Vector2), Main.rand.Next(61, 64), 5f);
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, Scale: 5f);
					Dust d = Main.dust[dust];
					d.noGravity = true;
					//d.fadeIn = 2f;
					Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
					SoundEngine.PlaySound(SoundID.DeerclopsIceAttack, Projectile.position);
				}
			}
			if (Projectile.ai[0] >= 3)
			{
				Projectile.velocity = default;
			}
			Projectile.ai[0]++;
			return false;
			//return base.OnTileCollide(oldVelocity);
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
