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
	public class ForbiddenSeed : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/4287f5:The embryo of some strange creature that has been sealed in a glass container.]\n"
							 //+ "Every minion on the field boosts Total DMG by 10%\n"
							 + "Every minion on the field boosts magic DMG by 8%\n"
							 + "Every minion on the field boosts ranged DMG by 8%\n"
							 + "Every minion on the field boosts damage reduction by 0.8%\n"
							 + "increases summon DMG by 60%\n"
							 + "+1 max minion and sentry\n"
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
			//player.GetDamage(DamageClass.Generic) +=  0.1f * player.numMinions;
			player.GetDamage(DamageClass.Magic) +=  0.08f * player.numMinions;
			player.GetDamage(DamageClass.Ranged) +=  0.08f * player.numMinions;
			player.GetDamage(DamageClass.Summon) +=  0.6f;
			player.endurance += 0.008f * player.numMinions;
			player.maxMinions += 1;
			player.maxTurrets += 1;
		}

		public override void UpdateEquip(Player player)
		{

        }
	}
}
