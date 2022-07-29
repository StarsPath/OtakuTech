using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;
using Terraria.Audio;

namespace OtakuTech.Content.Projectiles
{
	public class CommandmentsProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Commandments");
		}

		public override void SetDefaults() {
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 4;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 2;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];
			ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
			if (modplayer.enchanced)
				target.immune[Projectile.owner] = 5;
			base.OnHitNPC(target, damage, knockback, crit);
		}

		public override void AI() {
			//Player player = Main.player[projectile.owner];

			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
			Main.dust[dust].scale = 0.5f;
			Main.dust[dust].noGravity = true;
			Lighting.AddLight(Projectile.position, 1f, 0.97f, 0.40f);
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.GemTopaz, oldVelocity.X/2, oldVelocity.Y/2);
			dust.scale = 1.5f;
            dust.noGravity = true;
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			return base.OnTileCollide(oldVelocity);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
    }
}
