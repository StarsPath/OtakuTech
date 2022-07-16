using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OtakuTech
{
	public class OtakuTech : Mod
	{
        public override void AddRecipeGroups()
        {
            RecipeGroup PlatinumGold = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Platinum Or Gold", new int[] {
                ItemID.PlatinumBar,
                ItemID.GoldBar
            });
            RecipeGroup LargeGems = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Large Gems", new int[] {
                ItemID.LargeAmber,
                ItemID.LargeAmethyst,
                ItemID.LargeDiamond,
                ItemID.LargeEmerald,
                ItemID.LargeRuby,
                ItemID.LargeSapphire,
                ItemID.LargeTopaz
            });
            RecipeGroup.RegisterGroup("OtakuTech:PlatinumGold", PlatinumGold);
            RecipeGroup.RegisterGroup("OtakuTech:LargeGems", LargeGems);
            //base.AddRecipeGroups();
        }
    }
}