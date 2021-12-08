using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMgr : MonoBehaviour
{
    public static SkinMgr Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SkinMgr>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("SkinMgr");
                    instance = instanceContainer.AddComponent<SkinMgr>();
                }
            }
            return instance;
        }
    }
    private static SkinMgr instance;

    [System.Serializable]
    public class WeaponAnimArray
    {
        public AnimationClip[] AnimClips;
    }
    public WeaponAnimArray[] WeaponArrays;

    [System.Serializable]
    public class ShoeAnimArray
    {
        public AnimationClip[] AnimClips;
    }
    public ShoeAnimArray[] ShoeArrays;

    [System.Serializable]
    public class ArmorAnimArray
    {
        public AnimationClip[] AnimClips;
    }
    public ArmorAnimArray[] ArmorArrays;

    [System.Serializable]
    public class HelmetAnimArray
    {
        public AnimationClip[] AnimClips;
    }
    public HelmetAnimArray[] HelmetArrays;

    [System.Serializable]
    public class ShieldAnimArray
    {
        public AnimationClip[] AnimClips;
    }
    public ShieldAnimArray[] ShieldArrays;
}