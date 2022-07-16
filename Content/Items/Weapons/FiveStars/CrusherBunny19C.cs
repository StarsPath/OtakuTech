using Microsoft.Xna.Framework;
using OtakuTech.Common.Players;
using OtakuTech.Content.Projectiles.Minions;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
    public class CrusherBunny19C : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:\"How could you have the heart to eat adorable bunnies ?]" +
                "\n[c/AF4BFF:Aren't you scared of being eaten by bunnies?\" Says]" +
                "\n[c/AF4BFF:Haxxor Bunny Bronie.]");
        }

        public override void SetDefaults()
        {
            Item.damage = 186; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.crit = 11;
            Item.DamageType = DamageClass.Ranged; // sets the damage type to ranged
            Item.width = 40; // hitbox width of the item
            Item.height = 24; // hitbox height of the item
            Item.useTime = 10; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 20; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.reuseDelay = 5;
            Item.useStyle = ItemUseStyleID.Shoot; // how you use the item (swinging, holding out, etc)
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.value = 10000; // how much the item sells for (measured in copper)
            Item.rare = ItemRarityID.Purple; // the color that the item's name will be in-game
            //item.UseSound = SoundID.Item40; // The sound that this item plays when used.
            Item.autoReuse = true; // if you can hold click to automatically use it again
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 30f; // the speed of the projectile (measured in pixels per frame)
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
            Item.scale = 0.75f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(3, 0);
            //return base.HoldoutOffset();
        }

        public override bool CanUseItem(Player player)
        {
            ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
            moddedPlayer.addCombo();
            if (moddedPlayer.comboCount >= 10)
            {
                moddedPlayer.comboCount = 0;
                if (moddedPlayer.haxxorDroneCount < moddedPlayer.MAX_haxxorDroneCount)
                {
                    moddedPlayer.haxxorDroneCount++;
                    int p = Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, default, ModContent.ProjectileType<HaxxorDrones>(), Item.damage / 2, Item.knockBack, Owner: player.whoAmI);
                }

            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item40);
            return base.Shoot(player, source,  position, velocity, type, damage, knockback);
        }

    }
}
