using OtakuTech.NPCs.HonkaiBeasts;
using OtakuTech.NPCs.HonkaiBeasts.Eruption;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Materials
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
