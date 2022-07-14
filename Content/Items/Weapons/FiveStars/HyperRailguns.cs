using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
    public class HyperRailguns : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/AF4BFF:ME Corp developed these hand cannons using Project-MEI]" +
                "\n[c/AF4BFF:test results from 9 years ago. These guns use an EM field to]" +
                "\n[c/AF4BFF:trap a large quantity of Honkai energy particles and to exponentially]" +
                "\n[c/AF4BFF:increase the power of the projectiles launched.]");
        }

        public override void SetDefaults()
        {
            Item.damage = 183; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.crit = 10;
            Item.DamageType = DamageClass.Ranged; // sets the damage type to ranged
            Item.width = 40; // hitbox width of the item
            Item.height = 24; // hitbox height of the item
            Item.useTime = 15; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 30; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.reuseDelay = 10;
            Item.useStyle = ItemUseStyleID.Shoot; // how you use the item (swinging, holding out, etc)
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.value = 10000; // how much the item sells for (measured in copper)
            Item.rare = ItemRarityID.Purple; // the color that the item's name will be in-game
            Item.UseSound = SoundID.Item41; // The sound that this item plays when used.
            Item.autoReuse = false; // if you can hold click to automatically use it again
            Item.shoot = ProjectileID.Bullet;
            //item.shoot = ModContent.ProjectileType<FeatherBlade>();
            //item.shoot = ModContent.ProjectileType<Projectiles.CommandmentsProjectile>(); //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 30f; // the speed of the projectile (measured in pixels per frame)
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
                                   //item.scale = 0.75f;
        }
        //public override void HoldItem(Player player)
        //{
        //    player.itemLocation.X -= 10 * player.direction;
        //    //base.HoldItem(player);
        //}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(3, 0);
            //return base.HoldoutOffset();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int spread = 8; //degrees
            float k = 15;
            float numberProjectiles = 5 + Main.rand.Next(2);
            float rotation = MathHelper.ToRadians(spread);
            position += Vector2.Normalize(velocity) * spread;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            for (int i = 0; i <= numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread)) * Item.shootSpeed; // Watch out for dividing by 0 if there is only 1 projectile.
                
                int p = Projectile.NewProjectile(source, position, perturbedSpeed, type, damage / 3, knockback, player.whoAmI);
                int temp = (int)(k * Math.Sin(i * Math.PI / numberProjectiles)) + 5;
                //Main.NewText(temp);
                Main.projectile[p].timeLeft = temp;
                //Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FeatherBlade>(), damage / 5, knockBack, player.whoAmI);
            }
            return false;
            //int spread = 20;
            //int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
            //for (int i = 0; i < numberProjectiles; i++)
            //{
            //    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(spread)); // 30 degree spread.
            //    // If you want to randomize the speed to stagger the projectiles
            //    float scale = 1f - (Main.rand.NextFloat() * .3f);
            //    perturbedSpeed = perturbedSpeed * scale;
            //    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            //}
            //return false;
        }

        //     public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        //     {
        //Vector2 pointPoisition = player.RotatedRelativePoint(player.MountedCenter);
        //float num2 = (float)Main.mouseX + Main.screenPosition.X - pointPoisition.X;
        //float num3 = (float)Main.mouseY + Main.screenPosition.Y - pointPoisition.Y;

        //float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
        //float value3 = 20f;
        //float value4 = 60f;
        //Vector2 vector50 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(value3, value4, Main.rand.NextFloat());
        //for (int num172 = 0; num172 < 50; num172++)
        //{
        //	vector50 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(value3, value4, Main.rand.NextFloat());
        //	if (Collision.CanHit(pointPoisition, 0, 0, vector50 + (vector50 - pointPoisition).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
        //	{
        //		break;
        //	}
        //	f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
        //}
        //Vector2 v5 = Main.MouseWorld - vector50;
        //Vector2 vector51 = new Vector2(num2, num3).SafeNormalize(Vector2.UnitY) * item.shootSpeed;
        //v5 = v5.SafeNormalize(vector51) * item.shootSpeed;
        ////v5 = Vector2.Lerp(v5, vector51, 0.25f);
        ////Projectile.NewProjectile(vector50, v5, ModContent.ProjectileType<Projectiles.CommandmentsProjectile2>(), damage, knockBack);

        //position = vector50;

        //float num108 = 16f;
        //         for (int num109 = 0; (float)num109 < num108; num109++)
        //         {
        //             Vector2 spinningpoint5 = Vector2.UnitX * 0f;
        //             spinningpoint5 += -Vector2.UnitY.RotatedBy((float)num109 * ((float)Math.PI * 2f / num108)) * new Vector2(1f, 4f);
        //             spinningpoint5 = spinningpoint5.RotatedBy(v5.ToRotation());
        //             int num110 = Dust.NewDust(position, 0, 0, DustID.TopazBolt);
        //             Main.dust[num110].scale = 1.5f;
        //             Main.dust[num110].noGravity = true;
        //             Main.dust[num110].position = position + spinningpoint5;
        //             Main.dust[num110].velocity = v5 * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 1f;
        //         }
        //speedX = v5.X;
        //speedY = v5.Y;
        //return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        //     }

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
