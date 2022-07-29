using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ReLogic.Content;
using OtakuTech.Common.Players;
using OtakuTech.Content.Items.Weapons.FiveStars;
using OtakuTech.Content.Items.Materials;
using OtakuTech.Content.Tiles;

namespace OtakuTech.Content.Items.Weapons.PRI
{
	public class ShuhadakuOfUriel : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/ffff0a:Destined to burn out all the stars, this greatsword brings]" +
				"\n[c/ffff0a:nothing but destruction. As its flames devour the heavens,]" +
				"\n[c/ffff0a:every soul will be claimed. This is the sword that transcends the end.]");
		}

		private float libationCD = 1500;

		private int charge = 0;
		private float timerMax = 120f;
		private float timer = 0f;

		public override void SetDefaults() {
			Item.damage = 291;
			Item.crit = 13;
			Item.scale = 1.5f;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.shootSpeed = 1f;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MightOfAnUtu>());
            recipe.AddIngredient(ModContent.ItemType<EinsteinsTorus>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SCMetalH2>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Nanoceramic>(), 10);
            recipe.AddTile(ModContent.TileType<ProgramingStation>());
            recipe.Register();
        }

        public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool CanUseItem(Player player) {
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.altFunctionUse == 2) {
				if (moddedPlayer.libationCD > 0)
					return false;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.noUseGraphic = true;
				Item.useTime = 20;
				Item.useAnimation = 20;
				Item.reuseDelay = 30;
				Item.damage = 291;
				if(charge == 0)
                {
					Item.shoot = ModContent.ProjectileType<Projectiles.RavagingFlame>();
					charge++;
					timer = timerMax;
				}
				else if(charge == 1 && timer > 0)
                {
					Item.shoot = ModContent.ProjectileType<Projectiles.RavagingFlame2>();
					moddedPlayer.libationCD = libationCD;
					charge++;
				}
				

				player.AddBuff(ModContent.BuffType<Buffs.LibationToFire>(), 300);

                SoundEngine.PlaySound(SoundID.Item100);
/*                int dust = Dust.NewDust(player.position, player.width, player.height, DustID.Fire);
                Main.dust[dust].noGravity = true;*/
			}
			else
			{
				Item.useStyle = ItemUseStyleID.Swing;
				Item.noUseGraphic = false;
				Item.useTime = 30;
				Item.useAnimation = 30;
				Item.reuseDelay = 0;
				Item.damage = 291;
				//Item.shoot = ProjectileID.None;

				//Main.PlaySound(SoundID.Item100);
                
            }
			return base.CanUseItem(player);
		}
        public override void UpdateInventory(Player player)
        {
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			if (timer > 0)
				timer--;
            if(timer <= 0)
            {
				if(charge > 0 && moddedPlayer.libationCD <= 0)
                {
					moddedPlayer.libationCD = libationCD;
				}
				charge = 0;
            }
            base.UpdateInventory(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(player.altFunctionUse == 2){
				if (Item.shoot == ModContent.ProjectileType<Projectiles.RavagingFlame2>())
				{
					Vector2 dir = player.position - Main.MouseWorld;
					dir.Normalize();
					//position = player.position - (dir * 200);
					velocity = dir * -20f;
					Projectile.NewProjectile(source, position, velocity, Item.shoot, damage, knockback, player.whoAmI);
					return false;
				}
				//speedY = 0;

				return base.Shoot(player, source, position, velocity, type, damage, knockback);
			}
			return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) {
			target.AddBuff(BuffID.OnFire, 600);
			base.OnHitNPC(player, target, damage, knockBack, crit);
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			if (player.altFunctionUse == 2)
				return;
			int dust = Dust.NewDust(hitbox.Center.ToVector2() - new Vector2(hitbox.Width/2, hitbox.Height/2), hitbox.Width, hitbox.Height, DustID.Torch);
			Main.dust[dust].noGravity = true;
			//Main.dust[dust].fadeIn = 1f;
			Main.dust[dust].scale = 1.5f;
			base.MeleeEffects(player, hitbox);
        }

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (modPlayer.libationCD > 0)
			{
				float percentCd = modPlayer.libationCD / libationCD;
				Vector2 center = position + new Vector2(frame.Width / 2 * scale, frame.Height / 2 * scale);
				Vector2 position2 = center - TextureAssets.Cd.Size() * Main.inventoryScale / 2f;
				Color white = Color.White;
				//spriteBatch.Draw(Main.cdTexture, position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
				Texture2D CD = ModContent.Request<Texture2D>("OtakuTech/Assets/Textures/Other/Cooldown-2").Value;
				spriteBatch.Draw(CD, position2, null, new Color(percentCd, percentCd, percentCd, percentCd), 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
			}
			base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
	}
}