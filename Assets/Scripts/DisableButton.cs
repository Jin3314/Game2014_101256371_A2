using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButton : MonoBehaviour
{
    public GameObject InventoryButton;
    public GameObject WeaponCanvas;

    // Start is called before the first frame update
    void Start()
    {
     
        WeaponCanvas = GameObject.Find("WeaponCanvas");
        WeaponCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOff()
    {
        if (WeaponCanvas.activeSelf == true)
        {
            WeaponCanvas.SetActive(false);
        }
        else if (WeaponCanvas.activeSelf == false)
        {
            WeaponCanvas.SetActive(true);
        }
       
      

    }

}

