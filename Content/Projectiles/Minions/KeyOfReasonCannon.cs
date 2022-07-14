using OtakuTech.Common.Players;
using OtakuTech.Projectiles.Minions;
using Terraria;
using Terraria.ID;

namespace OtakuTech.Content.Projectiles.Minions
{
	// PurityWisp uses inheritace as an example of how it can be useful in modding.
	// HoverShooter and Minion classes help abstract common functionality away, which is useful for mods that have many similar behaviors.
	// Inheritance is an advanced topic and could be confusing to new programmers, see ExampleSimpleMinion.cs for a simpler minion example.
	public class KeyOfReasonCannon : HoverShooter
	{
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 3;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults() {
			Projectile.damage = 187 / 2;
			Projectile.netImportant = true;
			Projectile.width = 64;
			Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			inertia = 20f;
			shoot = ProjectileID.MoonlordBullet;
			shootSpeed = 12f;
			Projectile.scale = 1.5f;
		}

		public override void CheckActive() {
			Player player = Main.player[Projectile.owner];
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.dead) {
				modPlayer.reasonableCannon = false;
			}
			if (modPlayer.reasonableCannon) { // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
				Projectile.timeLeft = 2;
			}
/*			int dust = Dust.NewDust(projectile.position, 0, 0, DustID.SapphireBolt);
			Main.dust[dust].scale = 1.5f;
			Main.dust[dust].noGravity = true;*/
		}
		public override void AI()
		{
			base.AI();
			Lighting.AddLight(Projectile.position, 0.28f, 0.82f, 1f);
		}

		//public override void CreateDust() {
		//	if (projectile.ai[0] == 0f) {
		//		if (Main.rand.NextBool(5)) {
		//			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height / 2, ModContent.DustType<PuriumFlame>());
		//			Main.dust[dust].velocity.Y -= 1.2f;
		//		}
		//	}
		//	else {
		//		if (Main.rand.NextBool(3)) {
		//			Vector2 dustVel = projectile.velocity;
		//			if (dustVel != Vector2.Zero) {
		//				dustVel.Normalize();
		//			}
		//			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<PuriumFlame>());
		//			Main.dust[dust].velocity -= 1.2f * dustVel;
		//		}
		//	}
		//	Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
		//}

		/*		public override void SelectFrame() {
					projectile.frameCounter++;
					if (projectile.frameCounter >= 8) {
						projectile.frameCounter = 0;
						projectile.frame = (projectile.frame + 1) % 3;
					}
				}*/
	}
}
