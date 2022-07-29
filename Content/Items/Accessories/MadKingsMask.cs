using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Beard)]
	public class MadKingsMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/4287f5:A mask that combines lightness with durability,]"
							 + "\n[c/4287f5:although its practicality as armor is nonetheless suspect.]\n"
							 //+ "Every 10 HP loss boosts Total DMG by 0.1%\n"
							 + "Every 10 HP loss boosts melee DMG by 0.8%\n"
							 + "Every 10 HP loss boosts magic DMG by 0.8%\n"
							 + "Every 10 HP loss boosts damage reduction by 0.08%\n"
							 + "increases max hp by 2.5%\n");

			//CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.vanity = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
			Item.rare = ItemRarityID.Cyan;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			float hpLoss = (player.statLifeMax2 - player.statLife) / 10f;
			//player.GetDamage(DamageClass.Generic) += hpLoss * 0.001f;
			player.GetDamage(DamageClass.Melee) += hpLoss * 0.008f;
			player.GetDamage(DamageClass.Magic) += hpLoss * 0.008f;
			player.endurance += hpLoss * 0.0008f;
		}

        public override void UpdateEquip(Player player)
        {
			player.statLifeMax2 = (int)(player.statLifeMax * 1.025f);
			//base.UpdateEquip(player);
        }
    }
}
