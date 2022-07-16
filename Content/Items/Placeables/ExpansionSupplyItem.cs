using OtakuTech.Content.Items.Materials;
using OtakuTech.Content.Items.Weapons.FiveStars;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Placeables
{
	public class ExpansionSupplyItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Expansion Supply Crate");
			Tooltip.SetDefault("Chances to obtain coins and upgrade materials");
		}

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 150;
			Item.createTile = ModContent.TileType<Tiles.ExpansionSupply>();
		}

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            float rng = Main.rand.NextFloat();
            float tier1 = 0.1146f;      //honkai cube frag
            float tier2 = 0.2050f;      //upgrade materials
            float tier3 = 0.3119f;      //silver coins x50
            float tier4 = 0.3685f;      //silver coins x10

            if (rng < tier1)
            {
                int[] lootTable =
                {
                    ModContent.ItemType<HonkaiCube>(),
                    ModContent.ItemType<HonkaiShard>(),
                    //ModContent.ItemType<VoidWings>(),
                    //ModContent.ItemType<CarKey>(),
                    ModContent.ItemType<OathOfJudah>(),
                    ModContent.ItemType<ObscuringWing>(),
                    ModContent.ItemType<CrusherBunny19C>()
                };
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, ModContent.ItemType<ExpansionSupplyItem>()), Utils.SelectRandom(Main.rand, lootTable)); ;
            }
            else if (rng < tier1 + tier2)
            {
                int[] lootTable =
                {
                    ModContent.ItemType<HonkaiShard>(),
                    ModContent.ItemType<EinsteinsTorus>(),
                    ModContent.ItemType<SCMetalH2>(),
                    ModContent.ItemType<Nanoceramic>(),
                    ModContent.ItemType<AEImaginon>(),
                    ModContent.ItemType<SSImaginon>()
                };
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, ModContent.ItemType<ExpansionSupplyItem>()), Utils.SelectRandom(Main.rand, lootTable));
            }
            else if (rng < tier1 + tier2 + tier3)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, ModContent.ItemType<ExpansionSupplyItem>()), ItemID.GoldCoin, 1);
            }
            else
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, ModContent.ItemType<ExpansionSupplyItem>()), ItemID.SilverCoin, 50);
            }
        }
    }
}