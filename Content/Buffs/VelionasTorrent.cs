using Terraria;
using Terraria.ModLoader;
using OtakuTech.Common.Players;

namespace OtakuTech.Content.Buffs
{
	public class VelionasTorrent : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Veliona's Torrent");
			Description.SetDefault("Increase Melee Damage by 35%");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Melee) += 0.35f;
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			moddedPlayer.velionasTorrent = true;
		}

		public override bool ReApply(Player player, int time, int buffIndex)
		{
			return false;
		}
	}
}