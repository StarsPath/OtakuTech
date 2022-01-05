using OtakuTech.Items.Weapons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace OtakuTech.Projectiles
{
	public class LightningSpear : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("LightningSpear");
		}

		public override void SetDefaults() {
			projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void AI() {
			//Player player = Main.player[projectile.owner];

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			Dust dust = Dust.NewDustPerfect(projectile.position, DustID.TopazBolt);
			dust.noGravity = true;
			//Main.dust[dust].scale = 1f;
			//Main.dust[dust].noGravity = true;
			Lighting.AddLight(projectile.position, 1f, 0.97f, 0.40f);
		}
/*        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Main.PlaySound(SoundID.Item105, projectile.position);
            base.OnHitNPC(target, damage, knockback, crit);
        }*/
    }
}
