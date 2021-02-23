using BepInEx;
using HarmonyLib;
using System;
using UnityEngine;
namespace LargeInventory
{
    [BepInPlugin("org.bepinex.plugins.largeinventory", "Large Inventory", "1.0.0.0")]
    public class LargeInventory : BaseUnityPlugin
    {
        void Awake()
        {
            Harmony harmony = new Harmony("mod.largeinventory");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Inventory), MethodType.Constructor)]
    [HarmonyPatch(new Type[] { typeof(string), typeof(Sprite), typeof(int), typeof(int) })]
    class Patch
    {
        static void Prefix(ref string name, ref Sprite bkg, ref int w, ref int h)
        {
            // Wood chest
            if (h == 2 && w == 5)
            {
                h = 4;
                w = 5;
            }
            // Player inventory
            else if (h == 4 && w == 8)
            {
                h = 7;
                w = 8;
            }
            // Iron chest, cart, boat
            else if (h == 3 && w == 6)
            {
                h = 4;
                w = 8;
            }
        }
    }

    [HarmonyPatch(typeof(InventoryGui), "Show")]
    public class InventoryGuiAwake
    {
        public static void Postfix(ref InventoryGui __instance, ref InventoryGrid ___m_playerGrid)
        {
            RectTransform container = __instance.m_container;
            RectTransform player = __instance.m_player;

            player.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 494);
            container.offsetMax = new Vector2(610, -582);
            container.offsetMin = new Vector2(40, -924);
        }
    }
}