using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public int health;
    public bool isShield;
    public GameObject shield;
    public Text endText;
    public GameObject Player;
    public GameObject explode;
    public RawImage[] heartUI;
    public bool alive;
    bool once;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        alive = true;
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShield)
        {
            shield.SetActive(true);
            Invoke("CancelShield", 10f);
        }
        for(int i = 0;i < 3;i++)
        {
            if(i < health)
            {
                heartUI[i].enabled = true;
            }
            else
            {
                heartUI[i].enabled = false;
            }
        }
        if(health == 0 && once)
        {
            once = false;
            EndOfGame();
        }
    }
    void CancelShield()
    {
        isShield = false;
        shield.SetActive(false);
    }
    void EndOfGame()
    {
        //let the timer stop
        GameObject.Find("GameManagementSystem").GetComponent<TimeManager>().end = true;
        alive = false;
        GameObject explodeTemp = Instantiate(explode, transform.position, transform.rotation);
        gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        //Destroy(explodeTemp, 3f);
        //Destroy(gameObject, 0f);
        Player.GetComponent<playerMovement>().enabled = false;
        Player.GetComponent<WeaponSystem>().enabled = false;
        endText.enabled = true;

    }
}
