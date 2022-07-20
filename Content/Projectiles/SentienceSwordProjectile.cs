using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;

namespace OtakuTech.Content.Projectiles
{
	public class SentienceSwordProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SentienceSwordProjectile");
		}

		public override void SetDefaults() {
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.ignoreWater = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.timeLeft = 30 * 5;
			Projectile.scale = 0.8f;
			Projectile.alpha = 70;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

   //     public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
   //     {
			//Projectile.damage = (int)(damage * 0.9f);
			//base.OnHitNPC(target, damage, knockback, crit);
   //     }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			damage /= 4;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

		public override void AI() {
			//Player player = Main.player[projectile.owner];

			//if (Projectile.ai[0] >= 2)
			//{
			//	Projectile.velocity = default;
			//}
			//if (Projectile.velocity.X == 0 || Projectile.velocity.Y == 0)
			//{
			//	Projectile.velocity = default;
			//}
			Player player = Main.player[Projectile.owner];
			if (Projectile.velocity != default)
			{
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			}

			//Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			for (int i = 0; i < 2; i++)
			{
				int dust = Dust.NewDust(Projectile.position, 0, 0, DustID.GemRuby);
				Main.dust[dust].scale = 0.6f;
				Main.dust[dust].noGravity = true;
			}
			Lighting.AddLight(Projectile.position, 0.9f, 0.17f, 0.17f);
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			if (Projectile.ai[0] < 2)
				Projectile.velocity = oldVelocity;
			if (Projectile.ai[0] >= 2)
				Projectile.velocity = default;
			Projectile.ai[0]++;
			return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.7f;
				Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
    }
}
