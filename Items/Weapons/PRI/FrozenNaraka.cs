using OtakuTech.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OtakuTech.Items.Weapons.FiveStars;
using OtakuTech.Items.Materials;

namespace OtakuTech.Items.Weapons.PRI
{
	public class FrozenNaraka : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/ffff0a:A frost-encased Soulium katana of ice. ]" +
				"\n[c/ffff0a:Historical records indicate that it used to belong to ]" +
				"\n[c/ffff0a:a warrior under the Previous Era organization ]" +
				"\n[c/ffff0a:known as the Moth Who Chases the Flames.]");

		}

		private float shootDist = 200f;

		public override void SetDefaults() {
			item.damage = 283;
			item.crit = 24;
			item.scale = 1.25f;
			item.melee = true;
			item.width = 24;
			item.height = 24;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shoot = ProjectileID.None;
			//item.shoot = ProjectileID.Bee;
			//item.shootSpeed = 5f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<IceEpiphyllum>());
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
			if (player.altFunctionUse == 2) {
				item.noUseGraphic = true;
				item.noMelee = true;
				item.useTime = 16;
				item.useAnimation = 16;
				item.shoot = ModContent.ProjectileType<Projectiles.InstantDraw>();
			}
			else {
				item.noUseGraphic = false;
				item.noMelee = false;
				//item.useStyle = ItemUseStyleID.SwingThrow;
				item.useTime = 16;
				item.useAnimation = 16;
				item.shoot = ProjectileID.None;
			}
			return base.CanUseItem(player);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) {
			target.AddBuff(BuffID.Frostburn, 300);
		}

        public override void MeleeEffects(Player player, Rectangle hitbox) {
/*			if (Main.rand.NextBool(3)) {
				if (player.altFunctionUse == 2) {
					int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 169, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity.X += player.direction * 2f;
					Main.dust[dust].velocity.Y += 0.2f;
				}
				else {
					int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 100, default(Color), 2.5f);
					Main.dust[dust].noGravity = true;
				}
			}*/
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			NPC[] targetNPC = FindNearest(player.position, 3);
			//mod.Logger.Info(targetNPC);
			if(targetNPC[0] == null)
            {
                if (Vector2.Distance(player.position, Main.MouseWorld) < shootDist)
                {
					position = Main.MouseWorld;
                }
                else
                {
					Vector2 dir = player.position - Main.MouseWorld;
					dir.Normalize();
					position = player.position - (dir * shootDist);
				}
            }
            else
            {
				for (int i = 1; i < targetNPC.Length; i++)
                {
					if (targetNPC[i] != null)
						Projectile.NewProjectile(targetNPC[i].Center, default, item.shoot, damage, knockBack, Main.myPlayer);
                }
				position = targetNPC[0].Center;
			}

            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

		public NPC[] FindNearest(Vector2 playerPos, int count = 1)
        {
			NPC[] target = new NPC[count];
			float[] dist = new float[count];
			for(int i = 0; i < count; i++)
            {
				dist[i] = 1000f;
            }
			int index = 0;

			for(int i = 0; i < Main.npc.Length; i++)
            {
				float newDist = Vector2.Distance(playerPos, Main.npc[i].position);
				if(newDist < dist[index % count] && newDist < shootDist && Main.npc[i].damage > 0 && Main.npc[i].active && !Main.npc[i].friendly)
                {
					dist[index % count] = newDist;
					target[index % count] = Main.npc[i];
					index++;
                }
            }

			return target;
        }
    }
}