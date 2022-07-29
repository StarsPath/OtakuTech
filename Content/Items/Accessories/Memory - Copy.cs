using OtakuTech.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Content.Items.Accessories
{
	public class Memory : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("[c/4287f5:A photo that seems to be from a while back. The frame has a lot of impact dents..]\n"
							 //+ "+0.1% increased damage\n"
							 + "+0.8% increased melee damage for every seconds on Signets of Vicissitude buff\n"
							 + "+0.8% increased ranged damage for every seconds on Signets of Vicissitude buff\n"
							 + "+0.08% increased damage reduction for every seconds on Signets of Vicissitude buff\n"
							 + "Taking hits reduces Signets of Vicissitude buff time by half\n"
							 + "Signets of Vicissitude buff lingers for some time after unequiping\n");
			//+"20% increased damage reduction while holding melee item"); ;

			//CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			//Item.vanity = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
			Item.rare = ItemRarityID.Cyan;

		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(ModContent.BuffType<SignetsOfVicissitude>(), 2);
			int buffStack = player.buffTime[player.FindBuffIndex(ModContent.BuffType<SignetsOfVicissitude>())] / 60;
			//player.GetDamage(DamageClass.Generic) += buffStack * 0.001f;
			player.GetDamage(DamageClass.Melee) += buffStack * 0.008f;
			player.GetDamage(DamageClass.Ranged) += buffStack * 0.008f;
			player.endurance += buffStack * 0.0008f;
		}

		public class HitPlayer : ModPlayer
        {
            public override void OnHitByNPC(NPC npc, int damage, bool crit)
            {
				//int buffTime = Player.buffTime[Player.FindBuffIndex(ModContent.BuffType<SignetsOfVicissitude>())];
				Player.buffTime[Player.FindBuffIndex(ModContent.BuffType<SignetsOfVicissitude>())] /= 2;
				base.OnHitByNPC(npc, damage, crit);
            }

            public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
            {
				Player.buffTime[Player.FindBuffIndex(ModContent.BuffType<SignetsOfVicissitude>())] /= 2;
				base.OnHitByProjectile(proj, damage, crit);
            }
        }
	}
}
