using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
	public class IceEpiphyllum : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/AF4BFF:Nagamitsu, the legendary bladesmith who lived for a ]" +
				"\n[c/AF4BFF:thousand years and the director of St. 1504 Labs, managed]" +
				"\n[c/AF4BFF:to partially characterize the structure of Sakura Blossom to]" +
				"\n[c/AF4BFF:create this imitation blade using Soulium.]");
		}

		public override void SetDefaults() {
			Item.damage = 169;
			Item.crit = 30;
			Item.scale = 1.25f;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) {
			target.AddBuff(BuffID.Frostburn, 60);
		}

        public override void MeleeEffects(Player player, Rectangle hitbox) {
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Frost, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 100, default(Color), 1f);
			Main.dust[dust].noGravity = true;
		}
	}
}