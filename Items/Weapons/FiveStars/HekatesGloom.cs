using OtakuTech.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OtakuTech.Projectiles.Minions;
using OtakuTech.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OtakuTech.Items.Weapons.FiveStars
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
            item.damage = 168;
            item.crit = 25;
            item.scale = 1f;
            //item.summon = true;
            item.melee = true;
            item.mana = 0;
            item.width = 24;
            item.height = 48;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<FangsOfDusk2>();
            item.shootSpeed = 20f;
            item.scale = 0.8f;

            item.reuseDelay = 20;
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
                item.melee = false;
                item.summon = true;
                item.mana = 10;
                item.useTime = 30;
                item.useAnimation = 30;
                item.useStyle = ItemUseStyleID.HoldingOut;

                item.UseSound = SoundID.Item44;
                item.shoot = ModContent.ProjectileType<HekatesCross>();
            }
            else
            {
                usage = 1;
                item.melee = true;
                item.summon = false;
                item.mana = 0;
                item.useTime = 30;
                item.useAnimation = 30;
                item.useStyle = ItemUseStyleID.SwingThrow;

                //item.shoot = ProjectileID.None;
                //basicAttack(modPlayer, player);
                //return base.CanUseItem(player);

                item.UseSound = SoundID.Item1;
                //if (modPlayer.comboCount >= 4)
                //{
                //    modPlayer.comboCount = 0;
                //    item.shoot = ModContent.ProjectileType<FangsOfDusk3>();
                //}
                //else
                item.shoot = ModContent.ProjectileType<FangsOfDusk2>();
                item.shootSpeed = 18f;
            }
            return base.CanUseItem(player);
        }

        public void basicAttack(ModdedPlayer moddedPlayer, Player player)
        {
            moddedPlayer.addCombo();
            Vector2 dir = -(player.Center - Main.MouseWorld);
            dir.Normalize();
            player.direction = Utils.ToDirectionInt(dir.X > 0);
            switch (moddedPlayer.comboCount)
            {
                case 1:
                case 2:
                case 3:
                    Projectile.NewProjectile(player.Center, dir * item.shootSpeed, ModContent.ProjectileType<FangsOfDusk2>(), item.damage / 2, item.knockBack, Main.myPlayer);
                    break;
                case 4:
                    moddedPlayer.comboCount = 0;
                    Projectile.NewProjectile(player.Center, dir * item.shootSpeed, ModContent.ProjectileType<FangsOfDusk3>(), item.damage / 2, item.knockBack, Main.myPlayer);
                    break;
                default:
                    moddedPlayer.comboCount = 0;
                    break;
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (usage == 1)
            {
                modPlayer.addCombo();
                if (modPlayer.comboCount >= 4)
                {
                    modPlayer.comboCount = 0;
                    type = ModContent.ProjectileType<FangsOfDusk3>();
                }
                Vector2 offset = new Vector2(0, -8);
                position += offset;
                damage /= 2;
            }
            else if (usage == 2)
            {
                position = Main.MouseWorld;
                speedX = 0;
                speedY = 5;
                if (!Collision.SolidCollision(position, 32, 52))
                {
                    modPlayer.crossDeploy = true;
                    return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
                }
                return false;
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Player player = Main.player[item.owner];
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (modPlayer.crossDeploy)
            {
                //Vector2 position4 = position - Main.inventoryBackTexture.Size() * Main.inventoryScale / 2f + Main.cdTexture.Size() * Main.inventoryScale / 2f;

                Vector2 center = position + new Vector2(frame.Width / 2 * scale, frame.Height / 2 * scale);
                Vector2 position2 = center - Main.cdTexture.Size() * Main.inventoryScale / 2f;
                Color white = Color.White;
                spriteBatch.Draw(ModLoader.GetMod("OtakuTech").GetTexture("Other/Cooldown-2"), position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
                //spriteBatch.Draw(Main.cdTexture, frame, white);
                /*spriteBatch.Draw(Main.cdTexture, new Rectangle((int)position.X, (int)position.Y, 
					(int)(Main.cdTexture.Size().X * Main.inventoryScale), (int)(Main.cdTexture.Size().Y * Main.inventoryScale)), white);*/
                //Main.NewText(frame);
                //Main.NewText(position);
                //Main.NewText(position2);
            }
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }
}
