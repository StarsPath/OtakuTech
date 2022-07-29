using Microsoft.Xna.Framework;
using OtakuTech.Common.Players;
using System;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.Content.Buffs
{
	public class SignetsOfSetsuna : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Signets of Setsuna");
			Description.SetDefault("Gains bonuses after triggering time fracture");
            Main.buffNoTimeDisplay[Type] = false;
			//Main.debuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.4f;
            player.eocDash = player.buffTime[buffIndex];
            base.Update(player, ref buffIndex);
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return false;
        }
    }
}