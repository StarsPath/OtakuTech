using OtakuTech.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace OtakuTech.Items.Weapons.FiveStars
{
	public class UndinesTale : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/AF4BFF:A scythe that appeared when Seele activated]" +
				"\n[c/AF4BFF:the powers of her stigma. This razor sharp weapon]" +
				"\n[c/AF4BFF:of terror is a stark contrast to the quiet and shy little girl.]");
		}

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
			item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 8f;
		}

		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool CanUseItem(Player player) {
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.altFunctionUse == 2) {
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.useTime = 20;
				item.useAnimation = 20;
				item.reuseDelay = 30;
				item.damage = 189;
				item.noUseGraphic = true;
				item.shoot = ModContent.ProjectileType<Projectiles.TestProjectile>();

                Main.PlaySound(SoundID.Item100);
			}
			else
			{
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.useTime = 30;
				item.useAnimation = 30;
				item.reuseDelay = 0;
				item.damage = 189;
				item.shoot = ProjectileID.None;
				item.noUseGraphic = false;

				//Main.PlaySound(SoundID.Item100);
                
            }
			return base.CanUseItem(player);
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

   //     public override void MeleeEffects(Player player, Rectangle hitbox)
   //     {
			//int dust = Dust.NewDust(hitbox.Center.ToVector2() - new Vector2(hitbox.Width/2, hitbox.Height/2), hitbox.Width, hitbox.Height, DustID.Fire);
			//Main.dust[dust].noGravity = true;
			////Main.dust[dust].fadeIn = 1f;
			//Main.dust[dust].scale = 1.5f;
			//base.MeleeEffects(player, hitbox);
   //     }

		//public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		//{
		//	Player player = Main.player[item.owner];
		//	ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
		//	if (modPlayer.libationCD > 0)
		//	{
		//		float percentCd = modPlayer.libationCD / libationCD;
		//		Vector2 center = position + new Vector2(frame.Width / 2 * scale, frame.Height / 2 * scale);
		//		Vector2 position2 = center - Main.cdTexture.Size() * Main.inventoryScale / 2f;
		//		Color white = Color.White;
		//		//spriteBatch.Draw(Main.cdTexture, position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
		//		spriteBatch.Draw(ModLoader.GetMod("OtakuTech").GetTexture("Other/Cooldown-2"), position2, null, new Color(percentCd, percentCd, percentCd, percentCd), 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
		//	}
		//	base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		//}
	}
}