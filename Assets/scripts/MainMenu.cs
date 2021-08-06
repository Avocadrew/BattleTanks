using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        fadeAnimator.SetTrigger("Fade In");
    }
    public void PlayParty()
    {
        SceneManager.LoadScene("FightScene");
       
    }
    public void PlayRace()
    {
        Application.Quit();
    }
    
}
