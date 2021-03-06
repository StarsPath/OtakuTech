using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles
{
    public class SereneReaper2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Serene Reaper");
            Main.projFrames[Projectile.type] = 5;
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            //ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 256;
            Projectile.height = 256;
            //projectile.aiStyle = 75;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = false;
            Projectile.scale = 1f;
            Projectile.alpha = 70;

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
            Projectile.alpha += 5;
            Player player = Main.player[Projectile.owner];
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    //projectile.frame = 0;
                    Projectile.Kill();
                }
            }
            Projectile.Center = player.Center;

            Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : -1));

            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            if (Projectile.spriteDirection == -1)
                Projectile.rotation += MathHelper.Pi;

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
