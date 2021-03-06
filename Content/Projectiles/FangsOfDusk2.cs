using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;

namespace OtakuTech.Content.Projectiles
{
	public class FangsOfDusk2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("FangsOfDusk");
		}

		public override void SetDefaults() {
			Projectile.width = 64;
			Projectile.height = 64;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 20;
			Projectile.tileCollide = false;
			Projectile.alpha = 150;
			//projectile.scale = 0.75f;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Player player = Main.player[Projectile.owner];
			ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
			if(modplayer.enchanced)
				target.immune[Projectile.owner] = 5;
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI() {
			Player player = Main.player[Projectile.owner];

			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			//projectile.rotation += 0.4f;

			Projectile.rotation += (float)Projectile.direction;

			
			if (Projectile.ai[1] == 1)
            {
				Projectile.Center = Vector2.Lerp(Projectile.Center, player.Center, 0.5f);
				//Vector2 dir = projectile.DirectionTo(player.Center);
				//dir.Normalize();
				//projectile.velocity = dir * 18f;
			}
			

			if(Projectile.ai[0] >= 0)
            {
				Projectile.ai[0]++;
				Vector2 dir = player.position - Main.MouseWorld;
				dir.Normalize();
				Vector2 position = player.position - (dir * 120f);
				Projectile.Center = Vector2.Lerp(Projectile.Center, position, 0.2f);
			}
				
			if (Projectile.ai[0] >= 15)
			{
				//projectile.velocity.X *= -1;
				//projectile.velocity.Y *= -1;
				Projectile.ai[0] = -1;
				Projectile.ai[1] = 1;
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

		public override bool PreDraw(ref Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
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
