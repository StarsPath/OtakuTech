using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Content.Projectiles;
using OtakuTech.Common.Players;
using OtakuTech.Content.Projectiles.Minions;
using Terraria.GameContent;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
    public class HekatesGloom : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:A unique Pantheon weapon. Like Nuada's Grief and Skadi]" +
                "\n[c/AF4BFF:Ondurgud, it's made using mysterious Soulium with]" +
                "\n[c/AF4BFF:strange hues. The cross can turn into a sharp hovering]" +
                "\n[c/AF4BFF:sword. Countless built-in microblades allow it to unleash a]" +
                "\n[c/AF4BFF:devastating storm of attacks.]");
        }
        public override void SetDefaults()
        {
            Item.damage = 168;
            Item.crit = 25;
            Item.scale = 1f;
            //item.summon = true;
            Item.DamageType = DamageClass.Melee;
            Item.mana = 0;
            Item.width = 24;
            Item.height = 48;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<FangsOfDusk2>();
            Item.shootSpeed = 20f;
            Item.scale = 0.8f;

            Item.reuseDelay = 20;
            //item.buffType = ModContent.BuffType<Buffs.KeyOfReasonCannon>(); //The buff added to player after used the item
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        private int usage = 0;
        public override bool CanUseItem(Player player)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (player.altFunctionUse == 2)
            {
                if (modPlayer.crossDeploy)
                {
                    return false;
                }
                usage = 2;
                Item.DamageType = DamageClass.Summon;
                Item.mana = 10;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.useStyle = ItemUseStyleID.Shoot;

                Item.UseSound = SoundID.Item44;
                Item.shoot = ModContent.ProjectileType<HekatesCross>();
            }
            else
            {
                usage = 1;
                Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
                Item.mana = 0;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.UseSound = SoundID.Item1;
                Item.shoot = ModContent.ProjectileType<FangsOfDusk2>();
                Item.shootSpeed = 18f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (usage == 1)
            {
                modPlayer.addCombo();
                if (modPlayer.resetCombo(4))
                    type = ModContent.ProjectileType<FangsOfDusk3>();
                Vector2 offset = new Vector2(0, -8);
                position += offset;
                damage /= 2;
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                return false;
                //return base.Shoot(player, source, position+offset, velocity, ModContent.ProjectileType<FangsOfDusk3>(), damage/2, knockback);
            }
            else if (usage == 2)
            {
                position = Main.MouseWorld;
                if (!Collision.SolidCollision(position, 32, 52))
                {
                    modPlayer.crossDeploy = true;
                    Projectile.NewProjectile(source, position, new Vector2(0, 5), Item.shoot, damage, knockback, player.whoAmI);
                }
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (modPlayer.crossDeploy)
            {
                //Vector2 position4 = position - Main.inventoryBackTexture.Size() * Main.inventoryScale / 2f + Main.cdTexture.Size() * Main.inventoryScale / 2f;

                Vector2 center = position + new Vector2(frame.Width / 2 * scale, frame.Height / 2 * scale);
                Vector2 position2 = center - TextureAssets.Cd.Size() * Main.inventoryScale / 2f;
                Color white = Color.White;
                Texture2D CD = ModContent.Request<Texture2D>("OtakuTech/Assets/Textures/Other/Cooldown-2").Value;
                spriteBatch.Draw(CD, position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
            }
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }
}
