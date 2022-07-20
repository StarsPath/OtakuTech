using Microsoft.Xna.Framework;
using OtakuTech.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
	public class SentienceSword : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("");
		}

		public override void SetDefaults() {
			Item.damage = 60;
			Item.scale = 1f;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<SentienceSwordProjectile>();
			Item.shootSpeed = 20f;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

        public override void MeleeEffects(Player player, Rectangle hitbox) {
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GemRuby, 0f, 0f, 100, default(Color), 2f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].scale = 0.5f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if (Main.rand.NextFloat() >= .33f)
				return false;
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}