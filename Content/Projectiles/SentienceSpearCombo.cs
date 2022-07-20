using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;

namespace OtakuTech.Content.Projectiles
{
	public class SentienceSpearCombo : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SentienceSpearCombo");
		}

		public override void SetDefaults() {
			Projectile.width = 192;
			Projectile.height = 192;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 20;
			Projectile.tileCollide = false;
			Projectile.alpha = 80;
            //projectile.scale = 0.75f;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];
			ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
			target.immune[Projectile.owner] = 6;
			base.OnHitNPC(target, damage, knockback, crit);
		}

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			damage /= 6;
            //base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void AI() {
			Player player = Main.player[Projectile.owner];

			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			//projectile.rotation += 0.4f;

			Projectile.rotation += (float)Projectile.direction;
			Projectile.Center = player.Center;
			//Projectile.scale *= 1.0001f;
			Projectile.alpha += 5;

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
				//Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.5f;
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.8f;
				Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation+0.2f*k, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
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
