using OtakuTech.Items.Weapons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace OtakuTech.Projectiles
{
	public class BlackHole : ModProjectile
	{
		private float pullDist = 120f;
		private int maxPull = 100;
		private float pullForce = 1f;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Black Hole");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults() {
			projectile.width = 60;
			projectile.height = 60;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
			projectile.netImportant = true;
		}

		public override void AI() {
			projectile.frameCounter++;
			projectile.frame = (projectile.frameCounter / 3) % Main.projFrames[projectile.type];

			for(int i = 0; i < maxPull; i++)
            {
				NPC npc = Main.npc[i];
				float dist = Vector2.Distance(projectile.position, npc.position);
				if(dist < pullDist)
                {
					Vector2 dirTo = npc.position - projectile.Center;
					dirTo.Normalize();
					npc.velocity -= (dirTo * pullForce);
                }
            }
			//int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.TopazBolt);
			Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.TopazBolt);
			Vector2 dir = dust.position - projectile.Center;
			dir.Normalize();
			dust.velocity = -dir * 2f;
			dust.noGravity = true;
			Lighting.AddLight(projectile.position, 1f, 0.97f, 0.40f);
		}
	}
}
