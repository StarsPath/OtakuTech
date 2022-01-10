using OtakuTech.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Buffs;
using OtakuTech.Projectiles;

namespace OtakuTech.Items.Weapons.FiveStars
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
			item.damage = 175;
			item.crit = 19;
			item.scale = 1.5f;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<Projectiles.SereneReaper>();
			item.autoReuse = false;
			item.useTurn = true;
			item.shootSpeed = 8f;

		}

        public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool CanUseItem(Player player) {
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.altFunctionUse == 2) {
				if (moddedPlayer.undineCD > 0)
					return false;
				item.useTime = 20;
				item.useAnimation = 20;
				item.reuseDelay = 30;
				item.autoReuse = false;
				item.noUseGraphic = false;
				item.shoot = ModContent.ProjectileType<FlutteringRipple>();
				moddedPlayer.undineCD = undineCD;
				player.AddBuff(ModContent.BuffType<VelionasTorrent>(), 4 * 60);
			}
			else
			{
				item.autoReuse = moddedPlayer.velionasTorrent ? true : false;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.useTime = 15;
				item.useAnimation = 15;
				item.reuseDelay = 5;
				item.shoot = moddedPlayer.velionasTorrent ? ModContent.ProjectileType<SereneReaper2>() : ModContent.ProjectileType<SereneReaper>();
				item.noUseGraphic = false;

				//Main.PlaySound(SoundID.Item100);
                
            }
			return base.CanUseItem(player);
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[item.owner];
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (modPlayer.undineCD > 0)
			{
				float percentCd = modPlayer.undineCD / undineCD;
				Vector2 center = position + new Vector2(frame.Width / 2 * scale, frame.Height / 2 * scale);
				Vector2 position2 = center - Main.cdTexture.Size() * Main.inventoryScale / 2f;
				Color white = Color.White;
				//spriteBatch.Draw(Main.cdTexture, position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
				spriteBatch.Draw(ModLoader.GetMod("OtakuTech").GetTexture("Other/Cooldown-2"), position2, null, new Color(percentCd, percentCd, percentCd, percentCd), 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
			}
			base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
	}
}