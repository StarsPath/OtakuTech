using OtakuTech.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Items.Weapons.FiveStars;
using OtakuTech.Items.Materials;

namespace OtakuTech.Items.Weapons.PRI
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
			item.damage = 291;
			item.crit = 13;
			item.scale = 1.5f;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 1f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<MightOfAnUtu>());
			recipe.AddIngredient(ModContent.ItemType<EinsteinsTorus>(), 10);
			recipe.AddIngredient(ModContent.ItemType<SCMetalH2>(), 10);
			recipe.AddIngredient(ModContent.ItemType<Nanoceramic>(), 10);
			recipe.AddTile(ModContent.TileType<ProgramingStation>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool CanUseItem(Player player) {
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.altFunctionUse == 2) {
				if (moddedPlayer.libationCD > 0)
					return false;
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.useTime = 20;
				item.useAnimation = 20;
				item.reuseDelay = 30;
				item.damage = 291;
				if(charge == 0)
                {
					item.shoot = ModContent.ProjectileType<Projectiles.RavagingFlame>();
					charge++;
					timer = timerMax;
				}
				else if(charge == 1 && timer > 0)
                {
					item.shoot = ModContent.ProjectileType<Projectiles.RavagingFlame2>();
					moddedPlayer.libationCD = libationCD;
					charge++;
				}
				item.noUseGraphic = true;

				//player.Teleport(mousePos, 4);
				//player.position = mousePos;
				//player.AddBuff(BuffID.OnFire, 300);
				player.AddBuff(ModContent.BuffType<Buffs.LibationToFire>(), 300);
				//player.AddBuff(BuffID.WeaponImbueFire, 900);
				//player.AddBuff(BuffID.Inferno, 900);

                Main.PlaySound(SoundID.Item100);
/*                int dust = Dust.NewDust(player.position, player.width, player.height, DustID.Fire);
                Main.dust[dust].noGravity = true;*/
			}
			else
			{
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.useTime = 30;
				item.useAnimation = 30;
				item.reuseDelay = 0;
				item.damage = 291;
				item.shoot = ProjectileID.None;
				item.noUseGraphic = false;

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
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if(item.shoot == ModContent.ProjectileType<Projectiles.RavagingFlame2>())
            {
				Vector2 dir = player.position - Main.MouseWorld;
				dir.Normalize();
				position = player.position - (dir * 200);
			}
			//speedY = 0;
			
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) {
			target.AddBuff(BuffID.OnFire, 600);
			base.OnHitNPC(player, target, damage, knockBack, crit);
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			int dust = Dust.NewDust(hitbox.Center.ToVector2() - new Vector2(hitbox.Width/2, hitbox.Height/2), hitbox.Width, hitbox.Height, DustID.Fire);
			Main.dust[dust].noGravity = true;
			//Main.dust[dust].fadeIn = 1f;
			Main.dust[dust].scale = 1.5f;
			base.MeleeEffects(player, hitbox);
        }

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[item.owner];
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (modPlayer.libationCD > 0)
			{
				float percentCd = modPlayer.libationCD / libationCD;
				Vector2 center = position + new Vector2(frame.Width / 2 * scale, frame.Height / 2 * scale);
				Vector2 position2 = center - Main.cdTexture.Size() * Main.inventoryScale / 2f;
				Color white = Color.White;
				//spriteBatch.Draw(Main.cdTexture, position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
				spriteBatch.Draw(ModLoader.GetMod("OtakuTech").GetTexture("Other/Cooldown-2"), position2, null, new Color(percentCd, percentCd, percentCd, percentCd), 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
			}
			base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
	}
}