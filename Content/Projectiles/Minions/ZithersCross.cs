using Microsoft.Xna.Framework;
using OtakuTech.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles.Minions
{
    public class ZithersCross : ModProjectile
    {
        private bool captureDir = false;
        private float shootcd = 20;
        private float shootSpeed = 25f;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = false;
        }
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 42;
            Projectile.height = 148;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 0;
            Projectile.penetrate = -1;
            //projectile.timeLeft = 18000;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            //inertia = 20f;
            //shoot = ProjectileID.MoonlordBullet;
            //shootSpeed = 12f;
            Projectile.timeLeft = 13 * 60;

            //projectile.spriteDirection = -Main.player[projectile.owner].direction;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (player.dead)
            {
                modPlayer.crossDeploy = false;
                Projectile.Kill();
            }

            if (!captureDir)
            {
                Projectile.direction = (Projectile.spriteDirection = (-Main.player[Projectile.owner].direction));
                captureDir = true;
            }
            //Main.NewText(projectile.ai[0]);
            if (Projectile.velocity == default && Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = 1f;
                Projectile.NewProjectile(new EntitySource_Misc("Zithers Cross"), Projectile.Top.X, Projectile.Top.Y - 30, 0, 0, ModContent.ProjectileType<EightFormations>(), Projectile.damage, Projectile.knockBack, player.whoAmI, Projectile.timeLeft);
            }
            //if (projectile.ai[0] > 0)
            //{
            //    projectile.ai[0]++;
            //    if(projectile.ai[0] >= shootcd)
            //    {
            //        projectile.ai[0] = 1f;
            //        int dustCount = 10;
            //        for(int i = 0; i < dustCount; i++)
            //        {
            //            Main.PlaySound(SoundID.Item105, projectile.position);
            //            Dust dust = Dust.NewDustPerfect(new Vector2(projectile.Center.X + Main.rand.NextFloatDirection() * 5f, projectile.Center.Y - Main.rand.NextFloat(1f, 1.2f) * 20f * i), DustID.TopazBolt);
            //            dust.noGravity = true;
            //        }
            //        int proj = Projectile.NewProjectile(projectile.Center.X + Main.rand.NextFloatDirection() * 200f, projectile.Center.Y - 1000, 0, shootSpeed, ModContent.ProjectileType<Projectiles.LightningSpear>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
            //        Main.projectile[proj].netUpdate = true;
            //        Main.projectile[proj].scale = 2;
            //    }
            //}
            
        }
/*        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = new Vector2(0, 0);
            return false;
        }*/
        public override void Kill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                ModdedPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<ModdedPlayer>();
                modPlayer.crossDeploy = false;
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
    }
}
