using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using OtakuTech.Content.Projectiles;

namespace OtakuTech.Content.Items.Weapons.PRI
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
			Item.damage = 283;
			Item.crit = 24;
			Item.scale = 1.2f;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.shoot = ProjectileID.None;
			//item.shoot = ProjectileID.Bee;
			//item.shootSpeed = 5f;
		}

        //public override void AddRecipes() {
        //	Recipe recipe = CreateRecipe();
        //	recipe.AddIngredient(ModContent.ItemType<IceEpiphyllum>());
        //	recipe.AddIngredient(ModContent.ItemType<EinsteinsTorus>(), 10);
        //	recipe.AddIngredient(ModContent.ItemType<SCMetalH2>(), 10);
        //	recipe.AddIngredient(ModContent.ItemType<Nanoceramic>(), 10);
        //	recipe.AddTile(ModContent.TileType<ProgramingStation>());
        //	recipe.Register();
        //}

        public override bool AltFunctionUse(Player player) {
			return true;
		}

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
				Item.useStyle = ItemUseStyleID.Guitar;
				Item.noUseGraphic = true;
                Item.noMelee = true;
				Item.useTime = 16;
                Item.useAnimation = 16;
                Item.shoot = ModContent.ProjectileType<InstantDraw>();
            }
            else
            {
				Item.useStyle = ItemUseStyleID.Swing;
				Item.noUseGraphic = false;
				Item.noMelee = false;
                Item.useTime = 16;
                Item.useAnimation = 16;
                //Item.shoot = ProjectileID.None;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(player.altFunctionUse == 2){
				NPC[] targetNPC = FindNearest(player.position, 3);
				//mod.Logger.Info(targetNPC);
				if (targetNPC[0] == null)
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
					Projectile.NewProjectile(source, position, default, Item.shoot, damage, knockback, player.whoAmI);
				}
				else
				{
					for (int i = 0; i < targetNPC.Length; i++)
					{
						if (targetNPC[i] != null)
							Projectile.NewProjectile(source, targetNPC[i].Center, default, Item.shoot, damage, knockback, player.whoAmI);
					}
					//position = targetNPC[0].Center;
				}
			}
			return false;
            //return base.Shoot(player, source, position, velocity, type, damage, knockback);
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