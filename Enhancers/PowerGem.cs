using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using UnityEngine;

namespace VagmrCards.Enhancers
{
    /// <summary>
    /// 力之宝石强化器类
    /// 这个类实现了一个能够大幅提升单位攻击力和生命值的强化器
    /// </summary>
    public class PowerGem
    {
        /// <summary>
        /// 强化器的唯一标识符
        /// </summary>
        public static readonly string ID = VagmrCardsPatch.GUID + "_PowerGem";
        
        /// <summary>
        /// 升级效果的唯一标识符
        /// </summary>
        public static readonly string UpgradeID = VagmrCardsPatch.GUID + "_PowerGemUpgrade";
        
        /// <summary>
        /// 升级过滤器的唯一标识符
        /// </summary>
        public static readonly string UpgradeMaskID = VagmrCardsPatch.GUID + "_PowerGemUpgradeMask";

        /// <summary>
        /// 构建并注册力之宝石强化器
        /// 该方法创建升级过滤器、升级数据和强化器，并将其注册到游戏中
        /// </summary>
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

            // 创建卡牌升级数据，定义升级效果：增加35点攻击力和25点生命值
            var upgrade = new CardUpgradeDataBuilder
            {
                UpgradeID = UpgradeID,
                UpgradeTitle = "力之宝石", // 升级标题
                UpgradeDescription = "增加单位 +25[attack] 和 +35[health]", // 升级描述
                BonusDamage = 25, // 增加25点攻击力
                BonusHP = 35, // 增加35点生命值
                HideUpgradeIconOnCard = false, // 在卡牌上显示升级图标
                FiltersBuilders = { filter }, // 应用之前创建的过滤器
                AssetPath = "assets/PowerGem.png", // 升级图标资源路径
            }.Build();

            // 创建并注册强化器数据
            new EnhancerDataBuilder
            {
                EnhancerID = ID,
                Name = "力之宝石", // 强化器名称
                Description = "增加单位 +25[attack] 和 +35[health] ", // 强化器描述
                // 强化器池ID列表，添加到普通池中，多次添加增加出现概率用于测试
                EnhancerPoolIDs = { 
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, 
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, 
                    // 正式游玩时注释掉
                    // VanillaEnhancerPoolIDs.UnitUpgradePoolCommon,
                    // VanillaEnhancerPoolIDs.UnitUpgradePoolCommon,
                    // VanillaEnhancerPoolIDs.UnitUpgradePoolCommon,
                    // VanillaEnhancerPoolIDs.UnitUpgradePoolCommon,
                    // VanillaEnhancerPoolIDs.UnitUpgradePoolCommon
                }, 
                Upgrade = upgrade, // 使用之前创建的升级数据
                Rarity = CollectableRarity.Uncommon, // 设置为普通稀有度
                CardType = CardType.Monster, // 适用于怪物卡牌
                IconPath = "assets/PowerGem.png", // 强化器图标资源路径
            }.BuildAndRegister();
        }
    }
}
