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
	public class InstantDraw : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("InstantDraw");
            Main.projFrames[projectile.type] = 13;
        }

        private bool toggle = true;

		public override void SetDefaults() {
			projectile.width = 64;
            projectile.height = 64;
            //projectile.aiStyle = 75;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = false;
            projectile.scale = 1.25f;

            /*drawOriginOffsetX = -82f;
            drawOriginOffsetY = 20;*/
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (++projectile.frame >= Main.projFrames[projectile.type])
            {
                projectile.Kill();
            }
            if (toggle)
            {
                projectile.rotation = Main.rand.NextFloatDirection();
                toggle = false;
            }
            /*projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));

            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            // Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
            if (projectile.spriteDirection == -1)
                projectile.rotation += MathHelper.Pi;
            */

            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            dust = Dust.NewDustDirect(projectile.position, 64, 64, DustID.Frost, 0f, 0f, 0, new Color(255, 255, 255), 1f);
            dust.noGravity = true;
            Lighting.AddLight(projectile.position, 0f, 1f, 1f);

        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.HasBuff(BuffID.Frostburn))
                crit = true;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int startY = frameHeight * projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = (float)((projectile.spriteDirection == 1) ? (sourceRectangle.Width - 84) : 84);

            Color drawColor = projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture,
            projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
            sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

            return false;
        }*/

    }

}
