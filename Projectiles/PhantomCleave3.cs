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
	public class PhantomCleave3 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("PhantomCleave3");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 10;
            projectile.tileCollide = false;
        }

        //private bool spawn = true;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI() {
			// let ai[0] = foward motion
			// let ai[1] = upward motion

			//spawn foward projectile
			//if((int)projectile.ai[0] > 0 && spawn)
   //         {
			//	Vector2 vector5 = projectile.Center;
			//	vector5.X += projectile.width * projectile.velocity.SafeNormalize(default).X;
			//	Projectile.NewProjectile(vector5, projectile.velocity, ModContent.ProjectileType<PhantomCleave3>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0] - 1, projectile.ai[1] + 0.2f);
				
   //         }

   //         if ((int)projectile.ai[1] > 0 && spawn)
   //         {
   //             Vector2 vector5 = projectile.Center;
   //             vector5.Y -= projectile.height;
   //             Projectile.NewProjectile(vector5, projectile.velocity, ModContent.ProjectileType<PhantomCleave3>(), projectile.damage, projectile.knockBack, projectile.owner, 0, projectile.ai[1] -1);
   //         }

   //         spawn = false;
            
			Dust dust = Dust.NewDustDirect(projectile.Center, projectile.width * 2, projectile.height * 2, DustID.IceTorch, SpeedX: projectile.velocity.X, SpeedY: projectile.velocity.Y, Scale:4f);
			dust.noGravity = true;
            dust.fadeIn = 3f;
			Lighting.AddLight(projectile.Center, 1f, 0.97f, 0.40f);
        }
	}
}
