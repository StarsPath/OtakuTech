using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;

namespace OtakuTech.Content.Projectiles
{
	public class RoseFinch2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("RoseFinch2");
		}

		public override void SetDefaults() {
			Projectile.width = 128;
			Projectile.height = 128;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 40;
			Projectile.tileCollide = false;
			//projectile.alpha = 150;
			//projectile.scale = 0.75f;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Player player = Main.player[Projectile.owner];
			ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
			if (modplayer.eightFormations)
			{
				player.ManaEffect(3);
				player.statMana += 3;
			}
			base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI() {
			Player player = Main.player[Projectile.owner];

			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			//projectile.rotation += 0.4f;

			Projectile.rotation += (float)Projectile.direction;
			Projectile.velocity *= 0.95f;
			Projectile.scale *= 1.01f;

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

			float num284 = MathHelper.Clamp(Projectile.timeLeft / 120f, 0f, 1f);

			Color value71 = new Color(145, 250, 255);
			Color color72 = Color.Lerp(Color.Transparent, value71, 200f);
			color72.A = (byte)((float)(int)color72.A * 0.5f);
			color72 *= num284;

			for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                //Color color = value71 * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color72, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
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
