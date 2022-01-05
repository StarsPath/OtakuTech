using OtakuTech.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Weapons.FiveStars
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
			item.damage = 169;
			item.crit = 30;
			item.scale = 1.25f;
			item.melee = true;
			item.width = 24;
			item.height = 24;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
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