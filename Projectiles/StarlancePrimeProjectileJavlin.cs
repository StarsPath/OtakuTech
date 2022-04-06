using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Projectiles
{
	public class StarlancePrimeProjectileJavlin : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("StarlancePrimeProjectileJavlin");
		}

		public override void SetDefaults() {
			projectile.width = 18;
			projectile.height = 18;
			projectile.penetrate = -1;
			projectile.scale = 1.3f;
			projectile.alpha = 0;

			projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.scale = 1.5f;
		}

        public override void AI()
        {
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			for(int i = 0; i < 3; i++)
            {
				int dust = Dust.NewDust(projectile.Center, 0, 0, DustID.BlueCrystalShard);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;

			}
			base.AI();
        }
    }
}
