// Decompiled with JetBrains decompiler
// Type: GorillaCosmeticNotifier.CosmeticNotifierPlugin
// Assembly: GorillaCosmeticNotifier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 53B13164-D047-4FF6-957E-5DC8218702D8
// Assembly location: C:\Users\erich\Downloads\GorillaCosmeticNotifier.dll

using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace GorillaCosmeticNotifier;

[BepInPlugin("com.WhonGT.GorillaCosmeticNotifier", "GorillaCosmeticNotifier", "1.0.0")]
[BepInDependency]
public class CosmeticNotifierPlugin : BaseUnityPlugin
{
  public const string GUID = "com.WhonGT.GorillaCosmeticNotifier";
  public const string PluginName = "GorillaCosmeticNotifier";
  public const string VersionString = "1.0.0";
  public static readonly Dictionary<string, string> WatchedCosmetics = new Dictionary<string, string>()
  {
    {
      "LBANI.",
      "AAC Badge"
    },
    {
      "LMAPY.",
      "Forest Guide Stick"
    },
    {
      "LBAAK.",
      "Developer Stick"
    },
    {
      "LBADE.",
      "Finger Painter Badge"
    },
    {
      "LBAGS.",
      "Illustrator Badge"
    },
    {
      "LBAAD.",
      "Admin Badge"
    }
  };
  internal static readonly HashSet<int> NotifiedRigInstanceIds = new HashSet<int>();

  private void Awake()
  {
    this.Logger.LogInfo((object) "GorillaCosmeticNotifier v1.0.0 loaded.");
    new Harmony("com.WhonGT.GorillaCosmeticNotifier").PatchAll(Assembly.GetExecutingAssembly());
  }
}
