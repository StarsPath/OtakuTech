using Microsoft.Xna.Framework;
using OtakuTech.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Projectiles
{
	public class SentienceSpearCharge : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SentienceSpearCharge");
		}

		private float pullDist = 300f;
		private int maxPull = 100;
		private float pullForce = 0.9f;

		public override void SetDefaults() {
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 40;
			Projectile.scale = 1.2f;

			//Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			//target.velocity *= 0.5f;
            //Player player = Main.player[Projectile.owner];
            //ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //if (!modPlayer.enchanced)
            //    Projectile.damage = (int)(damage * 0.7f); // Multihit penalty. Decrease the damage the more enemies the whip hits.
        }

        // It appears that for this AI, only the ai0 field is used!
        public override void AI() {
			Player player = Main.player[Projectile.owner];
			Projectile.velocity = Vector2.Lerp(Projectile.velocity, default, 0.07f);


			for (int i = 0; i < maxPull; i++)
			{
				NPC npc = Main.npc[i];
				float dist = Vector2.Distance(Projectile.position, npc.position);
				if (dist < pullDist)
				{
					npc.Center = Vector2.Lerp(npc.Center, Projectile.Center, 0.1f);
					//Vector2 dirTo = npc.Center - Projectile.Center;
					//dirTo.Normalize();
					//npc.velocity -= (dirTo * pullForce);
					npc.velocity *= 0.8f;
                }
			}


			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.GemTopaz);
				Vector2 dir = Projectile.velocity;
				dir.Normalize();
				dust.velocity = -dir.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-60, 60))) * 4f;
				dust.noGravity = true;
				dust.scale = 1f;
				Lighting.AddLight(Projectile.position, 1f, 0.97f, 0.40f);
			}
		}
	}
}
