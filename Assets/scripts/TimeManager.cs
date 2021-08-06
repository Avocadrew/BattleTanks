using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    double CountDownTime;
    double startTime;
    public AudioSource Music;
    public AudioClip countdown;
    public GameObject FallingCrate;
    public GameObject Player;
    public Text countText;
    public Text counter;
    public Text endText;
    public GameObject billBoard;
    int threeSec = 3;
    int timeLeft;
    bool startCount;
    public bool end;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Invoke("GameStart", 4f);
        startCount = false;
        CountDown();
        Initialize();
        end = false;
        endText.enabled = false;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("ChangeCountdownNumber", 1, 1);
        timeLeft = 180 - (int)(Time.time - CountDownTime);
        if (timeLeft == -1)
        {
            EndOfGame();
        }
        if(startCount && !end)
        {
            ChangeCounterNumber();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            billBoard.SetActive(!billBoard.activeSelf);
            if(billBoard.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }

    }
    public void EndOfGame()
    {
        end = true;
        
        Player.GetComponent<playerMovement>().enabled = false;
        Player.GetComponent<WeaponSystem>().enabled = false;
        endText.enabled = true;

    }
    void Initialize()
    {
        Player.GetComponent<playerMovement>().enabled = false;
        Player.GetComponent<WeaponSystem>().enabled = false;
        FallingCrate.SetActive(false);
        billBoard.SetActive(false);
    }
    void GameStart()
    {
        CountDownTime = Time.time;
        Music.Play();
        Player.GetComponent<playerMovement>().enabled = true;
        Player.GetComponent<WeaponSystem>().enabled = true;
        FallingCrate.SetActive(true);
        startCount = true;
    }
    void CountDown()
    {
        Music.PlayOneShot(countdown);
    }
    void ChangeCountdownNumber()
    {
        threeSec--;
        if (threeSec < 0)
        {
            countText.enabled = false;
            CancelInvoke("ChangeCountdownNumber");
        }
            countText.text = threeSec.ToString();
        CancelInvoke("ChangeCountdownNumber");
    }

    void ChangeCounterNumber()
    {
        int minute = timeLeft / 60;
        int second = timeLeft - minute * 60;
        if(second > 10)
        {
            counter.text = minute.ToString() + ":" + second.ToString();
        }
        else
        {
            counter.text = minute.ToString() + ":" + "0" + second.ToString();
        }
    }
}
