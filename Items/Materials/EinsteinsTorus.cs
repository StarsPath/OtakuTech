using OtakuTech.NPCs.HonkaiBeasts;
using OtakuTech.NPCs.HonkaiBeasts.Eruption;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Materials
{
    public class EinsteinsTorus : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:An upgrade material for PRI weapons.]" +
                "\n[c/AF4BFF:A toroidal device that uses powerful magnetic fields to confine]" +
                "\n[c/AF4BFF:nuclear fusion reactions (also known as a \"Tokomak\"). Dr. Einstein]" +
                "\n[c/AF4BFF:miniaturized this but it is still far from perfect.]");
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
