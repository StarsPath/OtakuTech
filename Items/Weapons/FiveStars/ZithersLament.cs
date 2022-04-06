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
    public class ZithersLament : ModItem
    {
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("[c/AF4BFF:The zither sings for the dearly missed, and fond memories that last a]" +
				"\n[c/AF4BFF:thousand years. Some say it's a wonderful instrument that moves]" +
				"\n[c/AF4BFF:listeners to tears, while others say it's a deadly weapon with hidden]" +
				"\n[c/AF4BFF:blades that shred enemies to pieces.]");
		}
        public override void SetDefaults()
        {
			item.damage = 188;
			item.crit = 9;
			item.scale = 1f;
			//item.summon = true;
			item.ranged = true;
			//item.mana = 4;
			item.width = 24;
			item.height = 48;
			item.useTime = 30;
			item.useAnimation = 30;
			item.reuseDelay = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.knockBack = 3;
			item.value = Item.buyPrice(0, 30, 0, 0);
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<RoseFinch>();
			item.shootSpeed = 20f;
			item.scale = 0.8f;
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
                item.ranged = false;
                item.summon = true;
                //item.mana = 10;
                item.useTime = 30;
                item.useAnimation = 30;
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.noUseGraphic = true;
                item.noMelee = true;
                item.autoReuse = false;

                item.UseSound = SoundID.Item44;
                item.shoot = ModContent.ProjectileType<ZithersCross>();
            }
            else
            {
				usage = 1;
                item.ranged = true;
                item.summon = false;
                //item.mana = 4;
                item.useTime = 24;
                item.useAnimation = 24;
				item.reuseDelay = 16;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.noUseGraphic = true;
                item.noMelee = true;
                item.autoReuse = true;

                item.UseSound = SoundID.Item105;
                item.shoot = ModContent.ProjectileType<RoseFinch>();
                item.shootSpeed = 25f;
            }
			return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (usage == 1)
            {
				modPlayer.addCombo();
				Vector2 offset = new Vector2(0, -8);
				position += offset;

				if (modPlayer.comboCount >= 4)
				{
					modPlayer.comboCount = 0;
					type = ModContent.ProjectileType<RoseFinch2>();
				}
			}
			else if (usage == 2)
            {
				position = Main.MouseWorld;
				speedX = 0;
				speedY = 5;
				if (!Collision.SolidCollision(position, 24, 61))
                {
					modPlayer.crossDeploy = true;
					player.AddBuff(ModContent.BuffType<Buffs.EightFormations>(), 15 * 60);
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

				Vector2 center = position + new Vector2(frame.Width /2 * scale, frame.Height /2 * scale);
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
