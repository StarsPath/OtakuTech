using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Materials
{
    public class HonkaiShard: ModItem
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
            Item.value = 1000;
            Item.rare = ItemRarityID.Purple;
        }
    }
}
