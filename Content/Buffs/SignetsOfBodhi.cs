using Microsoft.Xna.Framework;
using OtakuTech.Common.Players;
using System;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.Content.Buffs
{
	public class SignetsOfBodhi : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Signets of Bodhi");
			Description.SetDefault("Gains bonuses when hit count reaches 150");
            Main.buffNoTimeDisplay[Type] = false;
			//Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) += 0.35f;
            player.GetDamage(DamageClass.Ranged) += 0.35f;
            player.endurance += 0.032f;

            //player.buffTime[buffIndex] = Math.Clamp(player.buffTime[buffIndex], 0, 40 * 60);
            //base.Update(player, ref buffIndex);
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            //player.buffTime[buffIndex] += time;
            return false;
        }
    }
}