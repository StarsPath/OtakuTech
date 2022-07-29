using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Players;
using OtakuTech.Content.Projectiles;
using OtakuTech.Content.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace OtakuTech.Content.Items.Weapons.FiveStars
{
	public class KeyOfOblivion : ModItem
	{
		public override void SetStaticDefaults() {
			//Tooltip.SetDefault("[c/AF4BFF:\"Hey, old timer, why do you use gauntlets?\"]" +
			//	"\n[c/AF4BFF:\"Be it sword, spear, or chain blade, flashy weapons can]" +
			//	"\n[c/AF4BFF:never bring out the true strength of a martial art master.\"]" +
			//	"\n[c/AF4BFF:\"Hmm, you should say that to Kevin.\"]");
			Tooltip.SetDefault("");
		}

		private int mode = 0;

		public override void SetDefaults() {
			Item.damage = 185;
			Item.crit = 12;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 24;
			Item.useTime = 24;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Purple;
			Item.value = Item.sellPrice(silver: 10);

			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;

			Item.UseSound = SoundID.Item1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SentienceChain>());
			recipe.AddIngredient(ModContent.ItemType<SentienceSpear>());
			recipe.AddIngredient(ModContent.ItemType<SentienceSword>());
			recipe.AddTile(ModContent.TileType<ProgramingStation>());
			recipe.Register();
		}

		public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			//Main.NewText(modPlayer.comboCount);
			if(player.altFunctionUse == 2)
            {
				mode++;
				mode %= 3;
            }
			switch (mode)
			{
				case 0:
					Item.CloneDefaults(ModContent.ItemType<SentienceSword>());
					Item.damage = 185;
					Item.crit = 12;
					Item.shoot = ModContent.ProjectileType<SentienceSwordSwing>();
					Item.noMelee = true;
                    Item.noUseGraphic = true;
                    break;
				case 1:
                    Item.CloneDefaults(ModContent.ItemType<SentienceSpear>());
					Item.damage = 185;
					Item.crit = 12;
					Item.autoReuse = true;
					if (modPlayer.comboCount >= 2)
					{
						Item.shoot = ModContent.ProjectileType<SentienceSpearCombo>();
						Item.useStyle = ItemUseStyleID.Swing;
						Item.noMelee = true;
						Item.noUseGraphic = true;
					}
					else
					{
						Item.shoot = ModContent.ProjectileType<SentienceSpearProjectile>();
						Item.useStyle = ItemUseStyleID.Shoot;
						Item.noMelee = true;
						Item.noUseGraphic = true;
					}

					if (player.ownedProjectileCounts[ModContent.ProjectileType<SentienceSpearProjectile>()] < 1)
					{
						return true;
					}
					return false;
				case 2:
					Item.CloneDefaults(ModContent.ItemType<SentienceChain>());
					Item.damage = 185;
					Item.crit = 12;
					Item.autoReuse = true;
					Item.shoot = ModContent.ProjectileType<SentienceWhip>();
					break;
			}
            return base.CanUseItem(player);
            //return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //Main.NewText("SHOOT" + modPlayer.comboCount);
            switch (mode)
            {
                case 0:
                    if (Main.rand.NextFloat() >= .66f)
                        Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(player, Item, AmmoID.None), position, velocity, ModContent.ProjectileType<SentienceSwordProjectile>(), damage, knockback, player.whoAmI);
                    break;

                case 1:
                    //Main.NewText(modPlayer.comboCount + "Adding Combo");
                    //if (modPlayer.comboCount == 1)
                    //{
                    //    modPlayer.addCombo();
                    //    modPlayer.resetCombo(3);
                    //    return false;
                    //}
                    modPlayer.addCombo();
                    modPlayer.resetCombo(3);
                    break;

                case 2:
                    break;

            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);

        }

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			//base.ModifyTooltips(tooltips);
			//TooltipLine line = new TooltipLine(Mod, "description", "[c/AF4BFF:\"Hey, old timer, why do you use gauntlets?\"]" +
			//	"\n[c/AF4BFF:\"Be it sword, spear, or chain blade, flashy weapons can]" +
			//	"\n[c/AF4BFF:never bring out the true strength of a martial art master.\"]" +
			//	"\n[c/AF4BFF:\"Hmm, you should say that to Kevin.\"]");
			TooltipLine line = new TooltipLine(Mod, "description1", "\"Hey, old timer, why do you use gauntlets?\"")
			{
				OverrideColor = new Color(175, 75, 255)
            };
			tooltips.Add(line);
			line = new TooltipLine(Mod, "description1", "\"Be it sword, spear, or chain blade, flashy weapons can")
			{
				OverrideColor = new Color(175, 75, 255)
			};
			tooltips.Add(line);
			line = new TooltipLine(Mod, "description1", "never bring out the true strength of a martial art master.\"")
			{
				OverrideColor = new Color(175, 75, 255)
			};
			tooltips.Add(line);
			line = new TooltipLine(Mod, "description1", "\"Hmm, you should say that to Kevin.\"")
			{
				OverrideColor = new Color(175, 75, 255)
			};
			tooltips.Add(line);
		}
    }
}
