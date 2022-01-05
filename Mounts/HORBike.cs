using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Mounts
{
	public class HORBike : ModMountData
	{
		public override void SetDefaults() {
			//mountData.spawnDust = ModContent.DustType<Smoke>();
			mountData.buff = ModContent.BuffType<ProjectBunnyBike>();
			mountData.heightBoost = 20;
			mountData.fallDamage = 0.5f;
			mountData.runSpeed = 11f;
			mountData.dashSpeed = 8f;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 5;
			mountData.acceleration = 0.19f;
			mountData.jumpSpeed = 4f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 4;
			mountData.constantJump = true;
			int[] array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++) {
				array[l] = 20;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 13;
			mountData.bodyFrame = 3;
			mountData.yOffset = 5;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 4;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 4;
			mountData.idleFrameDelay = 12;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = true;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode == NetmodeID.Server) {
				return;
			}

			mountData.textureWidth = mountData.backTexture.Width;
			mountData.textureHeight = mountData.backTexture.Height;
			//mountData.textureWidth = 150;
			//mountData.textureHeight = 54;
		}

		public override void UpdateEffects(Player player) {
			// This code simulates some wind resistance for the balloons. 

			// This code spawns some dust if we are moving fast enough.
			if (!(Math.Abs(player.velocity.X) > 4f)) {
				return;
			}
			Rectangle rect = player.getRect();
			Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, DustID.SapphireBolt);
		}

		public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow) {
			// Draw is called for each mount texture we provide, so we check drawType to avoid duplicate draws.

			// by returning true, the regular drawing will still happen.
			return true;
		}
	}
}