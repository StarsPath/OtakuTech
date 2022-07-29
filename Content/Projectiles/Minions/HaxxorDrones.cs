using OtakuTech.Common.Players;
using OtakuTech.Content.Items.Weapons.FiveStars;
using OtakuTech.Projectiles.Minions;
using Terraria;
using Terraria.ID;

namespace OtakuTech.Content.Projectiles.Minions
{
    // PurityWisp uses inheritace as an example of how it can be useful in modding.
    // HoverShooter and Minion classes help abstract common functionality away, which is useful for mods that have many similar behaviors.
    // Inheritance is an advanced topic and could be confusing to new programmers, see ExampleSimpleMinion.cs for a simpler minion example.
    public class HaxxorDrones : HoverShooter
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
        }

        public override void SetDefaults()
        {
            Projectile.damage = 20;
            Projectile.netImportant = true;
            Projectile.width = 40;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30 * 60;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.scale = 1f;
            //Projectile.damage = 20;

            inertia = 20f;
            shoot = ProjectileID.Bullet;
            shootSpeed = 12f;
            viewDist = 400f;
            chaseDist = 300f;
            spacingMult = 0.8f;
            shootCool = 20f;
            rotateTowardsTarget = false;
            overrideDamage = true;
            oDamage = 20;
        }

        public override void CheckActive()
        {
            Player player = Main.player[Projectile.owner];
            if(player.HeldItem.ModItem is CrusherBunny19C)
            {
                Projectile.timeLeft = 30 * 60;
            }
            //ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
        }
        public override void AI()
        {
            //Main.NewText(Projectile.damage);
            base.AI();
            Lighting.AddLight(Projectile.position, 0.28f, 0.82f, 1f);
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
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
