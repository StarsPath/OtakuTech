using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using OtakuTech.Common.Players;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using OtakuTech.Content.Buffs;

namespace OtakuTech.Content.Items.Accessories
{
	public class ForgetMeNot : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/4287f5:Several light blue flowers. If you draw near, you can smell their faint fragrance.]\n"
							 + "Allows the player to dash by double tap\n"
							 + "Triggers time fracture on dash lasting 2 seconds\n"
							 + "+40% movement (walk, jump, fly) speed and attack speed\n"
							 + "+40% increased damage for 5 seconds after dash\n"
							 );
							 //+ "increases mana regen based on missing mana (max 100% at 0 mana)\n");

			//CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			//Item.vanity = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
			Item.rare = ItemRarityID.Cyan;

		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DashPlayer modplayer = player.GetModPlayer<DashPlayer>();
			modplayer.DashAccessoryEquipped = true;
            if (modplayer.DashTimer > 0)
            {
                //player.GetDamage(DamageClass.Generic) += 0.3f;
                modplayer.duringDash();
            }
            player.GetAttackSpeed(DamageClass.Generic) += 0.4f;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.4f;
			player.accRunSpeed += 0.4f;
			player.wingAccRunSpeed += 0.4f;
			player.jumpSpeedBoost += 0.4f;
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
			DashPlayer modPlayer = player.GetModPlayer<DashPlayer>();
			if (modPlayer.DashDelay > 0)
			{
				float percentCd = (float)modPlayer.DashDelay / DashPlayer.DashCooldown;
				Vector2 center = position + new Vector2(frame.Width / 2 * scale, frame.Height / 2 * scale);
				Vector2 position2 = center - TextureAssets.Cd.Size() * Main.inventoryScale / 2f;
				Color white = Color.White;
				//spriteBatch.Draw(Main.cdTexture, position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
				Texture2D CD = ModContent.Request<Texture2D>("OtakuTech/Assets/Textures/Other/Cooldown-2").Value;
				spriteBatch.Draw(CD, position2, null, new Color(percentCd, percentCd, percentCd, percentCd), 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
			}
			base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

		public class DashPlayer : ModPlayer
		{
			// These indicate what direction is what in the timer arrays used
			public const int DashDown = 0;
			public const int DashUp = 1;
			public const int DashRight = 2;
			public const int DashLeft = 3;

			public const int DashCooldown = 15 * 60; // Time (frames) between starting dashes. If this is shorter than DashDuration you can start a new dash before an old one has finished
			public const int DashDuration = 5*60; // Duration of the dash afterimage effect in frames

			// The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
			public const float DashVelocity = 16f;

			// The direction the player has double tapped.  Defaults to -1 for no dash double tap
			public int DashDir = -1;

			// The fields related to the dash accessory
			public bool DashAccessoryEquipped;
			public int DashDelay = 0; // frames remaining till we can dash again
			public int DashTimer = 0; // frames remaining in the dash

			public override void ResetEffects()
			{
				// Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
				DashAccessoryEquipped = false;

				// ResetEffects is called not long after player.doubleTapCardinalTimer's values have been set
				// When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
				// If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
				if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[DashDown] < 15)
				{
					DashDir = DashDown;
				}
				else if (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[DashUp] < 15)
				{
					DashDir = DashUp;
				}
				else if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
				{
					DashDir = DashRight;
				}
				else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
				{
					DashDir = DashLeft;
				}
				else
				{
					DashDir = -1;
				}
			}

			// This is the perfect place to apply dash movement, it's after the vanilla movement code, and before the player's position is modified based on velocity.
			// If they double tapped this frame, they'll move fast this frame
			public override void PreUpdateMovement()
			{
				// if the player can use our dash, has double tapped in a direction, and our dash isn't currently on cooldown
				if (CanUseDash() && DashDir != -1 && DashDelay == 0)
				{
					Vector2 newVelocity = Player.velocity;

					switch (DashDir)
					{
						// Only apply the dash velocity if our current speed in the wanted direction is less than DashVelocity
						case DashUp when Player.velocity.Y > -DashVelocity:
						case DashDown when Player.velocity.Y < DashVelocity:
							{
								// Y-velocity is set here
								// If the direction requested was DashUp, then we adjust the velocity to make the dash appear "faster" due to gravity being immediately in effect
								// This adjustment is roughly 1.3x the intended dash velocity
								float dashDirection = DashDir == DashDown ? 1 : -1.3f;
								newVelocity.Y = dashDirection * DashVelocity;
								break;
							}
						case DashLeft when Player.velocity.X > -DashVelocity:
						case DashRight when Player.velocity.X < DashVelocity:
							{
								// X-velocity is set here
								float dashDirection = DashDir == DashRight ? 1 : -1;
								newVelocity.X = dashDirection * DashVelocity;
								break;
							}
						default:
							return; // not moving fast enough, so don't start our dash
					}

					// start our dash
					onDash();
					DashDelay = DashCooldown;
                    DashTimer = DashDuration;
					Player.velocity = newVelocity;

					// Here you'd be able to set an effect that happens when the dash first activates
					// Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
				}

				if (DashDelay > 0)
					DashDelay--;

				if (DashTimer > 0)
				{ // dash is active
				  // This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
				  // Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
				  // Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
					//Player.eocDash = DashTimer;
					//Player.armorEffectDrawShadowEOCShield = true;

					// count down frames remaining
					DashTimer--;
				}
				if (DashTimer <= 0)
				{
					onDashEnd();
				}
			}

			private bool CanUseDash()
			{
				return DashAccessoryEquipped
					&& Player.dashType == 0 // player doesn't have Tabi or EoCShield equipped (give priority to those dashes)
					&& !Player.setSolar // player isn't wearing solar armor
					&& !Player.mount.Active; // player isn't mounted, since dashes on a mount look weird
			}

			private const int radius = 1600;
			private const int debuffTime = 2 * 60;

			private void onDash()
            {
				Player.immune = true;
				Player.immuneAlpha = 120;
				Player.immuneTime = 40;
				Player.AddBuff(ModContent.BuffType<SignetsOfSetsuna>(), 5 * 60);
				if (Main.netMode != NetmodeID.Server && !Filters.Scene["TimeFracture"].IsActive()) // This all needs to happen client-side!
				{
					Filters.Scene.Activate("TimeFracture").GetShader().UseTargetPosition(Player.Center);
                }
				for(int i = 0; i < Main.npc.Length; i++)
                {
					if(Main.npc[i].damage > 0 && Main.npc[i].active && !Main.npc[i].friendly && !Main.npc[i].boss)
                    {
						float newDist = Vector2.Distance(Player.Center, Main.npc[i].Center);
						if (newDist < radius)
						{
							Main.npc[i].AddBuff(ModContent.BuffType<TimeFracture>(), debuffTime);
						}
					}
				}
			}

			public void duringDash()
            {
				if (Main.netMode != NetmodeID.Server && Filters.Scene["TimeFracture"].IsActive()) // This all needs to happen client-side!
				{
					//Filters.Scene["TimeFracture"].GetShader().UseColor(1f, 0f, 0.9f).UseOpacity(0f);
					if(DashTimer <= DashDuration - debuffTime)
                    {
						Filters.Scene["TimeFracture"].Deactivate();
					}
				}
			}

			private void onDashEnd()
			{
				//if (Main.netMode != NetmodeID.Server && Filters.Scene["TimeFracture"].IsActive()) // This all needs to happen client-side!
				//{
				//	Filters.Scene["TimeFracture"].Deactivate();
				//}
			}
		}
	}
}
