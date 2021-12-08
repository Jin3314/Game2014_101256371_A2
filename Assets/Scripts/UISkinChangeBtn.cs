using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkinChangeBtn : MonoBehaviour
{
    public void ChangeWeaponButton(int index)
    {
        if (index == -1)
        {
            PlayerSkinControl.Instance.PlayerSkins[0].EquipItem(null);
        }
        else
        {
            PlayerSkinControl.Instance.PlayerSkins[0].EquipItem(SkinMgr.Instance.WeaponArrays[index].AnimClips);
        }
    }
    public void ChangeArmorButton(int index)
    {
        if (index == -1)
        {
            PlayerSkinControl.Instance.PlayerSkins[1].EquipItem(null);
        }
        else
        {
            PlayerSkinControl.Instance.PlayerSkins[1].EquipItem(SkinMgr.Instance.ArmorArrays[index].AnimClips);
        }
    }
    public void ChangeHelmetButton(int index)
    {
        if (index == -1)
        {
            PlayerSkinControl.Instance.PlayerSkins[2].EquipItem(null);
        }
        else
        {
            PlayerSkinControl.Instance.PlayerSkins[2].EquipItem(SkinMgr.Instance.HelmetArrays[index].AnimClips);
        }
    }
    public void ChangeShieldButton(int index)
    {
        if (index == -1)
        {
            PlayerSkinControl.Instance.PlayerSkins[3].EquipItem(null);
        }
        else
        {
            PlayerSkinControl.Instance.PlayerSkins[3].EquipItem(SkinMgr.Instance.ShieldArrays[index].AnimClips);
        }
    }
    public void ChangeShoeButton(int index)
    {
        if (index == -1)
        {
            PlayerSkinControl.Instance.PlayerSkins[4].EquipItem(null);
        }
        else
        {
            PlayerSkinControl.Instance.PlayerSkins[4].EquipItem(SkinMgr.Instance.ShoeArrays[index].AnimClips);
        }
    }
}