using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkinChangeBtn : MonoBehaviour
{
    public PlayerAction playerAction;   //추가

    public void ChangeWeaponButton(int index)
    {
        playerAction.EquipWeapon(index);
        if (index == -1)
        {
            PlayerSkinControl.Instance.PlayerSkins[0].EquipItem(null);
            PlayerSkinControl.Instance.PlayerSkins[0].gameObject.SetActive(false);// 추가
        }
        else
        {
            PlayerSkinControl.Instance.PlayerSkins[0].gameObject.SetActive(true);// 추가
            PlayerSkinControl.Instance.PlayerSkins[0].EquipItem(SkinMgr.Instance.WeaponArrays[index].AnimClips);
        }
    }
    public void ChangeArmorButton(int index)
    {
        if (index == -1)
        {
            PlayerSkinControl.Instance.PlayerSkins[1].EquipItem(null);
            PlayerSkinControl.Instance.PlayerSkins[1].gameObject.SetActive(false);
        }
        else
        {
            PlayerSkinControl.Instance.PlayerSkins[1].gameObject.SetActive(true);
            PlayerSkinControl.Instance.PlayerSkins[1].EquipItem(SkinMgr.Instance.ArmorArrays[index].AnimClips);
        }
    }
    public void ChangeHelmetButton(int index)
    {
        if (index == -1)
        {
            PlayerSkinControl.Instance.PlayerSkins[2].EquipItem(null);
            PlayerSkinControl.Instance.PlayerSkins[2].gameObject.SetActive(false);
        }
        else
        {
            PlayerSkinControl.Instance.PlayerSkins[2].gameObject.SetActive(true);
            PlayerSkinControl.Instance.PlayerSkins[2].EquipItem(SkinMgr.Instance.HelmetArrays[index].AnimClips);
        }
    }
    public void ChangeShieldButton(int index)
    {
        if (index == -1)
        {
            playerAction.EquipShield(false);
            PlayerSkinControl.Instance.PlayerSkins[3].EquipItem(null);
            PlayerSkinControl.Instance.PlayerSkins[3].gameObject.SetActive(false);
        }
        else
        {
            playerAction.EquipShield(true);
            PlayerSkinControl.Instance.PlayerSkins[3].gameObject.SetActive(true);
            PlayerSkinControl.Instance.PlayerSkins[3].EquipItem(SkinMgr.Instance.ShieldArrays[index].AnimClips);
        }
    }
    public void ChangeShoeButton(int index)
    {
        playerAction.EquipShoe(index);
        if (index == -1)
        {
            PlayerSkinControl.Instance.PlayerSkins[4].EquipItem(null);
            PlayerSkinControl.Instance.PlayerSkins[4].gameObject.SetActive(false);
        }
        else
        {
            PlayerSkinControl.Instance.PlayerSkins[4].gameObject.SetActive(true);
            PlayerSkinControl.Instance.PlayerSkins[4].EquipItem(SkinMgr.Instance.ShoeArrays[index].AnimClips);
        }
    }
}