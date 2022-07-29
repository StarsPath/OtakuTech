using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Accessories
{
	public class Burden : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/4287f5:A small tongue of crimson flame sealed within solid ice.]\n"
							 //+ "+35% increased damage\n"
							 + "+30% increased melee damage\n"
							 + "+30% increased magic damage\n"
							 + "+25 increased melee damage penetration\n"
							 + "+20 increased magic damage penetration\n");
							 //+"20% increased damage reduction while holding melee item"); ;

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
			//if(player.HeldItem.DamageType == DamageClass.Melee)
   //         {
			//	player.endurance += 0.2f;
			//}
			//player.GetDamage(DamageClass.Generic) += 0.035f;
			player.GetDamage(DamageClass.Melee) += 0.30f;
			player.GetDamage(DamageClass.Magic) += 0.30f;
			player.GetArmorPenetration(DamageClass.Melee) += 25;
			player.GetArmorPenetration(DamageClass.Magic) += 20;
		}
	}
}
