using OtakuTech.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.Content.Buffs
{
	public class LibationToFire : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Libation To Fire");
			Description.SetDefault("Lose 8% of current HP per second");
			Main.debuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			int damage = (int)(player.statLife * 0.08f * 2);
			player.lifeRegen = -damage;
			ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
			moddedPlayer.libationToFire = true;
		}

        public override bool ReApply(Player player, int time, int buffIndex)
        {
			player.buffTime[buffIndex] += time;
			return true;
        }
    }
}