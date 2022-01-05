using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Projectiles.Minions
{
    public class HekatesSombreCross : ModProjectile
    {
        private bool captureDir = false;
        private float shootcd = 8;
        private float shootSpeed = 30f;
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 1;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = false;
        }
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 134;
            projectile.height = 216;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 0;
            projectile.penetrate = -1;
            //projectile.timeLeft = 18000;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            //inertia = 20f;
            //shoot = ProjectileID.MoonlordBullet;
            //shootSpeed = 12f;
            projectile.timeLeft = 900;

            //projectile.spriteDirection = -Main.player[projectile.owner].direction;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (player.dead)
            {
                modPlayer.crossDeploy = false;
                projectile.Kill();
            }

            if (!captureDir)
            {
                projectile.direction = (projectile.spriteDirection = (-Main.player[projectile.owner].direction));
                captureDir = true;
            }
            //Main.NewText(projectile.ai[0]);
            if (projectile.velocity == default && projectile.ai[0] == 0)
            {
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] > 0)
            {
                projectile.ai[0]++;
                if(projectile.ai[0] >= shootcd)
                {
                    projectile.ai[0] = 1f;
                    int dustCount = 10;
                    for(int i = 0; i < dustCount; i++)
                    {
                        Main.PlaySound(SoundID.Item105, projectile.position);
                        Dust dust = Dust.NewDustPerfect(new Vector2(projectile.Center.X + Main.rand.NextFloatDirection() * 5f, projectile.Center.Y - Main.rand.NextFloat(1f, 1.2f) * 20f * i), DustID.IceTorch);
                        dust.noGravity = true;
                    }
                    int proj = Projectile.NewProjectile(projectile.Center.X + Main.rand.NextFloatDirection() * 400f, projectile.Center.Y - 1000, 0, shootSpeed, ModContent.ProjectileType<FangsOfDusk>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].netUpdate = true;
                }
            }
            
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.owner == Main.myPlayer)
            {
                ModdedPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<ModdedPlayer>();
                modPlayer.crossDeploy = false;
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return true;
        }
    }
}
