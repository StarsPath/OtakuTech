using Terraria;
using Terraria.ModLoader;
using OtakuTech.Projectiles.Minions;
using OtakuTech.Common.Players;

namespace OtakuTech.Content.Buffs
{
	public class EightFormations : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Eight Formations");
			Description.SetDefault("Increase all damage multiplier by 25%");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Generic) += 0.25f;
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			moddedPlayer.eightFormations = true;
		}

		public override bool ReApply(Player player, int time, int buffIndex)
		{
			return false;
		}
	}
}