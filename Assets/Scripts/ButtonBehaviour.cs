using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ButtonBehaviour.cs, YeongjinLim, 101256371, last Modified in 2021/11/20, Script for changing scene, revision history - #01 added button change function
public class ButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInstructionButtonPressed()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void OnGamePlayButtonPressed()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
