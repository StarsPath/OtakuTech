using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace OtakuTech.Content.Projectiles
{
	public class RavagingFlame2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ravaging Flame");
            Main.projFrames[Projectile.type] = 12;
        }

		public override void SetDefaults() {
			Projectile.width = 189;
            Projectile.height = 189;
            //projectile.aiStyle = 75;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = false;
            Projectile.scale = 2f;

            //drawOriginOffsetX = -82f;
            //drawOriginOffsetY = 20;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                //projectile.frame = 0;
                Projectile.Kill();
            }
            
            Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : -1));

            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            // Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
            if (Projectile.spriteDirection == -1)
                Projectile.rotation += MathHelper.Pi;

            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = player.position - new Vector2(189, 189);
            dust = Terraria.Dust.NewDustDirect(position, 378, 378, DustID.Torch, 0f, 0f, 0, new Color(255, 255, 255), 1.5f);
            Lighting.AddLight(Projectile.position, 1f, 0.28f, 0f);

        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = true;
            damage *= 4;
            target.AddBuff(BuffID.OnFire3, 600);
            //base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = (float)((Projectile.spriteDirection == 1) ? (sourceRectangle.Width - 94) : 94);

            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

            return false;
        }

    }

}
