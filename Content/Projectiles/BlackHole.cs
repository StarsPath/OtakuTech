using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles
{
	public class BlackHole : ModProjectile
	{
		private float pullDist = 120f;
		private int maxPull = 100;
		private float pullForce = 0.75f;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Black Hole");
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults() {
			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 60;
			Projectile.netImportant = true;
		}

		public override void AI() {
			Projectile.frameCounter++;
			Projectile.frame = (Projectile.frameCounter / 3) % Main.projFrames[Projectile.type];

			for(int i = 0; i < maxPull; i++)
            {
				NPC npc = Main.npc[i];
				float dist = Vector2.Distance(Projectile.position, npc.position);
				if(dist < pullDist)
                {
					Vector2 dirTo = npc.position - Projectile.Center;
					dirTo.Normalize();
					npc.velocity -= (dirTo * pullForce);
					npc.velocity *= 0.9f;
                }
            }
			//int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.TopazBolt);
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz);
			Vector2 dir = dust.position - Projectile.Center;
			dir.Normalize();
			dust.velocity = -dir * 2f;
			dust.noGravity = true;
			Lighting.AddLight(Projectile.position, 1f, 0.97f, 0.40f);
		}
	}
}
