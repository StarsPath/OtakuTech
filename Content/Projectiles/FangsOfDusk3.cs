using Microsoft.Xna.Framework;
using OtakuTech.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles
{
    public class FangsOfDusk3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FangsOfDusk");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 112;
            Projectile.height = 112;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.tileCollide = false;
            Projectile.alpha = 50;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
        }

        public bool toggle = false;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            toggle = true;
            Player player = Main.player[Projectile.owner];
            ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
            target.immune[Projectile.owner] = 8;
            if (modplayer.enchanced)
                target.immune[Projectile.owner] = 2;
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (toggle)
                if (++Projectile.frameCounter >= 4)
                {
                    Projectile.frameCounter = 0;
                    if (Projectile.frame < Main.projFrames[Projectile.type]-1)
                    {
                        Projectile.frame++;
                    }
                }

            //projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            //projectile.rotation += 0.4f;

            Projectile.rotation += (float)Projectile.direction;


            if (Projectile.ai[1] == 1)
            {
                Projectile.Center = Vector2.Lerp(Projectile.Center, player.Center, 0.5f);
                //Vector2 dir = projectile.DirectionTo(player.Center);
                //dir.Normalize();
                //projectile.velocity = dir * 18f;
            }


            if (Projectile.ai[0] >= 0)
            {
                Projectile.ai[0]++;
                Vector2 dir = player.position - Main.MouseWorld;
                dir.Normalize();
                Vector2 position = player.position - (dir * 120f);
                Projectile.Center = Vector2.Lerp(Projectile.Center, position, 0.2f);
            }

            if (Projectile.ai[0] >= 24)
            {
                //projectile.velocity.X *= -1;
                //projectile.velocity.Y *= -1;
                Projectile.ai[0] = -1;
                Projectile.ai[1] = 1;
            }

            //int numParticle = 16;
            //int radius = 28;
            //double angle = 2 * Math.PI / numParticle;

            //for (int i = 0; i < numParticle; i++)
            //{
            //	Dust d = Dust.NewDustPerfect(projectile.Center + new Vector2(radius * (float)Math.Cos(angle*i), radius * (float)Math.Sin(angle * i)), DustID.PurpleTorch);
            //	d.noGravity = true;
            //}

            //Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.IceTorch, Scale:2f);
            //dust.noGravity = true;
            //Main.dust[dust].scale = 1f;
            //Main.dust[dust].noGravity = true;
            //Lighting.AddLight(projectile.position, 1f, 0.97f, 0.40f);
        }

        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        //{
        //    //Redraw the projectile with the color not influenced by light
        //    Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height/3 * 0.5f);
        //    for (int k = 0; k < projectile.oldPos.Length; k++)
        //    {
        //        Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
        //        Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
        //        spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
        //    }
        //    return true;
        //}
        /*        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
				{
					Main.PlaySound(SoundID.Item105, projectile.position);
					base.OnHitNPC(target, damage, knockback, crit);
				}*/
    }
}
