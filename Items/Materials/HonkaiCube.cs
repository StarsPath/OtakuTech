using OtakuTech.NPCs.HonkaiBeasts;
using OtakuTech.NPCs.HonkaiBeasts.Eruption;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Materials
{
    public class HonkaiCube: ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:A very rare aggregation of Honkai Energy.]");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.value = 3000;
            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item1;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = ItemRarityID.Purple;
        }

        public override bool UseItem(Player player)
        {
            if (!HonkaiWorld.honkaiInvasionActive)
            {
                Main.NewText("The Honkai Eruption is starting.......", 175, 75, 255, false);
                Eruption.startInvasion();
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.HonkaiShard>(), 3);
            recipe.AddTile(ModContent.TileType<Tiles.ProgramingStation>());
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
