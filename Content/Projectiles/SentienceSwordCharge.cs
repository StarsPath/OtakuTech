using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;
using Terraria.DataStructures;

namespace OtakuTech.Content.Projectiles
{
	public class SentienceSwordCharge : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SentienceSwordCharge");
		}

		public override void SetDefaults() {
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.ignoreWater = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.timeLeft = 30;
            Projectile.scale = 2f;
		}

		//     public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		//     {
		//Projectile.damage = (int)(damage * 0.9f);
		//base.OnHitNPC(target, damage, knockback, crit);
		//     }

		public int interval = 4;

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void AI() {
			Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
			Projectile.timeLeft = player.immuneTime;

            if (Projectile.ai[0] >= interval)
            {
				Projectile.NewProjectile(new EntitySource_Misc("SentienceSwordCharge"), Projectile.Center + new Vector2(0, -40), new Vector2(0, 10).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-10, 10))), ModContent.ProjectileType<SentienceSwordProjectile>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
				Projectile.ai[0] = 0;
            }

			Projectile.ai[0]++;
        }
    }
}
