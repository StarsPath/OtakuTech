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
			projectile.width = 60;
			projectile.height = 60;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 15 * 60;
			projectile.tileCollide = true;

		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			return false;
            //return base.OnTileCollide(oldVelocity);
        }

        public override void AI() {
			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Color colorA = default;
			Color colorTransparent = Color.Transparent;
			Color colorC = Color.Lerp(colorTransparent, colorA, 2f);

			Texture2D texture = Main.projectileTexture[projectile.type];
			Vector2 position = default;
			Rectangle rect = texture.Frame();

			float rotation = default;
			Vector2 origin = default;
			float scale = 1f;

			spriteBatch.Draw(texture, position, rect, colorC, rotation, origin, scale, SpriteEffects.None, 0);
            return true;
        }
    }
}

