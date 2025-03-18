using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using UnityEngine;

namespace VagmrCards.Enhancers
{
    /// <summary>
    /// 疾风之刃强化器类
    /// 这个类实现了一个能够提升单位攻击力、降低生命值并赋予迅捷和多重攻击能力的强化器
    /// </summary>
    public class SwiftBlade
    {
        public static readonly string ID = VagmrCardsPatch.GUID + "_SwiftBlade";
        public static readonly string UpgradeID = VagmrCardsPatch.GUID + "_SwiftBladeUpgrade";
        public static readonly string UpgradeMaskID = VagmrCardsPatch.GUID + "_SwiftBladeMask";

        public static void BuildAndRegister()
        {
            // 创建卡牌升级过滤器，适用于所有怪物卡牌
            var filter = new CardUpgradeMaskDataBuilder
            {
                CardUpgradeMaskID = UpgradeMaskID,
                ExcludeNonAttackingMonsters = true,  
                CardType = CardType.Monster, // 只应用于怪物卡牌
                CostRange = new Vector2 { x = 0, y = 99 }, // 适用于任何费用的卡牌
            };

            // 创建卡牌升级数据，定义升级效果：+5攻击，-5生命，迅捷1，多重攻击2
            var upgrade = new CardUpgradeDataBuilder
            {
                UpgradeID = UpgradeID,
                UpgradeTitle = "疾风之刃", // 升级标题
                UpgradeDescription = "增加单位 +5[attack] 和 -5[health] 并附加 1层[quick] 和 2层[multistrike] ", // 升级描述
                BonusDamage = 5, // 增加5点攻击力
                BonusHP = -5, // 减少5点生命值
                StatusEffectUpgrades = 
                {
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Quick, count = 1}, // 添加1层迅捷效果
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Multistrike, count = 2}, // 添加2层多重攻击效果
                },
                HideUpgradeIconOnCard = false, // 在卡牌上显示升级图标
                FiltersBuilders = { filter }, // 应用之前创建的过滤器
                AssetPath = "assets/SwiftBlade.png", // 升级图标资源路径
            }.Build();

            // 创建并注册强化器数据
            new EnhancerDataBuilder
            {
                EnhancerID = ID,
                Name = "疾风之刃", // 强化器名称
                Description = "增加单位 +5[attack] 和 -5[health] 增加 [quick] 1 和 [multistrike] 2", // 强化器描述
                EnhancerPoolIDs = { 
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, 
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, 
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon
                }, // 强化器池ID列表，添加到非普通池中，多次添加增加出现概率
                Upgrade = upgrade, // 使用之前创建的升级数据
                Rarity = CollectableRarity.Common, 
                CardType = CardType.Monster, // 适用于怪物卡牌
                IconPath = "assets/SwiftBlade.png", // 强化器图标资源路径
            }.BuildAndRegister();
        }
    }
}
