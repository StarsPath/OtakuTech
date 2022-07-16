//using OtakuTech.Buffs;
//using OtakuTech.Dusts;
//using OtakuTech.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OtakuTech.Common.Players
{
    // ModPlayer classes provide a way to attach data to Players and act on that data. ExamplePlayer has a lot of functionality related to 
    // several effects and items in ExampleMod. See SimpleModPlayer for a very simple example of how ModPlayer classes work.
    public class ModdedPlayer : ModPlayer
    {
        public bool reasonableCannon;
        public bool crossDeploy;
        public bool libationToFire;
        public bool excelsisKing;
        public bool fervent;
        public bool velionasTorrent;
        public bool eightFormations;

        public bool enchanced = false;

        public float voidTeleportCD = 0;
        public float libationCD = 0;
        public float undineCD = 0;

        public bool checkCombo = true;
        public float comboTimer = 0f;
        public int comboCount = 0;
        //public int voidConsecutiveHit = 0;
        public int haxxorDroneCount = 0;
        public int MAX_haxxorDroneCount = 2;

        //private int MAX_FEATHER = 20;


        public override void ResetEffects()
        {
            reasonableCannon = false;
            libationToFire = false;
            excelsisKing = false;
            fervent = false;
            velionasTorrent = false;
            eightFormations = false;
        }
        public override void UpdateDead()
        {
            reasonableCannon = false;
            libationToFire = false;
            excelsisKing = false;
            fervent = false;
            velionasTorrent = false;
            eightFormations = false;
            base.UpdateDead();
        }
        public override void PostItemCheck()
        {
            //if (Player.HeldItem.ModItem is HekatesSombre || Player.HeldItem.ModItem is DomainOfVoid)
            //    enchanced = true;
            //else
            //    enchanced = false;

            base.PostItemCheck();
        }


        //public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        //{
        //    if (player.HeldItem.modItem is ObscuringWing)
        //    {
        //        int current_feather = Main.projectile.Where(p => p is FeatherBlade).ToArray().Length;
        //        Main.NewText(current_feather);

        //    }
        //    base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        //}
        public override void PostUpdate()
        {
            //Main.NewText(comboCount);
            if (checkCombo && comboTimer > 0)
            {
                comboTimer--;
            }
            if (comboTimer <= 0)
            {
                comboCount = 0;
            }

            if (voidTeleportCD > 0)
                voidTeleportCD -= 1;
            if (libationCD > 0)
                libationCD -= 1;
            if (undineCD > 0)
                undineCD -= 1;
            base.PostUpdate();
        }
        public void addCombo(int count = 1)
        {
            comboCount += count;
            comboTimer = 90f;
        }

        public bool resetCombo(int limit)
        {
            if (comboCount >= limit)
            { 
                comboCount = 0;
                return true;
            }
            return false;
        }
    }
}
