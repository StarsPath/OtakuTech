using Microsoft.Xna.Framework;
using OtakuTech.Content.Projectiles;
using OtakuTech.Projectiles;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
    public class ObscuringWing : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:In the twilight, the red-eyed hunter lurks among the trees.]" +
                "\n[c/AF4BFF:You can never get a close look at her, because when the]" +
                "\n[c/AF4BFF:dark wings flash across the sky, the arrowhead has already]" +
                "\n[c/AF4BFF:found its way to the heart of the prey.]");
        }

        public override void SetDefaults()
        {
            Item.damage = 186; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.crit = 11;
            Item.DamageType = DamageClass.Ranged; // sets the damage type to ranged
            Item.width = 26; // hitbox width of the item
            Item.height = 88; // hitbox height of the item
            Item.useTime = 12; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 12; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.reuseDelay = 18;
            Item.useStyle = ItemUseStyleID.Shoot; // how you use the item (swinging, holding out, etc)
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.value = 10000; // how much the item sells for (measured in copper)
            Item.rare = ItemRarityID.Purple; // the color that the item's name will be in-game
            Item.UseSound = SoundID.Item5; // The sound that this item plays when used.
            Item.autoReuse = true; // if you can hold click to automatically use it again
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            //item.shoot = ModContent.ProjectileType<FeatherBlade>();
            //item.shoot = ModContent.ProjectileType<Projectiles.CommandmentsProjectile>(); //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 30f; // the speed of the projectile (measured in pixels per frame)
            Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
                                         //item.scale = 0.75f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ProjectileID.None;
                Projectile[] current_feather = Main.projectile.Where(p => p.Name == "FeatherBlade" && p.owner == player.whoAmI).ToArray();
                foreach (Projectile p in current_feather)
                {
                    Vector2 dir = -(p.Center - player.Center);
                    dir.Normalize();

                    p.ai[0] = 1;
                    p.tileCollide = false;
                    p.penetrate = -1;
                    p.timeLeft += 120;
                    p.damage *= 10;
                }
            }
            else
            {
                Item.shoot = ProjectileID.WoodenArrowFriendly;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int spread = 8; //degrees
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(spread);
            position += Vector2.Normalize(velocity) * spread;
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FeatherBlade>(), damage / 5, knockback, player.whoAmI);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * Item.shootSpeed; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage / 2, knockback, player.whoAmI);
                //Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FeatherBlade>(), damage / 5, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
