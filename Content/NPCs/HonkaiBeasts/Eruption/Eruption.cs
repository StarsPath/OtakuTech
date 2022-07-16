using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace OtakuTech.Content.NPCs.HonkaiBeasts.Eruption
{
    public class Eruption
    {
        public static int[] invaders =
        {
            ModContent.NPCType<NPCs.HonkaiBeasts.AlloyChariot>(),
            ModContent.NPCType<NPCs.HonkaiBeasts.Cassiel>(),
            ModContent.NPCType<NPCs.HonkaiBeasts.Paladin>()
        };

        public static void startInvasion()
        {
            if (Main.invasionType != 0 && Main.invasionSize == 0)
            {
                Main.invasionType = 0;
            }

            if (Main.invasionType == 0)
            {
                int numPlayer = 0;
                for(int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active)
                        numPlayer++;
                }
                Main.NewText("numPlayer:" + numPlayer);
                if(numPlayer > 0)
                {
                    Main.invasionType = -1;
                    HonkaiWorld.honkaiInvasionActive = true;
                    Main.invasionSize = 100 * numPlayer;
                    Main.invasionSizeStart = Main.invasionSize;
                    Main.invasionProgress = 0;
                    Main.invasionProgressIcon = 3;
                    Main.invasionProgressWave = 0;
                    Main.invasionProgressMax = Main.invasionSize;
                    Main.invasionWarn = 3600;

                    //Main.invasionX = (double)Main.maxTilesX;
                    Main.invasionX = Main.spawnTileX;

                    /*if (Main.rand.Next(2) == 0)
                    {
                        Main.invasionX = 0.0; //Starts invasion immediately rather than wait for it to spawn
                        return;
                    }
                    Main.invasionX = (double)Main.maxTilesX; //Set the initial starting location of the invasion to max tiles*/
                }
            }
        }

        public static void invasionWarning()
        {
            String text = "";
            if (Main.invasionX == (double)Main.spawnTileX)
            {
                text = "Custom invasion has reached the spawn position!";
            }
            if (Main.invasionSize <= 0)
            {
                text = "Custom invasion has been defeated.";
            }
            if (Main.netMode == 0)
            {
                Main.NewText(text, new Color(175, 75, 255));
                return;
            }
            if (Main.netMode == 2)
            {
                //Sync with net
                NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f, 0, 0, 0);
            }
        }

        public static void UpdateInvasion()
        {
            if (HonkaiWorld.honkaiInvasionActive)
            {
                if (Main.invasionSize <= 0)
                {
                    HonkaiWorld.honkaiInvasionActive = false;
                    invasionWarning();
                    Main.invasionType = 0;
                    Main.invasionDelay = 0;
                }

                if (Main.invasionX == (double)Main.spawnTileX)
                {
                    return;
                }

                float moveRate = (float)Main.dayRate;

                if (Main.invasionX > (double)Main.spawnTileX)
                {
                    //Decrement invasion x as to "move them"
                    Main.invasionX -= (double)moveRate;

                    //If less than the spawn pos, set invasion pos to spawn pos and warn players that invaders are at spawn
                    if (Main.invasionX <= (double)Main.spawnTileX)
                    {
                        Main.invasionX = (double)Main.spawnTileX;
                        invasionWarning();
                    }
                    else
                    {
                        Main.invasionWarn--;
                    }
                }
                else
                {
                    //Same thing as the if statement above, just it is from the other side
                    if (Main.invasionX < (double)Main.spawnTileX)
                    {
                        Main.invasionX += (double)moveRate;
                        if (Main.invasionX >= (double)Main.spawnTileX)
                        {
                            Main.invasionX = (double)Main.spawnTileX;
                            invasionWarning();
                        }
                        else
                        {
                            Main.invasionWarn--;
                        }
                    }
                }
            }
        }

        public static void CheckCustomInvasionProgress()
        {
            //Not really sure what this is
            if (Main.invasionProgressMode != 2)
            {
                Main.invasionProgressNearInvasion = false;
                return;
            }

            //Checks if NPCs are in the spawn area to set the flag, which I do not know what it does
            bool flag = false;
            Player player = Main.player[Main.myPlayer];
            Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
            int num = 5000;
            int icon = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active)
                {
                    icon = 0;
                    int type = Main.npc[i].type;
                    for (int n = 0; n < invaders.Length; n++)
                    {
                        if (type == invaders[n])
                        {
                            Rectangle value = new Rectangle((int)(Main.npc[i].position.X + (float)(Main.npc[i].width / 2)) - num, (int)(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2)) - num, num * 2, num * 2);
                            if (rectangle.Intersects(value))
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
            }
            Main.invasionProgressNearInvasion = flag;
            int progressMax3 = 1;

            //If the custom invasion is up, set the max progress as the initial invasion size
            if (HonkaiWorld.honkaiInvasionActive)
            {
                progressMax3 = Main.invasionSizeStart;
            }

            //If the custom invasion is up and the enemies are at the spawn pos
            if (HonkaiWorld.honkaiInvasionActive && (Main.invasionX == (double)Main.spawnTileX))
            {
                //Shows the UI for the invasion
                Main.ReportInvasionProgress(Main.invasionSizeStart - Main.invasionSize, progressMax3, icon, 0);
            }

            //Syncing start of invasion
            foreach (Player p in Main.player)
            {
                NetMessage.SendData(MessageID.InvasionProgressReport, p.whoAmI, -1, null, Main.invasionSizeStart - Main.invasionSize, (float)Main.invasionSizeStart, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
            }
        }
    }
}
