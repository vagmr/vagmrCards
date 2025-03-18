# 怪物列车自定义强化器 (Monster Train Custom Enhancers)

![Monster Train Logo](https://img.shields.io/badge/Monster%20Train-Mod-orange)
![C#](https://img.shields.io/badge/Language-C%23-blue)
![Version](https://img.shields.io/badge/Version-0.0.1-green)

## 📝 项目介绍 (Project Introduction)

这是一个为游戏《怪物列车》(Monster Train) 开发的MOD，添加了多种自定义强化器，为游戏增添新的策略选择和玩法可能性。

This is a mod developed for the game "Monster Train", adding various custom enhancers to bring new strategic choices and gameplay possibilities.

## ✨ 特性 (Features)

本MOD添加了以下自定义强化器：

1. **隐匿石 (StealthyStone)**
   - 效果：减少单位 -5 攻击力并增加 3 层隐匿效果
   - Effect: Reduces unit's attack by 5 and grants 3 stacks of stealth

2. **力之宝石 (PowerGem)**
   - 效果：增加单位 +25 攻击力和 +35 生命值
   - Effect: Increases unit's attack by 25 and health by 35

3. **疾风之刃 (SwiftBlade)**
   - 效果：增加单位 +5 攻击力和 -5 生命值，并附加 1 层快速和 2 层多重打击
   - Effect: Increases unit's attack by 5, decreases health by 5, and grants 1 stack of quick and 2 stacks of multistrike

4. **亡魂之力 (SoulHarvester)**
   - 效果：当前楼层上任何单位死亡时，获得 +3 攻击力和 +5 生命值
   - Effect: When any unit dies on the current floor, gain +3 attack and +5 health

## 🛠️ 安装方法 (Installation)


### 前置条件
   1. 安装 [Trainworks](https://github.com/KittenAqua/TrainworksModdingTools) 模组框架
   2. 安装 [BepInEx](https://github.com/BepInEx/BepInEx) 模组框架

### 安装步骤
   1. 下载本MOD的最新版本
   2. 将下载的dll文件复制到BepInEx的plugins文件夹中
   3. 启动游戏


## 🔧 开发说明 (Development Notes)

项目使用 C# 开发，基于 Trainworks 模组框架。主要文件结构：

- `main.cs` - 主入口文件，负责注册所有强化器
- `Enhancers/` - 包含所有强化器的实现代码


## 📄 许可证 (License)

本项目采用 MIT 许可证。详情请查看 [LICENSE](LICENSE) 文件。

---

由 vagmr 开发 © 2025
