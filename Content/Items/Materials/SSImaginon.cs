using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Materials
{
    public class SSImaginon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:Necessary material for forging weapons.]" +
                "\n[c/AF4BFF:An imaginon developed by Schicksal for investigating physics]" +
                "\n[c/AF4BFF:theories. The competition came up with another version with bad]" +
                "\n[c/AF4BFF:coloring. Schicksal is always right.]");
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
