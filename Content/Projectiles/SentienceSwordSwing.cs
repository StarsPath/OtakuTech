using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;

namespace OtakuTech.Content.Projectiles
{
	public class SentienceSwordSwing : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SentienceSwordSwing");
			Main.projFrames[Projectile.type] = 8;
		}

		//public int timeLeftTotal = 20;
		public override void SetDefaults() {
			Projectile.width = 128;
			Projectile.height = 128;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			//Projectile.timeLeft = timeLeftTotal;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			//Projectile.scale = 1.5f;
			//Projectile.alpha = 70;
            //projectile.scale = 0.75f;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];
			ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
			target.immune[Projectile.owner] = 20;
			base.OnHitNPC(target, damage, knockback, crit);
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Projectile.Center = player.Center;
			if (++Projectile.frameCounter >= 2)
			{
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
				{
					//projectile.frame = 0;
					Projectile.Kill();
				}
				Projectile.frameCounter = 0;
			}

			Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : -1));

			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (Projectile.spriteDirection == -1)
				Projectile.rotation += MathHelper.Pi;

			int dust;
			// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
			Vector2 position = player.position - new Vector2(64, 64);
			dust = Dust.NewDust(position, 128, 128, DustID.GemRuby, 0f, 0f, 0, Color.White, 0.6f);
			Main.dust[dust].noGravity = true;
			Lighting.AddLight(Projectile.position, 0.9f, 0.17f, 0.17f);

		}

		public override bool PreDraw(ref Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			int frameHeight = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int startY = frameHeight * Projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)((Projectile.spriteDirection == 1) ? (sourceRectangle.Width - 64) : 64);

			Color drawColor = Projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
			Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
			sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

			return false;
		}
	}
}
