using OtakuTech.Items.Weapons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OtakuTech.Projectiles.FerventCombo
{
	public class FerventCombo4 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Fervent Combo");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults() {
			projectile.width = 90;
			projectile.height = 90;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.ownerHitCheck = false;
			projectile.scale = 1f;
			projectile.alpha = 50;
			projectile.timeLeft = 60;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			damage *= 2;
			target.immune[projectile.owner] = 4;
			base.OnHitNPC(target, damage, knockback, crit);
		}

		public override void AI() {
			//Player player = Main.player[projectile.owner];
			projectile.alpha += 5;
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			//Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.IceTorch, Scale:4f);
			//dust.noGravity = true;
			//dust.fadeIn = 2f;
			////Main.dust[dust].scale = 1f;
			////Main.dust[dust].noGravity = true;
			Lighting.AddLight(projectile.position, 1f, 0.97f, 0.40f);
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
