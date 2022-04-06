using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Weapons
{
    public class TestStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used for Debugging");
        }

        public override void SetDefaults()
        {
            item.damage = 188;
            item.value = 10000;
            item.useTime = 12;
            item.useAnimation = 12;
            item.reuseDelay = 20;
            item.autoReuse = false;
            item.width = 32;
            item.height = 32;
            //item.scale = 0.5f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Yellow;
            item.shoot = ModContent.ProjectileType<Projectiles.EightFormations>();
        }

        //public override bool AltFunctionUse(Player player)
        //{
        //    return false;
        //}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 15*60);
            return false;
            //return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

    }
}
