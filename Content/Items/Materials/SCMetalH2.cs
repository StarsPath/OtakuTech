using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Materials
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
