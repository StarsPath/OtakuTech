using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

using OtakuTech.NPCs.HonkaiBeasts.Eruption;

namespace OtakuTech.NPCs.HonkaiBeasts
{
    public class HonkaiWorld : ModWorld
    {
        public static string invasionName = "honkaiEruption";
        public static bool honkaiInvasionActive = false;
        public static bool honkaiInvasionDown = false;

        public override void Initialize()
        {
            Main.invasionSize = 0;
            honkaiInvasionActive = false;
            honkaiInvasionDown = false;
            //base.Initialize();
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (honkaiInvasionDown)
                downed.Add(invasionName);

            return new TagCompound
            {
                {"downed", downed }
            };
            //honkaiInvasionDown = downed.Contains(invasionName);
            //return base.Save();
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            honkaiInvasionDown = downed.Contains(invasionName);
            //base.Load(tag);
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = honkaiInvasionDown;
            writer.Write(flags);
            //base.NetSend(writer);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            honkaiInvasionDown = flags[0];
            //base.NetReceive(reader);
        }

        public override void PostUpdate()
        {
            if (honkaiInvasionActive)
            {
                if(Main.invasionX == (double)Main.spawnTileX)
                {
                    //honk
                    Eruption.Eruption.CheckCustomInvasionProgress();
                }
                Eruption.Eruption.UpdateInvasion();
            }
            //base.PostUpdate();
        }
    }
}
