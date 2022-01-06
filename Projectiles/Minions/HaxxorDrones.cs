using Microsoft.Xna.Framework;
using OtakuTech.Items.Weapons.FiveStars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Projectiles.Minions
{
    // PurityWisp uses inheritace as an example of how it can be useful in modding.
    // HoverShooter and Minion classes help abstract common functionality away, which is useful for mods that have many similar behaviors.
    // Inheritance is an advanced topic and could be confusing to new programmers, see ExampleSimpleMinion.cs for a simpler minion example.
    public class HaxxorDrones : HoverShooter
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 3;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true; //This is necessary for right-click targeting
        }

        public override void SetDefaults()
        {
            //projectile.damage = 186 / 2;
            projectile.netImportant = true;
            projectile.width = 40;
            projectile.height = 48;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 30 * 60;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            inertia = 20f;
            shoot = ProjectileID.Bullet;
            shootSpeed = 12f;
            projectile.scale = 1f;
            viewDist = 400f;
            chaseDist = 300f;
            spacingMult = 0.8f;
            shootCool = 20f;
            rotateTowardsTarget = false;
        }

        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            if(player.HeldItem.modItem is CrusherBunny19C)
            {
                projectile.timeLeft = 30 * 60;
            }
            //ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
        }
        public override void AI()
        {
            base.AI();
            Lighting.AddLight(projectile.position, 0.28f, 0.82f, 1f);
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
            moddedPlayer.haxxorDroneCount--;
            base.Kill(timeLeft);
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
