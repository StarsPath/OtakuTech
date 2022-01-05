using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Projectiles.Minions
{
	// PurityWisp uses inheritace as an example of how it can be useful in modding.
	// HoverShooter and Minion classes help abstract common functionality away, which is useful for mods that have many similar behaviors.
	// Inheritance is an advanced topic and could be confusing to new programmers, see ExampleSimpleMinion.cs for a simpler minion example.
	public class ExcelsisKingMinion : HoverMelee2
	{
		public override void SetStaticDefaults() {
			Main.projFrames[projectile.type] = 16;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = false; //This is necessary for right-click targeting
		}

		public override void SetDefaults() {
			projectile.netImportant = true;
			projectile.width = 40;
			projectile.height = 90;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;

            drawOffsetX = -52;
            drawOriginOffsetY = -40;
            drawOriginOffsetX = -33;
        }

		public override void CheckActive() {
			Player player = Main.player[projectile.owner];
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.dead) {
				modPlayer.excelsisKing = false;
			}
			if (modPlayer.excelsisKing) { // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
				projectile.timeLeft = 2;
			}
		}
		public override void AI()
		{
			base.AI();
			Lighting.AddLight(projectile.position, 0.28f, 0.82f, 1f);
		}
	}
}
