using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Materials
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
