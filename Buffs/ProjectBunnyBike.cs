using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.Buffs
{
	public class ProjectBunnyBike : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Project Bunny");
			Description.SetDefault("Project Bunny Bike\n No Underage Driver Allowed!");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.mount.SetMount(ModContent.MountType<Mounts.HORBike>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
