using Microsoft.Xna.Framework;
using OtakuTech.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.Content.Buffs
{
	public class TimeFracture : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Time Fracture");
			Description.SetDefault("Time Slows");
			Main.debuff[Type] = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
		{
			//npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -1, 1);
			//npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -1, 1);
			npc.velocity *= 0.2f;
			//npc.color = Color.Purple;
			//base.Update(npc, ref buffIndex);
		}

  //      public override void Update(Player player, ref int buffIndex) {
		//	player.velocity.X = MathHelper.Clamp(player.velocity.X, -1, 1);
		//	player.velocity.Y = MathHelper.Clamp(player.velocity.Y, -1, 1);
		//	//int damage = (int)(player.statLife * 0.08f * 2);
		//	//player.lifeRegen = -damage;
		//	//ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
		//	//moddedPlayer.libationToFire = true;
		//}

        public override bool ReApply(Player player, int time, int buffIndex)
        {
			//player.buffTime[buffIndex] += time;
			return false;
        }
    }
}