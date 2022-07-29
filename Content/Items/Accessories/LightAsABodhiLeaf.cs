using OtakuTech.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Accessories
{
	public class LightAsABodhiLeaf : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/4287f5:An ordinary leaf that falls from the tree of Sumeru.]\n"
							 //+ "+0.1% increased damage\n"
							 + "Gain a massive buff after hitting enemies 150 times that:\n"
							 + "+35% increased melee damage\n"
							 + "+35% increased ranged damage\n"
							 + "+3.2% increased damage reduction\n"
							 + "Hit count is reset after granting buff or when accessory removed\n");
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
			AttackPlayer attackPlayer = player.GetModPlayer<AttackPlayer>();
			attackPlayer.bohiLeafEquiped = true;
		}

		public class AttackPlayer : ModPlayer
        {
			public int hitCount;
			public bool bohiLeafEquiped = false;

            public override void ResetEffects()
            {
				bohiLeafEquiped = false;
                base.ResetEffects();
            }
            public override void OnHitAnything(float x, float y, Entity victim)
            {
				if(bohiLeafEquiped)
				{
					hitCount += 1;
					//Main.NewText("ADD HIT" + hitCount);

					if (hitCount >= 150)
					{
						hitCount = 0;
						Player.AddBuff(ModContent.BuffType<SignetsOfBodhi>(), 10 * 60);
					}
				}
                base.OnHitAnything(x, y, victim);
            }
        }
	}
}
