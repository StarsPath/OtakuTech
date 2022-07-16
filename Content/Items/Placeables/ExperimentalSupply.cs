using OtakuTech.Content.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Placeables
{
    public class ExperimentalSupply : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Experimental Supply Crate");
            Tooltip.SetDefault("Right click to open supply" +
                "\nExperimental Supply, Gives all items from this mod." +
                "\nUse for debugging purposes. This mod is still WIP. You are" +
                "\nwelcomed to use this item to try out all the toys");
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
            Item.createTile = ModContent.TileType<Tiles.ExperimentalSupply>();
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.Register();
        }

        public override void RightClick(Player player)
        {
            EntitySource_ItemOpen source = new EntitySource_ItemOpen(player, ModContent.ItemType<ExperimentalSupply>());
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
                //ModContent.ItemType<VoidWings>(),
                //ModContent.ItemType<CarKey>(),
                ModContent.ItemType<ProgramingStation>()
            };

            foreach (int item in lootTable1)
            {
                player.QuickSpawnItem(source, item, 99);
            }
            foreach (int item in lootTable2)
            {
                player.QuickSpawnItem(source, item);
            }
        }
    }
}