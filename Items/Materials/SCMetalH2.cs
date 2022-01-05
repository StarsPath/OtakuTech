using OtakuTech.NPCs.HonkaiBeasts;
using OtakuTech.NPCs.HonkaiBeasts.Eruption;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Materials
{
    public class SCMetalH2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:Upgrade material for PRI weapons.]" +
                "\n[c/AF4BFF:Conductive material with excellent energy storage capacities.]" +
                "\n[c/AF4BFF:Created by subjecting liquid hydrogen to extreme pressures. Unlike]" +
                "\n[c/AF4BFF:naturally occurring metals, metallic hydrogen is not solid and must]" +
                "\n[c/AF4BFF:be stored in a special container.]");
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
