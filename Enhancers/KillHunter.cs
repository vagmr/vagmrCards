using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using UnityEngine;

namespace VagmrCards.Enhancers
{
    /// <summary>
    /// 击杀猎手强化器类
    /// 这个类实现了一个能够在击杀敌人时获得+10攻击和+5生命的强化器
    /// </summary>
    public class KillHunter
    {
        public static readonly string ID = VagmrCardsPatch.GUID + "_KillHunter";
        public static readonly string UpgradeID = VagmrCardsPatch.GUID + "_KillHunterUpgrade";
        public static readonly string UpgradeMaskID = VagmrCardsPatch.GUID + "_KillHunterMask";
        public static readonly string TriggeredEffectID = VagmrCardsPatch.GUID + "_KillHunterTriggeredEffect";

        public static void BuildAndRegister()
        {
            // 创建卡牌升级过滤器，适用于所有攻击型怪物卡牌
            var filter = new CardUpgradeMaskDataBuilder
            {
                CardUpgradeMaskID = UpgradeMaskID,
                ExcludeNonAttackingMonsters = true,
                CardType = CardType.Monster, // 只应用于怪物卡牌
                CostRange = new Vector2 { x = 0, y = 99 }, // 适用于任何费用的卡牌
            };

            var AttackBuff = new CardEffectDataBuilder
            {
                EffectStateType = typeof(CardEffectBuffDamage),
                TargetMode = TargetMode.Self,
                TargetTeamType = Team.Type.Monsters,
                ParamInt = 10, // +10攻击
            };

            var HealthyBuff = new CardEffectDataBuilder
            {
                EffectStateType = typeof(CardEffectBuffMaxHealth),
                TargetMode = TargetMode.Self,
                TargetTeamType = Team.Type.Monsters,
                ParamInt = 2, // +2生命
            };

            //全体护盾增益
            var ShieldBuff = new CardEffectDataBuilder
            {
                EffectStateType = typeof(CardEffectAddStatusEffect),
                TargetMode = TargetMode.Room,
                TargetTeamType =  Team.Type.Monsters,
                ParamStatusEffects =
                {
                    new StatusEffectStackData
                    {
                    statusId = VanillaStatusEffectIDs.Armor,
                    count = 2
                    }
                },// +2护盾
            };

            // 创建触发效果 - 当击杀敌人时触发
            var triggeredEffect = new CharacterTriggerDataBuilder
            {
                TriggerID = TriggeredEffectID,
                Trigger = CharacterTriggerData.Trigger.OnKill, // 当击杀敌人时触发
                Description = "当击杀敌人时，获得+10[attack]、+5[health]和+2[armor]",
                DescriptionKey = TriggeredEffectID + "DESC",
                EffectBuilders = { AttackBuff, HealthyBuff, ShieldBuff },
                AdditionalTextOnTrigger = "猎手本能触发",
                AdditionalTextOnTriggerKey = TriggeredEffectID + "GAIN_KILL_HUNTER",
            };

            // 创建卡牌升级数据
            var upgrade = new CardUpgradeDataBuilder
            {
                UpgradeID = UpgradeID,
                UpgradeTitle = "猎手本能",
                BonusHP = -5,
                UpgradeDescription = "当击杀敌人时，获得+10[attack]和+5[health]和+2[armor]",
                // 目标类型CharacterTriggerDataBuilder对角色施加效果
                TriggerUpgradeBuilders = {triggeredEffect},
                HideUpgradeIconOnCard = false,
                FiltersBuilders = { filter },   
                AssetPath = "assets/KillHunter.png",
            }.Build();

            // 创建并注册强化器数据
            new EnhancerDataBuilder
            {
                EnhancerID = ID,
                Name = "猎手本能",
                Description = " -5[health]，当击杀敌人时，获得+10[attack]和+5[health]及+2[armor]",
                EnhancerPoolIDs = { 
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon,
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon,
                },
                Upgrade = upgrade,
                Rarity = CollectableRarity.Rare,
                CardType = CardType.Monster,
                IconPath = "assets/KillHunter.png"
            }.BuildAndRegister();
        }
    }
} 