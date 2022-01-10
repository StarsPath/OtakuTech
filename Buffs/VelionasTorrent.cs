using Terraria;
using Terraria.ModLoader;
using OtakuTech.Projectiles.Minions;

namespace OtakuTech.Buffs
{
	public class VelionasTorrent : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Veliona's Torrent");
			Description.SetDefault("Increase Melee Damage by 35%");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.meleeDamageMult += 0.35f;
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			moddedPlayer.velionasTorrent = true;
		}

		public override bool ReApply(Player player, int time, int buffIndex)
		{
			return false;
		}
	}
}