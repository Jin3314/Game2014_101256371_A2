using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    Color srColor;
    SpriteRenderer sr;
    public Animator anim;
    public AnimatorOverrideController aoc;

    void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        srColor = sr.color;
        srColor.a = 0;
        sr.color = srColor;
    }
    public void EquipItem(AnimationClip[] clips)
    {
        if (clips != null)
        {
            sr.color = Color.white;
            aoc["Attack"] = clips[0];
            aoc["Dmg"] = clips[1];
            aoc["Idle"] = clips[2];
            aoc["Jump"] = clips[3];
            aoc["Walk"] = clips[4];
        }
        else
        {
            srColor.a = 0;
            sr.color = srColor;
            aoc["Attack"] = null;
            aoc["Dmg"] = null;
            aoc["Idle"] = null;
            aoc["Jump"] = null;
            aoc["Walk"] = null;
        }
    }
}