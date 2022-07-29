using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;

namespace OtakuTech.Content.Projectiles
{
	public class SentienceChainExtra2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SentienceChainExtra2");
		}

		public override void SetDefaults() {
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}
    }
}
