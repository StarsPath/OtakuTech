using OtakuTech.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Weapons.FiveStars
{
	public class KeyOfCastigation : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/AF4BFF:This lightning-imbued blade was born along with ]" +
				"\n[c/AF4BFF:the awakening of the Herrscher of Thunder, ]" +
				"\n[c/AF4BFF:and it obeys only the lightning queen's commands]");
		}

		public override void SetDefaults() {
			item.damage = 192;
			item.crit = 5;
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

        public override void MeleeEffects(Player player, Rectangle hitbox) {
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PurpleTorch, 0f, 0f, 100, default(Color), 2f);
			Main.dust[dust].noGravity = true;
		}
	}
}