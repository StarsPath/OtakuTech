using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Weapons.PRI
{
    public class DomainOfVoid : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("50 mana to teleport"+
                "\n[c/ffff0a:Herrscher of the Void, hidden within K423, has fallen into a ]" +
                "\n[c/ffff0a:deep slumber for a long time. But her will remains strong.]" +
                "\n[c/ffff0a:As her power grows, these oddly shaped dual pistols sealed]" +
                "\n[c/ffff0a:within the Imaginary Space have gradually changed form.]");
        }

        private float teleportCD = 300;

        public override void SetDefaults()
        {
            Item.damage = 272;
            Item.crit = 29;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.useTime = 5;
            Item.useAnimation = 20;
            Item.reuseDelay = 20;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item105;
            Item.shoot = ModContent.ProjectileType<Projectiles.CommandmentsProjectile>();
            Item.shootSpeed = 30f;
            //item.useAmmo = AmmoID.None;
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ModContent.ItemType<KeyOfVoid>());
        //    recipe.AddIngredient(ModContent.ItemType<EinsteinsTorus>(), 10);
        //    recipe.AddIngredient(ModContent.ItemType<SCMetalH2>(), 10);
        //    recipe.AddIngredient(ModContent.ItemType<Nanoceramic>(), 10);
        //    recipe.AddTile(ModContent.TileType<ProgramingStation>());
        //    recipe.Register();
        //}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
            if (player.altFunctionUse == 2 && (player.statMana < 50 || modplayer.voidTeleportCD > 0))
            {
                return false;
            }
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.shoot = ProjectileID.None;
                var mousePos = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                if (player.gravDir == 1f)
                {
                    mousePos.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
                }
                else
                {
                    mousePos.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                }
                mousePos.X -= player.width / 2;

                if (!Collision.SolidCollision(mousePos, player.width, player.height) && player.statMana > 50 && modplayer.voidTeleportCD <= 0)
                {
                    Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(player, Item, AmmoID.None), player.Center, default, ModContent.ProjectileType<Projectiles.BlackHole>(), Item.damage / 10, 0, Main.myPlayer);
                    modplayer.voidTeleportCD = teleportCD;
                    player.statMana -= 50;
                    player.Teleport(mousePos, 4);
                }
            }
            else
            {
                Item.useTime = 3;
                Item.useAnimation = 12;
                Item.shoot = ModContent.ProjectileType<Projectiles.CommandmentsProjectile>();
                modplayer.addCombo();
                //Main.NewText(modplayer.voidConsecutiveHit);
            }
            return base.CanUseItem(player);
        }

        public override void UpdateInventory(Player player)
        {

            base.UpdateInventory(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();

            Vector2 pointPoisition = player.RotatedRelativePoint(player.MountedCenter);
            float num2 = (float)Main.mouseX + Main.screenPosition.X - pointPoisition.X;
            float num3 = (float)Main.mouseY + Main.screenPosition.Y - pointPoisition.Y;

            float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
            float value3 = 20f;
            float value4 = 60f;
            Vector2 vector50 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(value3, value4, Main.rand.NextFloat());
            for (int num172 = 0; num172 < 50; num172++)
            {
                vector50 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(value3, value4, Main.rand.NextFloat());
                if (Collision.CanHit(pointPoisition, 0, 0, vector50 + (vector50 - pointPoisition).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
                {
                    break;
                }
                f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
            }
            Vector2 v5 = Main.MouseWorld - vector50;
            Vector2 vector51 = new Vector2(num2, num3).SafeNormalize(Vector2.UnitY) * Item.shootSpeed;
            v5 = v5.SafeNormalize(vector51) * Item.shootSpeed;
            //v5 = Vector2.Lerp(v5, vector51, 0.25f);
            Projectile.NewProjectile(source, vector50, v5, ModContent.ProjectileType<Projectiles.CommandmentsProjectile>(), damage, knockback, player.whoAmI);

            position = vector50;

            float num108 = 16f;
            for (int num109 = 0; (float)num109 < num108; num109++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy((float)num109 * ((float)Math.PI * 2f / num108)) * new Vector2(1f, 4f);
                spinningpoint5 = spinningpoint5.RotatedBy(v5.ToRotation());
                int num110 = Dust.NewDust(position, 0, 0, DustID.GemTopaz);
                Main.dust[num110].scale = 1.5f;
                Main.dust[num110].noGravity = true;
                Main.dust[num110].position = position + spinningpoint5;
                Main.dust[num110].velocity = v5 * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 1f;
            }
            //speedX = v5.X;
            //speedY = v5.Y;
            //Main.NewText(modplayer.comboCount);
            if (modplayer.comboCount >= 3)
            {
                modplayer.comboCount = 0;
                Projectile.NewProjectile(source, Main.MouseWorld, default, ModContent.ProjectileType<Projectiles.BlackHole>(), Item.damage / 10, 0f, player.whoAmI);
                for (int i = 0; i < 3; i++)
                {
                    Vector2 randPos = Main.rand.NextVector2CircularEdge(20f, 20f);
                    Vector2 vel = (-randPos).SafeNormalize(Vector2.UnitY) * Item.shootSpeed / 2;
                    int proj = Projectile.NewProjectile(source, Main.MouseWorld + randPos, vel, ModContent.ProjectileType<Projectiles.CommandmentsProjectile>(), damage, knockback, player.whoAmI);
                    Main.projectile[proj].timeLeft = 10;
                    //Main.projectile[proj].penetrate = -1;
                    Main.projectile[proj].tileCollide = false;
                    //Main.NewText(Main.projectile[proj].position);
                }
            }
            return false;
            //return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (modPlayer.voidTeleportCD > 0)
            {
                float percentCd = modPlayer.voidTeleportCD / teleportCD;
                Vector2 center = position + new Vector2(frame.Width / 2 * scale, frame.Height / 2 * scale);
                Vector2 position2 = center - TextureAssets.Cd.Size() * Main.inventoryScale / 2f;
                Color white = Color.White;
                //spriteBatch.Draw(Main.cdTexture, position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
                Texture2D CD = ModContent.Request<Texture2D>("OtakuTech/Assets/Textures/Other/Cooldown-2").Value;
                spriteBatch.Draw(CD, position2, null, new Color(percentCd, percentCd, percentCd, percentCd), 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
            }
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        /*
		 * Feel free to uncomment any of the examples below to see what they do
		 */

        // What if I wanted this gun to have a 38% chance not to consume ammo?
        /*public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .38f;
		}*/

        // What if I wanted it to work like Uzi, replacing regular bullets with High Velocity Bullets?
        // Uzi/Molten Fury style: Replace normal Bullets with High Velocity
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.Bullet) // or ProjectileID.WoodenArrowFriendly
			{
				type = ProjectileID.BulletHighVelocity; // or ProjectileID.FireArrow;
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}*/

        // What if I wanted it to shoot like a shotgun?
        // Shotgun style: Multiple Projectiles, Random spread 
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
				// If you want to randomize the speed to stagger the projectiles
				// float scale = 1f - (Main.rand.NextFloat() * .3f);
				// perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; // return false because we don't want tmodloader to shoot projectile
		}*/

        // What if I wanted an inaccurate gun? (Chain Gun)
        // Inaccurate Gun style: Single Projectile, Random spread 
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}*/

        // What if I wanted multiple projectiles in a even spread? (Vampire Knives) 
        // Even Arc style: Multiple Projectile, Even Spread 
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(45);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}*/

        // Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
        /*public override Vector2? HoldoutOffset()
		{
			return new Vector2(10, 0);
		}*/

        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}*/

        // How can I get a "Clockwork Assault Rifle" effect?
        // 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
        /*	The following changes to SetDefaults()
		 	item.useAnimation = 12;
			item.useTime = 4;
			item.reuseDelay = 14;
		public override bool ConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (useAnimation - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < item.useAnimation - 2);
		}*/

        // How can I shoot 2 different projectiles at the same time?
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.GrenadeI, damage, knockBack, player.whoAmI);
			// By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
			return true;
		}*/

        // How can I choose between several projectiles randomly?
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ProjectileType<Projectiles.ExampleBullet>() });
			return true;
		}*/
    }
}
