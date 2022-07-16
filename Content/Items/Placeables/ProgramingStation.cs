using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Placeables
{
	public class ProgramingStation : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Used to create high-tech items");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 14;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 150;
			Item.createTile = ModContent.TileType<Tiles.ProgramingStation>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Wire, 99);
			recipe.AddIngredient(ItemID.TinkerersWorkshop);
			recipe.AddRecipeGroup("OtakuTech:LargeGems");
			recipe.AddRecipeGroup("OtakuTech:PlatinumGold", 20);
			recipe.Register();
		}
	}
}