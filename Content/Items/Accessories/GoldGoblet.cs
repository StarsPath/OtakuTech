using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Accessories
{
	public class GoldGoblet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/4287f5:A wine goblet made of gold. It ever runs over with fine wine.]\n"
							 //+ "Every 10 mana left boosts Total DMG by 0.30%\n"
							 + "Every 10 mana left boosts magic DMG by 2.5%\n"
							 + "Every 10 mana left boosts ranged DMG by 2.5%\n"
							 + "Every 10 mana left boosts damage reduction by 0.25%\n"
							 + "increases max mana by 30\n"
							 + "increases mana regen\n"
							 + "decreases mana regen delay\n");
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
			float spLeft = player.statMana / 10f;
			//float spMissing = (float)(player.statManaMax - player.statMana) / player.statManaMax;
			//player.GetDamage(DamageClass.Generic) += spLeft * 0.003f;
			player.GetDamage(DamageClass.Magic) += spLeft * 0.025f;
			player.GetDamage(DamageClass.Ranged) += spLeft * 0.025f;
			player.endurance += spLeft * 0.0025f;
			//player.manaRegen = (int)(player.manaRegen * (1 + spMissing) * 1f);
		}

		public override void UpdateEquip(Player player)
		{
			player.manaRegenBonus += 20;
			//player.statManaMax = 200;
			player.manaRegenDelayBonus += 2;
            player.statManaMax2 += 30;
            //base.UpdateEquip(player);
        }
	}
}
