using Terraria;
using Terraria.ModLoader;
using OtakuTech.Projectiles.Minions;

namespace OtakuTech.Buffs
{
	public class ExcelsisKing : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Excelsis King");
			Description.SetDefault("King will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ExcelsisKingMinion>()] > 0) {
				modPlayer.excelsisKing = true;
			}
			if (!modPlayer.excelsisKing) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}