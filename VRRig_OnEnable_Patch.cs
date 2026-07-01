// Decompiled with JetBrains decompiler
// Type: GorillaCosmeticNotifier.VRRig_OnEnable_Patch
// Assembly: GorillaCosmeticNotifier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 53B13164-D047-4FF6-957E-5DC8218702D8
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\GorillaCosmeticNotifier.dll

using GorillaNetworking;
using GorillaNotifications;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace GorillaCosmeticNotifier;

[HarmonyPatch(typeof (VRRig), "OnEnable")]
internal static class VRRig_OnEnable_Patch
{
  private static readonly FieldInfo _ownedCosmeticsField = typeof (VRRig).GetField("_playerOwnedCosmetics", BindingFlags.Instance | BindingFlags.NonPublic);

  private static void Postfix(VRRig __instance)
  {
    if (Object.op_Equality((Object) __instance, (Object) null) || __instance.isOfflineVRRig)
      return;
    ((MonoBehaviour) __instance).StartCoroutine(VRRig_OnEnable_Patch.CheckCosmeticsAfterDelay(__instance));
  }

  private static IEnumerator CheckCosmeticsAfterDelay(VRRig rig)
  {
    yield return (object) new WaitForSeconds(2f);
    if (!Object.op_Equality((Object) rig, (Object) null) && rig.cosmeticSet != null)
    {
      int instanceId = ((Object) rig).GetInstanceID();
      if (!CosmeticNotifierPlugin.NotifiedRigInstanceIds.Contains(instanceId))
      {
        CosmeticsController.CosmeticSet cosmeticSet = rig.cosmeticSet;
        HashSet<string> temporaryCosmetics = rig.TemporaryCosmetics;
        HashSet<string> stringSet = VRRig_OnEnable_Patch._ownedCosmeticsField?.GetValue((object) rig) as HashSet<string>;
        List<string> values = new List<string>();
        if (cosmeticSet.items != null)
        {
          foreach (CosmeticsController.CosmeticItem cosmeticItem in cosmeticSet.items)
          {
            string str;
            if (!cosmeticItem.isNullItem && CosmeticNotifierPlugin.WatchedCosmetics.TryGetValue(cosmeticItem.itemName, out str) && (!cosmeticItem.canTryOn || stringSet != null && stringSet.Contains(cosmeticItem.itemName) ? 1 : (temporaryCosmetics == null ? 0 : (temporaryCosmetics.Contains(cosmeticItem.itemName) ? 1 : 0))) != 0)
              values.Add(str);
          }
        }
        if (values.Count != 0)
        {
          CosmeticNotifierPlugin.NotifiedRigInstanceIds.Add(instanceId);
          string str1 = "A player";
          NetPlayer creator = rig.Creator;
          if (creator != null && !string.IsNullOrEmpty(creator.NickName))
            str1 = creator.NickName;
          string str2 = string.Join(", ", (IEnumerable<string>) values);
          NotificationLib.SendNotification($"{str1} joined wearing: {str2}", true, 6f);
        }
      }
    }
  }
}
