using OtakuTech.NPCs.HonkaiBeasts;
using OtakuTech.NPCs.HonkaiBeasts.Eruption;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Materials
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
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.value = 3000;
            item.UseSound = SoundID.Item1;
            item.useStyle = 1;
            item.rare = ItemRarityID.Purple;
        }
    }
}
