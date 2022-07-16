using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles
{
	public class HlersSerenityProjectileJavlin : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("HlersSerenityProjectileJavlin");
		}

		public override void SetDefaults() {
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.penetrate = -1;
			Projectile.scale = 1.3f;
			Projectile.alpha = 0;

			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = true;
			Projectile.friendly = true;
		}

        public override void AI()
        {
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			for(int i = 0; i < 3; i++)
            {
				int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.BlueCrystalShard);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;

			}
			base.AI();
        }
    }
}
