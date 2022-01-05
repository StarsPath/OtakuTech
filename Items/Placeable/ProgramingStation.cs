using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Placeable
{
	public class ProgramingStation : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Used to create high-tech items");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 14;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 150;
			item.createTile = ModContent.TileType<Tiles.ProgramingStation>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wire, 99);
			recipe.AddIngredient(ItemID.TinkerersWorkshop);
			recipe.AddRecipeGroup("OtakuTech:LargeGems");
			recipe.AddRecipeGroup("OtakuTech:PlatinumGold", 20);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}