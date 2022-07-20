using Microsoft.Xna.Framework;
using OtakuTech.Common.Players;
using OtakuTech.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace OtakuTech.Content.Items.Weapons.FiveStars
{
	public class SentienceSpear : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("");
		}

		public override void SetDefaults() {
			Item.damage = 60;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 24;
			Item.useTime = 24;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Purple;
			Item.value = Item.sellPrice(silver: 10);

			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = false; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			Item.UseSound = SoundID.Item1;
			//item.shoot = ModContent.ProjectileType<HlersSerenityProjectile>();
		}
		public override bool CanUseItem(Player player) {
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			//Main.NewText(modPlayer.comboCount + " " + player.ownedProjectileCounts[ModContent.ProjectileType<HlersSerenityProjectile>()]);
			//Main.NewText(modPlayer.comboCount);
			//         if (player.altFunctionUse == 2)
			//         {
			//	item.UseSound = SoundID.Item1;
			//	item.shoot = ModContent.ProjectileType<ExcelsisKingMinion>();
			//	item.buffType = ModContent.BuffType<ExcelsisKing>();

			//	player.AddBuff(item.buffType, 2);
			//}
			//         else
			if (modPlayer.comboCount >= 2)
			{
				Item.shoot = ModContent.ProjectileType<SentienceSpearCombo>();
				Item.useStyle = ItemUseStyleID.Swing;
				Item.noMelee = true;
				Item.noUseGraphic = true;
			}
			else if (modPlayer.comboCount == 1)
			{
				Item.useStyle = ItemUseStyleID.Swing;
				Item.noMelee = false;
				Item.noUseGraphic = false;
			}
			else
            {
				Item.shoot = ModContent.ProjectileType<SentienceSpearProjectile>();
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.noMelee = true;
				Item.noUseGraphic = true;
			}

			if(player.ownedProjectileCounts[ModContent.ProjectileType<SentienceSpearProjectile>()] < 1)
            {
				return true;
			}
			return false;
			//return player.ownedProjectileCounts[item.shoot] < 1;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			//Main.NewText("SHOOT" + modPlayer.comboCount);
			if (modPlayer.comboCount == 1)
			{
				modPlayer.addCombo();
				modPlayer.resetCombo(3);
				return false;
			}
			modPlayer.addCombo();
			modPlayer.resetCombo(3);
			//if (type == ModContent.ProjectileType<HlersSerenityProjectileJavlin>())
   //         {
			//	Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(player, Item, AmmoID.None), position, velocity * 6f, ModContent.ProjectileType<HlersSerenityProjectileJavlin>(), damage, knockback, player.whoAmI);
			//	return false;
			//	//return base.Shoot(player, source, position, velocity * 20f, type, damage, knockback);
			//}
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
	}
}
