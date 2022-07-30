using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Materials
{
    public class Crystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/00BFFF:Mysterious crystals for buying anything in the Honkai-verse.]");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Purple;
            Item.value = 0;
        }
    }
}
