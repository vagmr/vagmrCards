using BepInEx;
using HarmonyLib;
using Trainworks.Interfaces;
using VagmrCards.Enhancers;

namespace VagmrCards
{

    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    [BepInDependency("tools.modding.trainworks")]
    public class VagmrCardsPatch: BaseUnityPlugin, IInitializable
    {
        public const string GUID = "com.vagmr.Patch";
        public const string NAME = "VagmrCards";
        public const string VERSION = "0.1.0";

        private void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        public void Initialize()
        {
            // 注册隐匿石强化器
            StealthyStone.BuildAndRegister();
            
            // 注册力之宝石强化器
            PowerGem.BuildAndRegister();
            
            // 注册疾风之刃强化器
            SwiftBlade.BuildAndRegister();
            
            // 注册亡魂之力强化器
            SoulHarvester.BuildAndRegister();
            KillHunter.BuildAndRegister();
        }
    }
}