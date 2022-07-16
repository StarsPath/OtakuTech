using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using OtakuTech.Common.Players;
using OtakuTech.Content.Projectiles;
using OtakuTech.Content.Buffs;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
	public class UndinesTale : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/AF4BFF:A scythe that appeared when Seele activated]" +
				"\n[c/AF4BFF:the powers of her stigma. This razor sharp weapon]" +
				"\n[c/AF4BFF:of terror is a stark contrast to the quiet and shy little girl.]");
		}

		private int undineCD = 25 * 60;

		public override void SetDefaults() {
			Item.damage = 175;
			Item.crit = 19;
			Item.scale = 1.5f;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<Projectiles.SereneReaper>();
			Item.autoReuse = false;
			Item.useTurn = true;
			Item.shootSpeed = 8f;

		}

        public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool CanUseItem(Player player) {
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.altFunctionUse == 2) {
				if (moddedPlayer.undineCD > 0)
					return false;
				Item.useTime = 20;
				Item.useAnimation = 20;
				Item.reuseDelay = 30;
				Item.autoReuse = false;
				Item.noUseGraphic = false;
				Item.shoot = ModContent.ProjectileType<FlutteringRipple>();
				moddedPlayer.undineCD = undineCD;
				player.AddBuff(ModContent.BuffType<VelionasTorrent>(), 4 * 60);
			}
			else
			{
				Item.autoReuse = moddedPlayer.velionasTorrent ? true : false;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.useTime = 15;
				Item.useAnimation = 15;
				Item.reuseDelay = 5;
				Item.shoot = moddedPlayer.velionasTorrent ? ModContent.ProjectileType<SereneReaper2>() : ModContent.ProjectileType<SereneReaper>();
				Item.noUseGraphic = false;

				//Main.PlaySound(SoundID.Item100);
                
            }
			return base.CanUseItem(player);
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (modPlayer.undineCD > 0)
			{
				float percentCd = modPlayer.undineCD / undineCD;
				Vector2 center = position + new Vector2(frame.Width / 2 * scale, frame.Height / 2 * scale);
				Vector2 position2 = center - TextureAssets.Cd.Size() * Main.inventoryScale / 2f;
				Color white = Color.White;
				Texture2D CD = ModContent.Request<Texture2D>("OtakuTech/Assets/Textures/Other/Cooldown-2").Value;
				spriteBatch.Draw(CD, position2, null, new Color(percentCd, percentCd, percentCd, percentCd), 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
			}
			base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
	}
}