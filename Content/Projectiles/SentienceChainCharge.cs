using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;
using Terraria.DataStructures;
using ReLogic.Content;

namespace OtakuTech.Content.Projectiles
{
	public class SentienceChainCharge : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SentienceChainCharge");
		}

		public override void SetDefaults() {
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			//Projectile.usesLocalNPCImmunity = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			//Projectile.localNPCHitCooldown = -1;
			Projectile.timeLeft = 30 * 3;
            //Projectile.scale = 2f;
		}

		//     public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		//     {
		//Projectile.damage = (int)(damage * 0.9f);
		//base.OnHitNPC(target, damage, knockback, crit);
		//     }

		//public int interval = 4;

		//      public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		//      {
		//          base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
		//      }
		public Vector2 spawnPos;
		public int interval = 4;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] == 0)
            {
				spawnPos = Projectile.position;
				Projectile.ai[0] = 1;
            }
            if (Projectile.ai[1] >= interval)
            {
				Projectile p = Projectile.NewProjectileDirect(new EntitySource_Misc("SentienceChainCharge"), Projectile.Center, default, ModContent.ProjectileType<SentienceChainExtra2>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
				p.timeLeft = Projectile.timeLeft;
				Projectile.ai[1] = 0;
			}
			Projectile.ai[1]++;

            Projectile.velocity = Vector2.Lerp(Projectile.velocity, default, 0.1f);

            if (Projectile.velocity != default)
            {
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			}

			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, 0, 0, DustID.GemRuby);
				dust.velocity = -Projectile.velocity.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-60, 60))) * 4f;
				//dust.scale = 0.6f;
				dust.noGravity = true;
			}
			Lighting.AddLight(Projectile.position, 0.9f, 0.17f, 0.17f);
		}

        public override bool PreDraw(ref Color lightColor)
        {
			Vector2 playerArmPosition = spawnPos;

			// This fixes a vanilla GetPlayerArmPosition bug causing the chain to draw incorrectly when stepping up slopes. The flail itself still draws incorrectly due to another similar bug. This should be removed once the vanilla bug is fixed.
			//playerArmPosition.Y -= Main.player[Projectile.owner].gfxOffY;

			Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("OtakuTech/Content/Projectiles/SentienceChainExtra");
			//Asset<Texture2D> chainTextureExtra = ModContent.Request<Texture2D>(ChainTextureExtraPath); // This texture and related code is optional and used for a unique effect

			Rectangle? chainSourceRectangle = null;
			// Drippler Crippler customizes sourceRectangle to cycle through sprite frames: sourceRectangle = asset.Frame(1, 6);
			float chainHeightAdjustment = 0f; // Use this to adjust the chain overlap. 

			Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
			Vector2 chainDrawPosition = Projectile.Center;
			Vector2 vectorFromProjectileToPlayerArms = playerArmPosition.MoveTowards(chainDrawPosition, 4f) - chainDrawPosition;
			Vector2 unitVectorFromProjectileToPlayerArms = vectorFromProjectileToPlayerArms.SafeNormalize(Vector2.Zero);
			float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
			if (chainSegmentLength == 0)
				chainSegmentLength = 10; // When the chain texture is being loaded, the height is 0 which would cause infinite loops.
			float chainRotation = unitVectorFromProjectileToPlayerArms.ToRotation() + MathHelper.PiOver2;
			int chainCount = 0;
			float chainLengthRemainingToDraw = vectorFromProjectileToPlayerArms.Length() + chainSegmentLength / 2f;

			// This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
			while (chainLengthRemainingToDraw > 0f)
			{
				// This code gets the lighting at the current tile coordinates
				Color chainDrawColor = Lighting.GetColor((int)chainDrawPosition.X / 16, (int)(chainDrawPosition.Y / 16f));

				// Flaming Mace and Drippler Crippler use code here to draw custom sprite frames with custom lighting.
				// Cycling through frames: sourceRectangle = asset.Frame(1, 6, 0, chainCount % 6);
				// This example shows how Flaming Mace works. It checks chainCount and changes chainTexture and draw color at different values

				var chainTextureToDraw = chainTexture;

				// Here, we draw the chain texture at the coordinates
				Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, chainDrawColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);

				// chainDrawPosition is advanced along the vector back to the player by the chainSegmentLength
				chainDrawPosition += unitVectorFromProjectileToPlayerArms * chainSegmentLength;
				chainCount++;
				chainLengthRemainingToDraw -= chainSegmentLength;
			}

			return true;
        }

    }
}
