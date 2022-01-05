using OtakuTech.Mounts;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Accessories
{
	public class CarKey : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Bike Key");
			Tooltip.SetDefault("Remodelled Bunny 19C goes DejaVu~");
		}

		public override void SetDefaults() {
			item.width = 20;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = 30000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item79;
			item.noMelee = true;
			item.mountType = ModContent.MountType<HORBike>();
		}
	}
}