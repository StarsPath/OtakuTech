using Microsoft.Xna.Framework;
using OtakuTech.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles.Minions
{
    public class JudahCross : ModProjectile
    {
        private bool captureDir = false;
        private float shootcd = 16;
        private float shootSpeed = 30f;
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
            Projectile.width = 48;
            Projectile.height = 110;
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
            Projectile.timeLeft = 720;

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
            }
            if (Projectile.ai[0] > 0)
            {
                Projectile.ai[0]++;
                if(Projectile.ai[0] >= shootcd)
                {
                    Projectile.ai[0] = 1f;
                    int dustCount = 10;
                    for(int i = 0; i < dustCount; i++)
                    {
                        SoundEngine.PlaySound(SoundID.Item105, Projectile.position);
                        Dust dust = Dust.NewDustPerfect(new Vector2(Projectile.Center.X + Main.rand.NextFloatDirection() * 5f, Projectile.Center.Y - Main.rand.NextFloat(1f, 1.2f) * 20f * i), DustID.GemTopaz);
                        dust.noGravity = true;
                    }
                    int proj = Projectile.NewProjectile(new EntitySource_Misc("Judah Cross"), Projectile.Center.X + Main.rand.NextFloatDirection() * 200f, Projectile.Center.Y - 1000, 0, shootSpeed, ModContent.ProjectileType<Projectiles.LightningSpear>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].netUpdate = true;
                    Main.projectile[proj].scale = 1.5f;
                }
            }
            
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
