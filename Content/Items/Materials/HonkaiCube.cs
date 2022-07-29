using Microsoft.Xna.Framework;
using OtakuTech.Common.Eruption;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Materials
{
    public class HonkaiCube : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:A very rare aggregation of Honkai Energy.]");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.value = 3000;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.rare = ItemRarityID.Purple;
        }

        public override Nullable<bool> UseItem(Player player)
        {
            if (!HonkaiWorld.honkaiInvasionActive)
            {
                Main.NewText("The Honkai Eruption is starting.......", new Color(175, 75, 255));
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
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<Materials.HonkaiShard>(), 3);
            recipe.AddTile(ModContent.TileType<Tiles.ProgramingStation>());
            recipe.Register();
        }
    }
}
