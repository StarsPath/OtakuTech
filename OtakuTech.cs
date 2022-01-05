using OtakuTech.NPCs.HonkaiBeasts;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OtakuTech
{
    public class OtakuTech : Mod
    {
        public override void PostUpdateEverything()
        {
            /*if(Main.invasionType != 0)
            {
                Main.NewText(Main.invasionX);
            }*/
            //base.PostUpdateEverything();
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (HonkaiWorld.honkaiInvasionActive && Main.invasionX == Main.spawnTileX)
            {
                music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/Befall");
                priority = MusicPriority.Event;
            }
            //base.UpdateMusic(ref music, ref priority);
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