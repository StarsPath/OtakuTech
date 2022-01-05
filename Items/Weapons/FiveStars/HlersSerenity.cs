using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OtakuTech.Buffs;
using OtakuTech.Projectiles;
using OtakuTech.Projectiles.Minions;
using OtakuTech.Tiles;


namespace OtakuTech.Items.Weapons.FiveStars
{
	public class HlersSerenity : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/AF4BFF:A unique Pantheon weapon made of mysterious Soulium]" +
				"\n[c/AF4BFF:with strange hues found in the depths of the sea.]");
		}

		public override void SetDefaults() {
			item.damage = 182;
			item.crit = 13;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 18;
			item.useTime = 24;
			item.shootSpeed = 3.7f;
			item.knockBack = 6.5f;
			item.width = 32;
			item.height = 32;
			item.scale = 1f;
			item.rare = ItemRarityID.Purple;
			item.value = Item.sellPrice(silver: 10);

			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<HlersSerenityProjectile>();
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player) {
            if (player.altFunctionUse == 2)
            {
				item.UseSound = SoundID.Item1;
				item.shoot = ModContent.ProjectileType<ExcelsisKingMinion>();
				item.buffType = ModContent.BuffType<ExcelsisKing>();

				player.AddBuff(item.buffType, 2);
			}
            else
            {
                item.UseSound = SoundID.Item1;
                item.shoot = ModContent.ProjectileType<HlersSerenityProjectile>();
			}

			return player.ownedProjectileCounts[ModContent.ProjectileType<HlersSerenityProjectile>()] < 1;
			//return player.ownedProjectileCounts[item.shoot] < 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
	}
}
