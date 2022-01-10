using OtakuTech.Items.Weapons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OtakuTech.Projectiles
{
    public class SereneReaper : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Serene Reaper");
            Main.projFrames[projectile.type] = 5;
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            //ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 256;
            projectile.height = 256;
            //projectile.aiStyle = 75;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = false;
            projectile.scale = 1f;
            projectile.alpha = 70;

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
            projectile.alpha += 15;
            Player player = Main.player[projectile.owner];
            if (++projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= Main.projFrames[projectile.type])
                {
                    //projectile.frame = 0;
                    projectile.Kill();
                }
            }
            projectile.Center = player.Center;

            projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));

            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            if (projectile.spriteDirection == -1)
                projectile.rotation += MathHelper.Pi;

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
