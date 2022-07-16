using OtakuTech.Content.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Placeables
{
    public class StandardSupplyItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Standard Supply Crate");
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
            Item.createTile = ModContent.TileType<Tiles.StandardSupply>();
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            EntitySource_ItemOpen source = new EntitySource_ItemOpen(player, ModContent.ItemType<StandardSupplyItem>());

            float rng = Main.rand.NextFloat();
            float tier1 = 0.1146f;      //honkai cube frag
            float tier2 = 0.2050f;      //upgrade materials
            float tier3 = 0.3119f;      //silver coins x50
            float tier4 = 0.3685f;      //silver coins x10

            if (rng < tier1)
            {
                player.QuickSpawnItem(source, ModContent.ItemType<HonkaiShard>());
            }
            else if (rng < tier1 + tier2)
            {
                int[] lootTable =
                {
                    ModContent.ItemType<EinsteinsTorus>(),
                    ModContent.ItemType<SCMetalH2>(),
                    ModContent.ItemType<Nanoceramic>(),
                    ModContent.ItemType<AEImaginon>(),
                    ModContent.ItemType<SSImaginon>()
                };
                player.QuickSpawnItem(source, Utils.SelectRandom(Main.rand, lootTable), Main.rand.Next(1, 3));
            }
            else if (rng < tier1 + tier2 + tier3)
            {
                player.QuickSpawnItem(source, ItemID.SilverCoin, 50);
            }
            else
            {
                player.QuickSpawnItem(source, ItemID.SilverCoin, 10);
            }
        }
    }
}