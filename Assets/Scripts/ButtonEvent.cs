using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{

    GameObject Player;
    PlayerMovement player;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("PLAYER");
        player = Player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LeftBtnDown()
    {
        player.LeftMove = true;
    }
    public void LeftBtnUp()
    {
        player.LeftMove = false;
    }

    public void RightBtnDown()
    {
        player.RightMove = true;
    }

    public void RightBtnUp()
    {
        player.RightMove = false;
    }

    public void JumpDown()
    {
        player.Jump = true;
    }

    public void JumpUp()
    {
        player.Jump = false;
    }

    public void AttackDown()
    {
        player.Attack = true;
    }

    public void AttackUp()
    {
        player.Attack = false;
    }


}