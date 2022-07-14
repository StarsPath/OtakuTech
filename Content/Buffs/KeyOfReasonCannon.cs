using OtakuTech.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.Content.Buffs
{
	public class KeyOfReasonCannon : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("KOR Cannon");
			Description.SetDefault("The KOR Cannon will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.KeyOfReasonCannon>()] > 0) {
				modPlayer.reasonableCannon = true;
			}
			if (!modPlayer.reasonableCannon) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}