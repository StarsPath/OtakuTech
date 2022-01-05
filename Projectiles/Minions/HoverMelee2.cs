using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace OtakuTech.Projectiles.Minions
{
    //AI_120_StardustGuardian
    public abstract class HoverMelee2 : Minion
    {
        //protected float idleAccel = 0.1f;
        //protected float spacingMult = 0.2f;
        //protected float viewDist = 600f;
        //protected float chaseDist = 400f;
        //protected float chaseAccel = 8f;
        //protected float inertia = 50f;
        //protected float shootCool = 50f;
        //protected float shootSpeed;
        //protected int shoot;

        private float agroDist = 200f;
        private float attackRange = 100f;
        private float movementSpeed = 8f;

        private float cooldown = 300f;
        private float currentCD = 0f;

        private int frames = 15;

        private const int State_idle = 0;
        private const int State_chase = 1;
        private const int State_attack = 2;

        private const int AI_State_Slot = 0;
        private const int AI_Target_Slot = 1;
        private const int AI_AttackCooldown_Slot = 2;

        public float AI_State
        {
            get => projectile.ai[AI_State_Slot];
            set => projectile.ai[AI_State_Slot] = value;
        }

        public float AI_TargetIndex
        {
            get => projectile.ai[AI_Target_Slot];
            set => projectile.ai[AI_Target_Slot] = value;
        }


        public virtual void CreateDust()
        {
        }

        public virtual void SelectFrame()
        {
        }

        public override void Behavior()
        {
            Player player = Main.player[projectile.owner];
            Vector2 playerPosition = player.Center;
            playerPosition.X -= (5 + player.width / 2) * player.direction;
            playerPosition.Y -= 25f;
            projectile.direction = (projectile.spriteDirection = player.direction);
            currentCD--;
            //Main.NewText(AI_State);

            if (AI_State == State_idle)
            {
                int targetNPCIndex = -1;
                float distanceToTarget = 0f;
                if (++projectile.frameCounter >= 4)
                {
                    projectile.frameCounter = 0;
                    if (++projectile.frame > 3)
                    {
                        projectile.frame = 0;
                    }
                }

                projectile.Center = Vector2.Lerp(projectile.Center, playerPosition, 0.2f);
                projectile.velocity *= 0.05f;
                projectile.direction = (projectile.spriteDirection = player.direction);

                AI_120_StardustGuardian_FindTarget(agroDist, ref targetNPCIndex, ref distanceToTarget);
                AI_TargetIndex = targetNPCIndex;

                if (AI_TargetIndex != -1 && currentCD <= 0)
                {
                    AI_State = State_chase;
                }
            }
            else if (AI_State == State_chase)
            {
                if (AI_TargetIndex != -1)
                {
                    int targetNPCIndex = -1;
                    float distanceToTarget = 0f;
                    if (++projectile.frameCounter >= 4)
                    {
                        projectile.frameCounter = 0;
                        if (++projectile.frame >= 3)
                        {
                            projectile.frame = 0;
                        }
                    }

                    AI_120_StardustGuardian_FindTarget(agroDist, ref targetNPCIndex, ref distanceToTarget);
                    AI_TargetIndex = targetNPCIndex;

                    if (AI_TargetIndex == -1)
                    {
                        AI_State = State_idle;
                        return;
                    }

                    NPC nPC = Main.npc[targetNPCIndex];
                    projectile.direction = (projectile.spriteDirection = (nPC.Center.X > projectile.Center.X).ToDirectionInt());
                    float num6 = Math.Abs(playerPosition.X - projectile.Center.X);
                    float num7 = Math.Abs(nPC.Center.X - projectile.Center.X);
                    float num8 = Math.Abs(playerPosition.Y - projectile.Center.Y);
                    float num9 = Math.Abs(nPC.Bottom.Y - projectile.Bottom.Y);
                    float num10 = (nPC.Center.Y > projectile.Bottom.Y).ToDirectionInt();
                    if ((num6 < agroDist || (playerPosition.X - projectile.Center.X) * (float)projectile.direction < 0f))
                    {
                        projectile.velocity.X += 1f * (float)projectile.direction;
                    }
                    else
                    {
                        projectile.velocity.X *= 0.7f;
                    }
                    if ((num8 < agroDist || (playerPosition.Y - projectile.Bottom.Y) * num10 < 0f))
                    {
                        projectile.velocity.Y += 1f * num10;
                    }
                    else
                    {
                        projectile.velocity.Y *= 0.7f;
                    }

                    NPC targetNPC = Main.npc[targetNPCIndex];
                    if (player.Distance(projectile.Center) > 1.4f * agroDist || AI_TargetIndex == -1)
                    {
                        //Main.NewText("Out of Range, Idle State");
                        AI_State = State_idle;
                        AI_TargetIndex = -1;
                    }
                    else if (projectile.Distance(targetNPC.Center) < attackRange)
                    {
                        AI_State = State_attack;
                        //Main.NewText("In Attack Range, StateAttack");
                    }
                    //else
                    //{
                    //    Main.NewText("Moving to NPC");
                    //    Vector2 dir = targetNPC.Bottom - projectile.Bottom;
                    //    dir.Normalize();
                    //    projectile.velocity = dir * movementSpeed;
                    //}
                }
                else
                {
                    AI_State = State_idle;
                }
            }
            else if (AI_State == State_attack)
            {
                if (AI_TargetIndex != -1)
                {
                    int targetNPCIndex = -1;
                    float distanceToTarget = 0f;
                    if (projectile.frame < 4)
                    {
                        projectile.frame = 4;
                    }
                    if (++projectile.frameCounter >= 4)
                    {
                        projectile.frameCounter = 0;
                        if (++projectile.frame >= 16)
                        {
                            projectile.frame = 4;
                            AI_State = State_idle;
                            AI_TargetIndex = -1;
                        }

                        if (projectile.frame == 9 && currentCD <= 0)
                        {
                            //Main.NewText("SHOOTING");
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.spriteDirection * 10f, 0, ModContent.ProjectileType<PhantomCleave1>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                            currentCD = cooldown;
                        }

                        if (projectile.frame == 13)
                        {
                            //Main.NewText("SHOOTING2");
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.spriteDirection * 12f, 0, ModContent.ProjectileType<PhantomCleave2>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                        }
                    }

                    //sound Item 82

                    AI_120_StardustGuardian_FindTarget(agroDist, ref targetNPCIndex, ref distanceToTarget);
                    AI_TargetIndex = targetNPCIndex;

                    if (AI_TargetIndex == -1)
                    {
                        AI_State = State_idle;
                        return;
                    }

                    NPC nPC = Main.npc[targetNPCIndex];
                    projectile.direction = (projectile.spriteDirection = (nPC.Center.X > projectile.Center.X).ToDirectionInt());
                    float num6 = Math.Abs(playerPosition.X - projectile.Center.X);
                    float num7 = Math.Abs(nPC.Center.X - projectile.Center.X);
                    float num8 = Math.Abs(playerPosition.Y - projectile.Center.Y);
                    float num9 = Math.Abs(nPC.Bottom.Y - projectile.Bottom.Y);
                    float num10 = (nPC.Center.Y > projectile.Bottom.Y).ToDirectionInt();
                    if ((num6 < agroDist || (playerPosition.X - projectile.Center.X) * (float)projectile.direction < 0f) && num7 > 20f && num7 < agroDist - num6 + 100f)
                    {
                        projectile.velocity.X += 1f * (float)projectile.direction;
                    }
                    else
                    {
                        projectile.velocity.X *= 0.7f;
                    }
                    if ((num8 < agroDist || (playerPosition.Y - projectile.Bottom.Y) * num10 < 0f) && num9 > 10f && num9 < agroDist - num8 + 10f)
                    {
                        projectile.velocity.Y += 1f * num10;
                    }
                    else
                    {
                        projectile.velocity.Y *= 0.7f;
                    }

                    NPC targetNPC = Main.npc[targetNPCIndex];
                    if (player.Distance(projectile.Center) > 1.4f * agroDist || AI_TargetIndex == -1)
                    {
                        //Main.NewText("Player Too far, StateIdle");
                        AI_State = State_idle;
                        AI_TargetIndex = -1;
                    }
                    //else if (projectile.Distance(targetNPC.Center) < attackRange)
                    //{
                    //    Main.NewText("Attacking");
                    //    Vector2 dir = targetNPC.Bottom - projectile.Bottom;
                    //    dir.Normalize();
                    //    projectile.velocity = dir * movementSpeed * 0.5f;
                    //}
                    //else
                    //{
                    //    Main.NewText("Chasing");
                    //    AI_State = State_chase;
                    //}
                }
                else
                {
                    AI_State = State_idle;
                }
            }
            else
            {

            }
        }

        private void AI_120_StardustGuardian_FindTarget(float lookupRange, ref int targetNPCIndex, ref float distanceToClosestTarget)
        {
            Player player = Main.player[projectile.owner];
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(this))
                {
                    float num = player.Distance(nPC.Center);
                    if (num < lookupRange)
                    {
                        targetNPCIndex = i;
                        distanceToClosestTarget = num;
                        lookupRange = num;
                    }
                }
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = true;
            return true;
        }
    }
}