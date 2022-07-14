using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles
{
	public class LightningSpear : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("LightningSpear");
		}

		public override void SetDefaults() {
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 3;
			Projectile.scale = 1.2f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void AI() {
			//Player player = Main.player[projectile.owner];

			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.GemTopaz);
			dust.noGravity = true;
			//Main.dust[dust].scale = 1f;
			//Main.dust[dust].noGravity = true;
			Lighting.AddLight(Projectile.position, 1f, 0.97f, 0.40f);
		}
/*        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Main.PlaySound(SoundID.Item105, projectile.position);
            base.OnHitNPC(target, damage, knockback, crit);
        }*/
    }
}
