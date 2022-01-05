using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Items.Weapons.FiveStars;
using OtakuTech.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Weapons
{
    public class ValkyrieJudah : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Eyes of wisdom will enlighten every entity on sight");
        }

        public override void SetDefaults()
        {
            item.damage = 176;
            item.crit = 20;
            item.value = 10000;
            item.useTime = 12;
            item.useAnimation = 12;
            item.reuseDelay = 20;
            item.autoReuse = true;
            item.width = 48;
            item.height = 122;
            //item.scale = 0.5f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.knockBack = 6;
            item.autoReuse = true;
            item.useTurn = true;
            item.melee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sunglasses);
            recipe.AddIngredient(ModContent.ItemType<OathOfJudah>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
