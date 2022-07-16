using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles
{
    public class FlutteringRipple : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fluttering Ripple");
            Main.projFrames[Projectile.type] = 7;
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            //ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 112;
            Projectile.height = 112;
            //projectile.aiStyle = 75;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = false;
            Projectile.scale = 1f;
            Projectile.alpha = 70;

            Projectile.timeLeft = 1 * 60;
            //drawOriginOffsetX = -82f;
            //drawOriginOffsetY = 20;
        }
        //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        //{
        //    target.immune[projectile.owner] = 8;
        //    base.OnHitNPC(target, damage, knockback, crit);
        //}

        //public override void ModifyDamageHitbox(ref Rectangle hitbox)
        //{
        //    hitbox.Width = 230;
        //    hitbox.Height = 230;
        //    hitbox.Location = (projectile.Center - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
        //    base.ModifyDamageHitbox(ref hitbox);
        //}

        public override void AI()
        {
            Projectile.alpha += 2;
            Player player = Main.player[Projectile.owner];
            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                    //projectile.Kill();
                }
            }
            //projectile.Center = player.Center;

            //projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));
            Projectile.rotation = Main.rand.NextFloatDirection();
            Projectile.velocity *= 0.98f;

            //projectile.rotation = projectile.velocity.ToRotation();
            //if (projectile.velocity.Y > 16f)
            //{
            //    projectile.velocity.Y = 16f;
            //}
            //if (projectile.spriteDirection == -1)
            //    projectile.rotation += MathHelper.Pi;

            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MagnetSphere, SpeedX: Projectile.velocity.X, SpeedY:Projectile.velocity.Y, Scale:1f);
            d.noGravity = true;
            d.fadeIn = 3f;

        }

        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //{

        //    Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
        //    for (int k = 0; k < projectile.oldPos.Length; k++)
        //    {
        //        Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
        //        Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
        //        Rectangle frame = new Rectangle(0, 0, 260, 60);
        //        frame.Y += 60 * k;
        //        spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, frame, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
        //    }
        //    return false;
        //}

    }

}
