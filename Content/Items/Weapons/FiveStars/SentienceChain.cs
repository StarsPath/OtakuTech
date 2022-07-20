using OtakuTech.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
    class SentienceChain : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.knockBack = 2;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.value = 10000;

            Item.width = 23;
            Item.height = 23;
            
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item152;

            Item.shoot = ModContent.ProjectileType<SentienceWhip>();
            Item.shootSpeed = 4;

            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }
    }
}
