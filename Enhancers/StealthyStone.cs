using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using UnityEngine;

namespace VagmrCards.Enhancers
{
    /// <summary>
    /// 隐匿石强化器类
    /// 这个类实现了一个能够赋予单位隐匿能力但减少攻击力的强化器
    /// </summary>
    public class StealthyStone
    {
        /// <summary>
        /// 强化器的唯一标识符
        /// </summary>
        public static readonly string ID = VagmrCardsPatch.GUID+ "_SteathyStone";
        
        /// <summary>
        /// 升级效果的唯一标识符
        /// </summary>
        public static readonly string UpgradeID = VagmrCardsPatch.GUID + "_SteathyStoneUpgrade";
        
        /// <summary>
        /// 升级过滤器的唯一标识符
        /// </summary>
        public static readonly string UpgradeMaskID = VagmrCardsPatch.GUID + "_SteathyStoneUpgradeMask";

        /// <summary>
        /// 构建并注册隐匿石强化器
        /// 该方法创建升级过滤器、升级数据和强化器，并将其注册到游戏中
        /// </summary>
        public static void BuildAndRegister()
        {
            // 创建卡牌升级过滤器，限制只能应用于攻击型怪物卡牌
            var filter = new CardUpgradeMaskDataBuilder
            {
                CardUpgradeMaskID = UpgradeMaskID,
                ExcludeNonAttackingMonsters = true,  // 排除非攻击型怪物 (Exclude non-attacking monsters)
                CardType = CardType.Monster,         // 只应用于怪物卡牌 (Only apply to monster cards)
                CostRange = new Vector2 { x = 0, y = 99 }, // 适用于任何费用的卡牌 (Apply to cards of any cost)
            };

            // 创建卡牌升级数据，定义升级效果：减少5点攻击力并增加5层隐匿效果
            var upgrade = new CardUpgradeDataBuilder
            {
                UpgradeID = UpgradeID,
                UpgradeTitle = "隐匿石",      // 升级标题 (Upgrade title)
                UpgradeDescription = "减少单位 -5[attack] 同时增加3层[stealth]", // 升级描述 (Upgrade description)
                BonusDamage = -5,                    // 减少5点攻击力 (Reduce attack by 5)
                StatusEffectUpgrades =               // 状态效果升级 (Status effect upgrades)
                {
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Stealth, count = 3}, // 添加3层隐匿效果 (Add 3 stacks of stealth)
                },
                HideUpgradeIconOnCard = false,       // 在卡牌上显示升级图标 (Show upgrade icon on card)
                FiltersBuilders = { filter },        // 应用之前创建的过滤器 (Apply the previously created filter)
                AssetPath = "assets/StealthyStone.png", // 升级图标资源路径 (Upgrade icon asset path)
            }.Build();

            // 创建并注册强化器数据
            new EnhancerDataBuilder
            {
                EnhancerID = ID,
                Name = "隐匿石",              // 强化器名称 (Enhancer name)
                Description = "让一个单位 -5[attack] 并增加 [stealth] 3 层", // 强化器描述 (Enhancer description)
                EnhancerPoolIDs = 
                { 
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon,
                    VanillaEnhancerPoolIDs.UnitUpgradePoolCommon,
                }, // 强化器池ID列表，多次添加增加出现概率
                Upgrade = upgrade,                   // 使用之前创建的升级数据 (Use the previously created upgrade data)
                Rarity = CollectableRarity.Common,   // 设置为普通稀有度 (Set to common rarity)
                CardType = CardType.Monster,         // 适用于怪物卡牌 (Applicable to monster cards)
                IconPath = "assets/StealthyStone.png", // 强化器图标资源路径 (Enhancer icon asset path)
            }.BuildAndRegister();
        }
    }
}