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
	public class EightFormations : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Eight Formations");
			//ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults() {
			projectile.width = 120;
			projectile.height = 360;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 13 * 60;
			projectile.tileCollide = true;
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			return false;
            //return base.OnTileCollide(oldVelocity);
        }

        public override void AI() {
			projectile.ai[0]--;
			projectile.timeLeft = (int)projectile.ai[0];
			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
		}

        //     public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //     {
        //Color colorA = default;
        //Color colorTransparent = Color.Transparent;
        //Color colorC = Color.Lerp(colorTransparent, colorA, 2f);

        //Texture2D texture = Main.projectileTexture[projectile.type];
        ////Texture2D texture = mod.GetTexture("NPCs/Benares/Benares_WingR");
        //Vector2 position = projectile.position;
        //Rectangle rect = texture.Frame();
        //Vector2 spinningPoint = Vector2.UnitY.RotatedBy(projectile.timeLeft * 0.1f);

        //float rotation = projectile.timeLeft;
        //Vector2 origin = rect.Size()/2;
        //float scale = 1f;

        //Main.spriteBatch.Draw(texture, position, rect, lightColor, rotation, origin, scale, SpriteEffects.None, 0);

        ////for (int i = 0; i < 20; i++)
        ////         {
        ////	position.Y -= 0.5f;
        ////	//rotation = projectile.timeLeft;
        ////	rotation = -(float)Math.PI / 50f * projectile.timeLeft;
        ////	//rotation = spinningPoint.RotatedBy()
        ////	scale -= 0.15f;
        ////	//Main.NewText("DRAW" + i);
        ////	Main.spriteBatch.Draw(texture, position, rect, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
        ////	//Main.NewText(position);
        ////}

        ////spriteBatch.Draw(texture, position, rect, colorC, rotation, origin, scale, SpriteEffects.None, 0);
        //         return false;
        //     }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			float num280 = 900f;
			if (projectile.type == 657)
			{
				num280 = 300f;
			}
			float num281 = 15f;
			float num282 = 15f;
			float num283 = projectile.ai[0];
			float num284 = MathHelper.Clamp(num283 / 30f, 0f, 1f);
			if (num283 > num280 - 60f)
			{
				num284 = MathHelper.Lerp(1f, 0f, (num283 - (num280 - 60f)) / 60f);
			}
			Point point5 = projectile.Center.ToTileCoordinates();
			Collision.ExpandVertically(point5.X, point5.Y, out var topY, out var bottomY, (int)num281, (int)num282);
			topY++;
			bottomY--;
			float num285 = 0.2f;
			Vector2 value68 = new Vector2(point5.X, topY) * 16f + new Vector2(8f);
			Vector2 value69 = new Vector2(point5.X, bottomY) * 16f + new Vector2(8f);
			Vector2.Lerp(value68, value69, 0.5f);
			Vector2 vector67 = new Vector2(0f, value69.Y - value68.Y);
			vector67.X = vector67.Y * num285;
			new Vector2(value68.X - vector67.X / 2f, value68.Y);
			//Texture2D value70 = TextureAssets.Projectile[projectile.type].get_Value();
			Texture2D value70 = mod.GetTexture("Projectiles/EightFormations");
			//Texture2D value70 = mod.GetTexture("NPCs/Benares/Benares_WingR");
			Rectangle rectangle18 = value70.Frame();
			Vector2 origin16 = rectangle18.Size() / 2f;
			float num286 = -(float)Math.PI / 50f * num283;
			Vector2 spinningpoint4 = Vector2.UnitY.RotatedBy(num283 * 0.1f);
			float num287 = 0f;
			float num288 = 5.1f;
			Color value71 = new Color(145, 250, 255);
			for (float num289 = (int)value69.Y; num289 > (float)(int)value68.Y; num289 -= num288)
			{
				num287 += num288;
				float num290 = num287 / vector67.Y;
				float num291 = num287 * ((float)Math.PI * 2f) / -20f;
				float num292 = num290 - 0.15f;
				Vector2 position18 = spinningpoint4.RotatedBy(num291);
				Vector2 vector68 = new Vector2(0f, num290 + 1f);
				vector68.X = vector68.Y * num285;
				Color color72 = Color.Lerp(Color.Transparent, value71, num290 * 2f);
				if (num290 > 0.5f)
				{
					color72 = Color.Lerp(Color.Transparent, value71, 2f - num290 * 2f);
				}
				color72.A = (byte)((float)(int)color72.A * 0.5f);
				color72 *= num284;
				position18 *= vector68 * 100f;
				position18.Y = 0f;
				position18.X = 0f;
				position18 += new Vector2(value69.X, num289) - Main.screenPosition;
				Main.spriteBatch.Draw(value70, position18, rectangle18, color72, num286 + num291, origin16, 2f + num292, SpriteEffects.None, 0);
				//Main.EntitySpriteDraw(value70, position18, rectangle18, color72, num286 + num291, origin16, 1f + num292, SpriteEffects.None, 0);
			}
			//return;
			//return true;
            //return base.PreDraw(spriteBatch, lightColor);
        }
    }
}

