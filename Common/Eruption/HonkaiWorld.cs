using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace OtakuTech.Common.Eruption
{
    public class HonkaiWorld : ModSystem
    {
        public static string invasionName = "honkaiEruption";
        public static bool honkaiInvasionActive = false;
        public static bool honkaiInvasionDown = false;
        public static bool benaresActive = false;
        public static bool benaresDown = false;

        public override void OnWorldLoad()
        {
            Main.invasionSize = 0;
            honkaiInvasionActive = false;
            honkaiInvasionDown = false;
            benaresActive = false;
            benaresDown = false;
            //base.Initialize();
        }

        public override void OnWorldUnload()
        {
            Main.invasionSize = 0;
            honkaiInvasionActive = false;
            honkaiInvasionDown = false;
            benaresActive = false;
            benaresDown = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            var downed = new List<string>();
            if (honkaiInvasionDown)
                downed.Add(invasionName);
            if (benaresDown)
                downed.Add("benares");

            tag["downed"] = downed;

            //return new TagCompound
            //{
            //    {"downed", downed }
            //};
            //honkaiInvasionDown = downed.Contains(invasionName);
            //return base.Save();
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            honkaiInvasionDown = downed.Contains(invasionName);
            benaresDown = downed.Contains("benares");
            //base.Load(tag);
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = honkaiInvasionDown;
            flags[1] = benaresDown;
            writer.Write(flags);
            //base.NetSend(writer);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            honkaiInvasionDown = flags[0];
            benaresDown = flags[1];
            //base.NetReceive(reader);
        }

        public override void PostUpdateWorld()
        {
            if (honkaiInvasionActive)
            {
                if (Main.invasionX == Main.spawnTileX)
                {
                    //honk
                    Eruption.CheckCustomInvasionProgress();
                }
                Eruption.UpdateInvasion();
            }
            if (honkaiInvasionDown)
            {

            }
            //base.PostUpdate();
        }
    }
}
