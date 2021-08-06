using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFalling : MonoBehaviour
{
    public GameObject prefab;
    float timer_float = 0f;
    bool haveTriggered = false;
    int timer_int = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer_float = 0f;
        haveTriggered = false;
        timer_int = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer_float += Time.deltaTime;
        timer_int = (int)timer_float;
        if (timer_int % 15 == 14 && haveTriggered == false)
        {
            haveTriggered = true;
            for (int i = 0; i < 7; i++)
            {
                
                var RandomXpos = Random.Range(84.0f, 247.0f);
                var RandomZpos = Random.Range(81.0f, 216.0f);
                Vector3 Pos = new Vector3(RandomXpos, 100.0f, RandomZpos); ;
                //Debug.Log(Pos);
                //SpawnLocation.position = Pos;
                //Debug.Log(SpawnLocation.position);
                var xSpin = Random.Range(0f, 360f);
                var ySpin = Random.Range(0f, 360f);
                var zSpin = Random.Range(0f, 360f);
                var wSpin = Random.Range(0f, 360f);
                Quaternion rotation = new Quaternion(xSpin, ySpin, zSpin, wSpin);
                GameObject.Instantiate(prefab, Pos, rotation);
                //Debug.Log("dropped");
            }
        }
        else if (timer_int % 15 != 14)
        {
            haveTriggered = false;
        }
        //Debug.Log(timer_int);
    }
}
