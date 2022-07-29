using OtakuTech.Common.Eruption;
using Terraria;
using Terraria.ModLoader;

namespace OtakuTech.Content.Effect
{
    public class HonkaiEruptionScene : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Befall");
        public override SceneEffectPriority Priority => SceneEffectPriority.Event; // We have set the SceneEffectPriority to be BiomeLow for purpose of example, however default behavour is BiomeLow.

        public override bool IsSceneEffectActive(Player player)
        {
            return HonkaiWorld.honkaiInvasionActive;
            //return base.IsSceneEffectActive(player);
        }

    }
}
