using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;
using OtakuTech.Content.Items.Weapons.FiveStars;
using OtakuTech.Content.Projectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace OtakuTech.Content.Items.Weapons.PRI
{
    public class DomainOfSentience : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("[c/AF4BFF:\"Hey, old timer, why do you use gauntlets?\"]" +
            //	"\n[c/AF4BFF:\"Be it sword, spear, or chain blade, flashy weapons can]" +
            //	"\n[c/AF4BFF:never bring out the true strength of a martial art master.\"]" +
            //	"\n[c/AF4BFF:\"Hmm, you should say that to Kevin.\"]");
            Tooltip.SetDefault("");
        }

        private int mode = 0;

        public override void SetDefaults()
        {
            Item.damage = 277;
            Item.crit = 33;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shootSpeed = 3.7f;
            Item.knockBack = 6.5f;
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(silver: 10);

            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item1;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //Main.NewText(modPlayer.comboCount);
            if (player.altFunctionUse == 2)
            {
                Vector2 dir = player.position - Main.MouseWorld;
                switch (mode)
                {
                    case 0:
                        dir.Normalize();
                        player.velocity += -dir * 15f;
                        player.immune = true;
                        player.immuneTime = 40;
                        player.immuneAlpha = 120;
                        Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(player, Item, AmmoID.None), player.Center, Vector2.Zero, ModContent.ProjectileType<SentienceSwordCharge>(), Item.damage, Item.knockBack, player.whoAmI);
                        break;
                    case 1:
                        dir.Normalize();
                        Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(player, Item, AmmoID.None), player.Center, -dir * 15f, ModContent.ProjectileType<SentienceSpearCharge>(), Item.damage, 0, player.whoAmI);
                        break;
                    case 2:
                        for(int i = -3; i < 4; i++)
                        {
                            Projectile p = Projectile.NewProjectileDirect(new EntitySource_ItemUse_WithAmmo(player, Item, AmmoID.None), player.Center + new Vector2(i * 64, 196), new Vector2(0, -56).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 30))), ModContent.ProjectileType<SentienceChainCharge>(), Item.damage, 0, player.whoAmI);
                        }
                        break;
                }
                mode++;
                mode %= 3;
                //return false;
            }
            switch (mode)
            {
                case 0:
                    Item.CloneDefaults(ModContent.ItemType<SentienceSword>());
                    Item.damage = 277;
                    Item.crit = 33;
                    Item.shoot = ModContent.ProjectileType<SentienceSwordSwing>();
                    Item.noMelee = true;
                    Item.noUseGraphic = true;
                    break;
                case 1:
                    Item.CloneDefaults(ModContent.ItemType<SentienceSpear>());
                    Item.damage = 277;
                    Item.crit = 33;
                    Item.autoReuse = true;
                    if (modPlayer.comboCount >= 2)
                    {
                        Item.shoot = ModContent.ProjectileType<SentienceSpearCombo>();
                        Item.useStyle = ItemUseStyleID.Swing;
                        Item.noMelee = true;
                        Item.noUseGraphic = true;
                    }
                    else
                    {
                        Item.shoot = ModContent.ProjectileType<SentienceSpearProjectile>();
                        Item.useStyle = ItemUseStyleID.Shoot;
                        Item.noMelee = true;
                        Item.noUseGraphic = true;
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<SentienceSpearProjectile>()] < 1)
                    {
                        return true;
                    }
                    return false;
                case 2:
                    Item.CloneDefaults(ModContent.ItemType<SentienceChain>());
                    Item.damage = 277;
                    Item.crit = 33;
                    Item.autoReuse = true;
                    Item.shoot = ModContent.ProjectileType<SentienceWhip>();
                    break;
            }
            return base.CanUseItem(player);
            //return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //Main.NewText("SHOOT" + modPlayer.comboCount);
            switch (mode)
            {
                case 0:
                    //if (Main.rand.NextFloat() >= .66f)
                    Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(player, Item, AmmoID.None), position, velocity, ModContent.ProjectileType<SentienceSwordProjectile>(), damage, knockback, player.whoAmI);
                    break;

                case 1:
                    //Main.NewText(modPlayer.comboCount + "Adding Combo");
                    //if (modPlayer.comboCount == 1)
                    //{
                    //    modPlayer.addCombo();
                    //    modPlayer.resetCombo(3);
                    //    return false;
                    //}
                    modPlayer.addCombo();
                    modPlayer.resetCombo(3);
                    break;

                case 2:
                    break;

            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);

        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //base.ModifyTooltips(tooltips);
            //TooltipLine line = new TooltipLine(Mod, "description", "[c/AF4BFF:\"Hey, old timer, why do you use gauntlets?\"]" +
            //	"\n[c/AF4BFF:\"Be it sword, spear, or chain blade, flashy weapons can]" +
            //	"\n[c/AF4BFF:never bring out the true strength of a martial art master.\"]" +
            //	"\n[c/AF4BFF:\"Hmm, you should say that to Kevin.\"]");
            TooltipLine line = new TooltipLine(Mod, "description1", "\"Why do you use such weird weapons?\"")
            {
                OverrideColor = new Color(255, 255, 10)
            };
            tooltips.Add(line);
            line = new TooltipLine(Mod, "description1", "\"Cuz I can and fists are boring\"")
            {
                OverrideColor = new Color(255, 255, 10)
            };
            tooltips.Add(line);
            line = new TooltipLine(Mod, "description1", "\"What's with the weird names?\"")
            {
                OverrideColor = new Color(255, 255, 10)
            };
            tooltips.Add(line);
            line = new TooltipLine(Mod, "description1", "\"It's humor.\"")
            {
                OverrideColor = new Color(255, 255, 10)
            };
            tooltips.Add(line);
        }
    }
}
