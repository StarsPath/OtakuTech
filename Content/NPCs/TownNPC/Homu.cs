using Microsoft.Xna.Framework.Graphics;
using OtakuTech.Common.Eruption;
using OtakuTech.Content.Items.Accessories;
using OtakuTech.Content.Items.Placeables;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace OtakuTech.Content.NPCs.TownNPC
{
    [AutoloadHead]
    public class Homu : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25; // The amount of frames the NPC has

            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs.
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the npc that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return HonkaiWorld.honkaiInvasionDown;
            //return false;
        }

        //public override bool CheckConditions(int left, int right, int top, int bottom)
        //{
        //    return false;
        //}

        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
        {
            return new List<string>() {
                "Homu",
                "Homei",
                "Hola",
                "Homi",
                "Holi",
                "Howo",
                "Hotaro"
            };
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add(Language.GetTextValue("Mods.OtakuTech.Dialogue.Homu.D1"));
            chat.Add(Language.GetTextValue("Mods.OtakuTech.Dialogue.Homu.D2"));
            chat.Add(Language.GetTextValue("Mods.OtakuTech.Dialogue.Homu.D3"));
            chat.Add(Language.GetTextValue("Mods.OtakuTech.Dialogue.Homu.D4"));
            chat.Add(Language.GetTextValue("Mods.OtakuTech.Dialogue.Homu.D5"));
            chat.Add(Language.GetTextValue("Mods.OtakuTech.Dialogue.Homu.D6"));
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            base.SetChatButtons(ref button, ref button2);
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
                shop = true;
            //base.OnChatButtonClicked(firstButton, ref shop);
        }

        Dictionary<int, int> sellItemDict = new Dictionary<int, int>()
        {
            {ModContent.ItemType<StandardSupplyItem>(), 28},
            {ModContent.ItemType<FocusSupplyItem>(), 28},
            {ModContent.ItemType<ExpansionSupplyItem>(), 28},
            {ModContent.ItemType<Burden>(), 28},
            {ModContent.ItemType<ForbiddenSeed>(), 28},
            {ModContent.ItemType<ForgetMeNot>(), 28},
            {ModContent.ItemType<GoldGoblet>(), 28},
            {ModContent.ItemType<LightAsABodhiLeaf>(), 28},
            {ModContent.ItemType<MadKingsMask>(), 28},
            {ModContent.ItemType<Memory>(), 28},
        };

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<StandardSupplyItem>());
            //shop.item[nextSlot].shopCustomPrice = 28;
            //shop.item[nextSlot].shopSpecialCurrency = OtakuTech.CrystalCurrencyId;
            //nextSlot++;

            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<FocusSupplyItem>());
            //shop.item[nextSlot].shopCustomPrice = 28;
            //shop.item[nextSlot].shopSpecialCurrency = OtakuTech.CrystalCurrencyId;
            //nextSlot++;

            //shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExpansionSupplyItem>());
            //shop.item[nextSlot].shopCustomPrice = 28;
            //shop.item[nextSlot].shopSpecialCurrency = OtakuTech.CrystalCurrencyId;
            //nextSlot++;

            foreach(KeyValuePair<int, int> entry in sellItemDict)
            {
                shop.item[nextSlot].SetDefaults(entry.Key);
                shop.item[nextSlot].shopCustomPrice = entry.Value;
                shop.item[nextSlot].shopSpecialCurrency = OtakuTech.CrystalCurrencyId;
                nextSlot++;
            }
            //base.SetupShop(shop, ref nextSlot);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
        }

        public override bool CanGoToStatue(bool toKingStatue) => true;


        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }

        //public override void LoadData(TagCompound tag)
        //{
        //    NumberOfTimesTalkedTo = tag.GetInt("numberOfTimesTalkedTo");
        //}

        //public override void SaveData(TagCompound tag)
        //{
        //    tag["numberOfTimesTalkedTo"] = NumberOfTimesTalkedTo;
        //}

        public override ITownNPCProfile TownNPCProfile()
        {
            return new HomuProfile();
        }
    }
    public class HomuProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;

        public string GetNameForVariant(NPC npc) => npc.getNewNPCName();


        public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
        {
            if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                return ModContent.Request<Texture2D>("OtakuTech/Content/NPCs/TownNPC/Homu");

            //if (npc.altTexture == 1)
            //    return ModContent.Request<Texture2D>("ExampleMod/Content/NPCs/ExamplePerson_Party");

            return ModContent.Request<Texture2D>("OtakuTech/Content/NPCs/TownNPC/Homu");
        }
        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("OtakuTech/Content/NPCs/TownNPC/Homu_Head");

    }
}
