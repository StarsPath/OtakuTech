using OtakuTech.Items.Accessories;
using OtakuTech.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Placeable
{
    public class ExperimentalSupply : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Standard Supply Crate");
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
            item.createTile = ModContent.TileType<Tiles.ExperimentalSupply>();
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void RightClick(Player player)
        {
            int[] lootTable1 =
            {
                ModContent.ItemType<EinsteinsTorus>(),
                ModContent.ItemType<SCMetalH2>(),
                ModContent.ItemType<Nanoceramic>(),
                ModContent.ItemType<AEImaginon>(),
                ModContent.ItemType<SSImaginon>(),
                ModContent.ItemType<HonkaiCube>(),
                ModContent.ItemType<HonkaiShard>(),
                ModContent.ItemType<ExpansionSupplyItem>(),
                ModContent.ItemType<FocusSupplyItem>(),
                ModContent.ItemType<StandardSupplyItem>()
            };

            int[] lootTable2 =
            {
                ModContent.ItemType<VoidWings>(),
                ModContent.ItemType<CarKey>(),
                ModContent.ItemType<ProgramingStation>()
            };

            foreach (int item in lootTable1)
            {
                player.QuickSpawnItem(item, 99);
            }
            foreach (int item in lootTable2)
            {
                player.QuickSpawnItem(item);
            }
        }
    }
}