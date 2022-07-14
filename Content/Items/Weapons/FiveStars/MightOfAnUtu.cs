using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;
using Terraria.GameContent;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
	public class MightOfAnUtu : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/AF4BFF:A primed form of the Key of Destruction formed ]" +
				"\n[c/AF4BFF:in the hand of its original wielder. The Might of An-Utu is ]" +
				"\n[c/AF4BFF:a literal incarnation of the god of destruction, ]" +
				"\n[c/AF4BFF:and emits a radiance rivaling that of a supernova.]");
		}

		private float libationCD = 1500;

		public override void SetDefaults() {
			Item.damage = 189;
			Item.crit = 8;
			Item.scale = 1.5f;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			//Item.shoot = ProjectileID.None;
			Item.autoReuse = true;
			Item.useTurn = true;
            //Item.noUseGraphic = false;
            Item.shootSpeed = 1f;
		}

		public override bool AltFunctionUse(Player player) {
			return true;
		}

        public override bool CanUseItem(Player player) {
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.altFunctionUse == 2)
			{
				if (moddedPlayer.libationCD > 0)
					return false;
				Item.shoot = ModContent.ProjectileType<Projectiles.RavagingFlame>();
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.noUseGraphic = true;
				Item.noMelee = true;
				SoundEngine.PlaySound(SoundID.Item100);

				moddedPlayer.libationCD = libationCD;

				player.AddBuff(ModContent.BuffType<Buffs.LibationToFire>(), 300);

			}
			else
            {
				//Item.shoot = ProjectileID.None;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.noUseGraphic = false;
				Item.noMelee = false;
            }
			return true;
			//return base.CanUseItem(player);
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if (player.altFunctionUse == 2)
				return base.Shoot(player, source, position, velocity, type, damage, knockback);
			return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) {
			target.AddBuff(BuffID.OnFire, 600);
			base.OnHitNPC(player, target, damage, knockBack, crit);
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
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
				Texture2D CD = ModContent.Request<Texture2D>("OtakuTech/Assets/Textures/Other/Cooldown-2").Value;
				//spriteBatch.Draw(Main.cdTexture, position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
				spriteBatch.Draw(CD, position2, null, new Color(percentCd, percentCd, percentCd, percentCd), 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
			}
			base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
	}
}