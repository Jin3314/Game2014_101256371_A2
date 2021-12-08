using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinControl : MonoBehaviour
{
    public static PlayerSkinControl Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerSkinControl>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerSkinControl");
                    instance = instanceContainer.AddComponent<PlayerSkinControl>();
                }
            }
            return instance;
        }
    }
    private static PlayerSkinControl instance;

    public PlayerSkin[] PlayerSkins;  //Script

    public void SkinSetTrigger(string AnimName)
    {
        foreach (PlayerSkin skin in PlayerSkins)
        {
            skin.anim.SetTrigger(AnimName);
        }
    }
}