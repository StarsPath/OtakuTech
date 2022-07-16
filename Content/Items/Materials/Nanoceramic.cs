using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Materials
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
