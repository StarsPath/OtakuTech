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
    public class OathOfJudah : ModItem
    {
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("[c/AF4BFF:A super weapon from the Previous Era ]" +
				"\n[c/AF4BFF:armed with the ultimate \"Seal\". Once deployed, all nearby enemies]" +
				"\n[c/AF4BFF:will be imprisoned and rendered completely immobile, ]" +
				"\n[c/AF4BFF:effectively nullifying the Honkai energy within them.]");
		}
        public override void SetDefaults()
        {
			Item.damage = 176;
			Item.crit = 20;
			Item.scale = 1f;
			//item.summon = true;
			Item.DamageType = DamageClass.Ranged;
			Item.mana = 4;
			Item.width = 24;
			Item.height = 48;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.autoReuse = true;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<LightningSpear>();
			Item.shootSpeed = 20f;
			Item.scale = 0.8f;
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
                Item.noUseGraphic = true;
                Item.noMelee = true;
                Item.autoReuse = false;

                Item.UseSound = SoundID.Item44;
                Item.shoot = ModContent.ProjectileType<JudahCross>();
            }
            else
            {
				usage = 1;
                Item.DamageType = DamageClass.Ranged;
                Item.mana = 4;
                Item.useTime = 24;
                Item.useAnimation = 24;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.noMelee = true;
                Item.autoReuse = true;

                Item.UseSound = SoundID.Item105;
                Item.shoot = ModContent.ProjectileType<LightningSpear>();
                Item.shootSpeed = 25f;
            }
			return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (usage == 1)
            {
				Vector2 offset = new Vector2(0, -8);
				position += offset;
			}
			else if (usage == 2)
            {
				position = Main.MouseWorld;
				if (!Collision.SolidCollision(position, 24, 61))
                {
					modPlayer.crossDeploy = true;
					Projectile.NewProjectile(source, position, new Vector2(0, 5), Item.shoot, damage, knockback, player.whoAmI);
					//return base.Shoot(player, source, position, new Vector2(speedX, speedY), type, damage, knockback);
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

				Vector2 center = position + new Vector2(frame.Width /2 * scale, frame.Height /2 * scale);
				Vector2 position2 = center - TextureAssets.Cd.Size() * Main.inventoryScale / 2f;
				Color white = Color.White;
				Texture2D CD = ModContent.Request<Texture2D>("OtakuTech/Assets/Textures/Other/Cooldown-2").Value;
				spriteBatch.Draw(CD, position2, null, white, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
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
