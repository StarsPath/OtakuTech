using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Weapons.FiveStars
{
	public class KeyOfCastigation : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/AF4BFF:This lightning-imbued blade was born along with ]" +
				"\n[c/AF4BFF:the awakening of the Herrscher of Thunder, ]" +
				"\n[c/AF4BFF:and it obeys only the lightning queen's commands]");
		}

		public override void SetDefaults() {
			Item.damage = 192;
			Item.crit = 5;
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

        public override void MeleeEffects(Player player, Rectangle hitbox) {
			Main.NewText(hitbox);
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PurpleTorch, 0f, 0f, 100, default(Color), 2f);
			Main.dust[dust].noGravity = true;
		}
	}
}