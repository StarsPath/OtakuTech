using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Materials
{
    public class AEImaginon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:Necessary material for forging weapons.]" +
                "\n[c/AF4BFF:An imaginon developed by Anti-Entropy (AE) for investigating]" +
                "\n[c/AF4BFF:physics theories. The competition came up with another version]" +
                "\n[c/AF4BFF:with bad coloring. AE is always cool.]");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.value = 3000;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = 1;
            Item.rare = ItemRarityID.Purple;
        }
    }
}
