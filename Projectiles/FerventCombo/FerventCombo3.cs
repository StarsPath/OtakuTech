using OtakuTech.Items.Weapons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OtakuTech.Projectiles.FerventCombo
{
    public class FerventCombo3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fervent Combo");
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            //ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 240;
            projectile.height = 200;
            //projectile.aiStyle = 75;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = false;
            projectile.scale = 1f;
            projectile.alpha = 50;

            //drawOriginOffsetX = -82f;
            //drawOriginOffsetY = 20;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            damage *= 2;
            target.immune[projectile.owner] = 4;
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = 230;
            hitbox.Height = 230;
            hitbox.Location = (projectile.Center - new Vector2(hitbox.Width / 2, hitbox.Height / 2)).ToPoint();
            base.ModifyDamageHitbox(ref hitbox);
        }

        public override void AI()
        {
            projectile.alpha += 10;
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
            // Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
            if (projectile.spriteDirection == -1)
                projectile.rotation += MathHelper.Pi;
        }

    }

}
