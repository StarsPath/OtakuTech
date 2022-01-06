using OtakuTech.Items.Accessories;
using OtakuTech.Items.Materials;
using OtakuTech.Items.Weapons.FiveStars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Placeable
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
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 150;
			item.createTile = ModContent.TileType<Tiles.ExpansionSupply>();
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
                    ModContent.ItemType<VoidWings>(),
                    ModContent.ItemType<CarKey>(),
                    ModContent.ItemType<OathOfJudah>(),
                    ModContent.ItemType<ObscuringWing>(),
                    ModContent.ItemType<CrusherBunny19C>()
                };
                player.QuickSpawnItem(Utils.SelectRandom(Main.rand, lootTable));
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
                player.QuickSpawnItem(Utils.SelectRandom(Main.rand, lootTable));
            }
            else if (rng < tier1 + tier2 + tier3)
            {
                player.QuickSpawnItem(ItemID.GoldCoin, 1);
            }
            else
            {
                player.QuickSpawnItem(ItemID.SilverCoin, 50);
            }
        }
    }
}