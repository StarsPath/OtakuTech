using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OtakuTech
{
	public class OtakuTech : Mod
	{
        public static Effect TimeFracture;
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                //Ref<Effect> filterRef = new Ref<Effect>(Assets.Request<Effect>("Assets/Shader/TimeFracture", AssetRequestMode.ImmediateLoad).Value);
                Filters.Scene["TimeFracture"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.8f, 0f, 0.8f).UseOpacity(0.50f), EffectPriority.High);
                Filters.Scene.Load();
            }
            //base.Load();
        }
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