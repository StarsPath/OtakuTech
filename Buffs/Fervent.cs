using Terraria;
using Terraria.ModLoader;
using OtakuTech.Projectiles.Minions;

namespace OtakuTech.Buffs
{
	public class Fervent : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Fervent");
			Description.SetDefault("Increase Melee Crit by 15%");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.meleeCrit += 15;
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			moddedPlayer.fervent = true;
		}

		public override bool ReApply(Player player, int time, int buffIndex)
		{
			return false;
		}
	}
}