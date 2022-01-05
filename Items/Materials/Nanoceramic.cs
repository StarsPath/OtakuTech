using OtakuTech.NPCs.HonkaiBeasts;
using OtakuTech.NPCs.HonkaiBeasts.Eruption;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Materials
{
    public class Nanoceramic : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:Upgrade material for PRI weapons.]" +
                "\n[c/AF4BFF:A composite of ceramic nano-particles and other materials in a]" +
                "\n[c/AF4BFF:ceramic substrate. Offers high machinability as well as rigidity and]" +
                "\n[c/AF4BFF:hardness that surpasses metal alloy equivalent.]");
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
