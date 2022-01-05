using OtakuTech.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OtakuTech.Projectiles.Minions;
using OtakuTech.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using OtakuTech.Items.Weapons.FiveStars;
using OtakuTech.Items.Materials;

namespace OtakuTech.Items.Weapons.PRI
{
    public class HekatesSombre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/ffff0a:The PRI-ARM form of Hekate's Gloom. Reforged based on]" +
                "\n[c/ffff0a:the battle data from the Twilight Paladin battlesuit, it's now]" +
                "\n[c/ffff0a:even more deadly and harnesses Honkai energy with]" +
                "\n[c/ffff0a:higher efficiency.]");
        }
        public override void SetDefaults()
        {
            item.damage = 279;
            item.crit = 36;
            item.scale = 1f;
            //item.summon = true;
            item.melee = true;
            item.mana = 0;
            item.width = 24;
            item.height = 48;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<FangsOfDusk2>();
            item.shootSpeed = 20f;
            item.scale = 0.8f;

            item.reuseDelay = 20;
            //item.buffType = ModContent.BuffType<Buffs.KeyOfReasonCannon>(); //The buff added to player after used the item
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<HekatesGloom>());
            recipe.AddIngredient(ModContent.ItemType<EinsteinsTorus>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SCMetalH2>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Nanoceramic>(), 10);
            recipe.AddTile(ModContent.TileType<ProgramingStation>());
            recipe.SetResult(this);
            recipe.AddRecipe();
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
                item.useTime = 20;
                item.useAnimation = 20;
                item.useStyle = ItemUseStyleID.HoldingOut;

                item.UseSound = SoundID.Item44;
                item.shoot = ModContent.ProjectileType<HekatesSombreCross>();
                //player.AddBuff(ModContent.BuffType<Buffs.Fervent>(), 15 * 60);
            }
            else
            {
                usage = 1;
                item.melee = true;
                item.summon = false;
                item.mana = 0;
                item.useTime = modPlayer.fervent ? 20 : 30;
                item.useAnimation = modPlayer.fervent ? 20 : 30;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.UseSound = SoundID.Item1;
                item.shootSpeed = 18f;

                item.reuseDelay = modPlayer.fervent ? 5 : 20;

                    //if (modPlayer.fervent)
                    //{
                    //    item.shootSpeed = 1f;
                    //    item.useTime = 20;
                    //    item.useAnimation = 20;
                    //    item.shoot = ProjectileID.None;
                    //    comboAttack(modPlayer, player);
                    //    return base.CanUseItem(player);
                    //}
                    //else
                    //{
                    //    item.shootSpeed = 20f;
                    //    item.useTime = 20;
                    //    item.useAnimation = 20;
                    //    item.shoot = ProjectileID.None;
                    //    basicAttack(modPlayer, player);
                    //    return base.CanUseItem(player);

                    //}

                    //if (modPlayer.comboCount >= 4)
                    //{
                    //    modPlayer.comboCount = 0;
                    //    item.shoot = ModContent.ProjectileType<FangsOfDusk3>();
                    //}
                    //else
                    //{
                    //    item.shoot = ModContent.ProjectileType<FangsOfDusk2>();
                    //}

                    item.shoot = ModContent.ProjectileType<FangsOfDusk2>();
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
        public void comboAttack(ModdedPlayer moddedPlayer, Player player)
        {
            moddedPlayer.addCombo();
            Vector2 dir = -(player.Center - Main.MouseWorld);
            dir.Normalize();
            player.direction = Utils.ToDirectionInt(dir.X > 0);
            switch (moddedPlayer.comboCount)
            {
                case 1:
                    //item.shoot = ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo1>();
                    Projectile.NewProjectile(player.Center, dir * 1f, ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo1>(), item.damage, item.knockBack, Main.myPlayer);
                    break;
                case 2:
                    //item.shoot = ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo2>();
                    Projectile.NewProjectile(player.Center, dir * 1f, ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo2>(), item.damage, item.knockBack, Main.myPlayer);
                    break;
                case 3:
                    //item.shoot = ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo3>();
                    Projectile.NewProjectile(player.Center, dir * 1f, ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo3>(), item.damage, item.knockBack, Main.myPlayer);
                    break;
                case 4:
                    Projectile.NewProjectile(player.Center, dir * 5f, ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo4>(), item.damage, item.knockBack, Main.myPlayer);
                    //item.shootSpeed = 10f;
                    //item.shoot = ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo4>();
                    break;
                case 5:
                    moddedPlayer.comboCount = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 pointPoisition = player.RotatedRelativePoint(player.MountedCenter);
                        float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                        Vector2 vector50 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(20, 60, Main.rand.NextFloat());
                        for (int num172 = 0; num172 < 50; num172++)
                        {
                            vector50 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(20, 60, Main.rand.NextFloat());
                            if (Collision.CanHit(pointPoisition, 0, 0, vector50 + (vector50 - pointPoisition).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
                            {
                                break;
                            }
                            f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                        }

                        dir = -(vector50 - Main.MouseWorld);
                        dir.Normalize();
                        int p = Projectile.NewProjectile(vector50, dir * 20f, ModContent.ProjectileType<FangsOfDusk>(), item.damage, item.knockBack, Main.myPlayer);
                        Main.projectile[p].penetrate = 3;
                    }
                    //item.shootSpeed = 10f;
                    //item.shoot = ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo4>();
                    break;
                default:
                    moddedPlayer.comboCount = 0;
                    break;
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //Main.NewText(modPlayer.comboCount);
            if (usage == 1)
            {
                modPlayer.addCombo();
                if (modPlayer.fervent)
                {
                    damage *= 2;
                    switch (modPlayer.comboCount)
                    {
                        case 1:
                            type = ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo1>();
                            break;
                        case 2:
                            type = ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo2>();
                            break;
                        case 3:
                            type = ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo3>();
                            break;
                        case 4:
                            type = ModContent.ProjectileType<Projectiles.FerventCombo.FerventCombo4>();
                            speedX /= 6;
                            speedY /= 6;
                            break;
                        case 5:
                            modPlayer.comboCount = 0;
                            type = ProjectileID.None;
                            Vector2 dir = -(player.Center - Main.MouseWorld);
                            dir.Normalize();
                            player.direction = Utils.ToDirectionInt(dir.X > 0);
                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 pointPoisition = player.RotatedRelativePoint(player.MountedCenter);
                                float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                                Vector2 vector50 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(20, 60, Main.rand.NextFloat());
                                for (int num172 = 0; num172 < 50; num172++)
                                {
                                    vector50 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(20, 60, Main.rand.NextFloat());
                                    if (Collision.CanHit(pointPoisition, 0, 0, vector50 + (vector50 - pointPoisition).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
                                    {
                                        break;
                                    }
                                    f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                                }

                                dir = -(vector50 - Main.MouseWorld);
                                dir.Normalize();
                                int p = Projectile.NewProjectile(vector50, dir * 20f, ModContent.ProjectileType<FangsOfDusk>(), item.damage, item.knockBack, Main.myPlayer);
                                Main.projectile[p].penetrate = 3;
                            }
                            break;
                        default:
                            modPlayer.comboCount = 0;
                            break;
                    }
                }
                else if (modPlayer.comboCount >= 4)
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
                    player.AddBuff(ModContent.BuffType<Buffs.Fervent>(), 15 * 60);
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
