using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using UnityEngine;

namespace VagmrCards.Enhancers
{
    /// <summary>
    /// 亡魂之力强化器类
    /// 这个类实现了一个能够在楼层上任何单位死亡时获得+3生命和+3攻击的强化器
    /// </summary>
    public class SoulHarvester
    {
        public static readonly string ID = VagmrCardsPatch.GUID + "_SoulHarvester";
        public static readonly string UpgradeID = VagmrCardsPatch.GUID + "_SoulHarvesterUpgrade";
        public static readonly string UpgradeMaskID = VagmrCardsPatch.GUID + "_SoulHarvesterMask";
        public static readonly string TriggeredEffectID = VagmrCardsPatch.GUID + "_SoulHarvesterTriggeredEffect";

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
                ParamInt = 3, // +3攻击   
            };

            var HealthyBuff = new CardEffectDataBuilder
            {
                EffectStateType = typeof(CardEffectBuffMaxHealth),
                TargetMode = TargetMode.Self,
                TargetTeamType = Team.Type.Monsters,
                ParamInt = 5, // +5生命
            };

            // 创建触发效果 - 当楼层上任何单位死亡时触发
            var triggeredEffect = new CharacterTriggerDataBuilder
            {
                TriggerID = TriggeredEffectID,
                Trigger = CharacterTriggerData.Trigger.OnAnyUnitDeathOnFloor, // 当楼层上任何单位死亡时触发
                Description = "当前楼层上任何单位死亡时，获得+3[attack]和+5[health]",
                EffectBuilders = { AttackBuff, HealthyBuff },
            };

            // 创建卡牌升级数据
            var upgrade = new CardUpgradeDataBuilder
            {
                UpgradeID = UpgradeID,
                UpgradeTitle = "亡魂之力",
                UpgradeDescription = "当前楼层任何单位死亡时，获得+3[attack]和+5[health]",
                // 目标类型CharacterTriggerDataBuilder对角色施加效果
                TriggerUpgradeBuilders = {triggeredEffect},
                HideUpgradeIconOnCard = false,
                FiltersBuilders = { filter },
                AssetPath = "assets/SoulHarvester.png",
            }.Build();

            // 创建并注册强化器数据
            new EnhancerDataBuilder
            {
                EnhancerID = ID,
                Name = "亡魂之力",
                Description = "当前楼层上任何单位死亡时，获得+3[attack]和+5[health]",
                EnhancerPoolIDs = { 
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, 
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, 
                },
                Upgrade = upgrade,
                Rarity = CollectableRarity.Common,
                CardType = CardType.Monster,
                IconPath = "assets/SoulHarvester.png"
            }.BuildAndRegister();
        }
    }
}
