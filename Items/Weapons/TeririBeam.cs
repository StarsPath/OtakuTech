using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Items.Weapons
{
    public class TeririBeam : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Eyes of wisdom will enlighten every entity on sight");
        }

        public override void SetDefaults()
        {
            item.value = 10000;
            item.useTime = 12;
            item.useAnimation = 12;
            item.reuseDelay = 20;
            item.autoReuse = true;
            item.width = 32;
            item.height = 32;
            item.scale = 0.5f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item105;
        }

        public override bool AltFunctionUse(Player player)
        {
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            ModdedPlayer modplayer = player.GetModPlayer<ModdedPlayer>();
            for(int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].CanBeChasedBy() || !Main.npc[i].friendly)
                {
                    Main.npc[i].knockBackResist = 0;
                    Main.npc[i].aiStyle = 0;
                    Main.npc[i].velocity = Vector2.Zero;
                }
                if (Main.npc[i].dontTakeDamage)
                {
                    Main.npc[i].dontTakeDamage = false;
                }
            }
            return base.CanUseItem(player);
        }
    }
}
