

using OtakuTech.Buffs;
using OtakuTech.Items.Weapons.FiveStars;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech
{
    class AllNPC : GlobalNPC
    {

        public override void NPCLoot(NPC npc)
        {
            int playerIndex = npc.lastInteraction;
            if (!Main.player[playerIndex].active || Main.player[playerIndex].dead)
            {
                base.NPCLoot(npc);
                return;
                //playerIndex = npc.FindClosestPlayer(); // Since lastInteraction might be an invalid player fall back to closest player.
            }
            Player player = Main.player[playerIndex];
            ModdedPlayer moddedPlayer = player.GetModPlayer<ModdedPlayer>();
            if(player.HeldItem.modItem is UndinesTale && moddedPlayer.velionasTorrent)
            {
                player.AddBuff(ModContent.BuffType<VelionasTorrent>(), 4 * 60);
                moddedPlayer.undineCD = 0;
            }

            base.NPCLoot(npc);
        }
    }
}
