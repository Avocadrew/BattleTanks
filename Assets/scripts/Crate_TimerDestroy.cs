using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate_TimerDestroy : MonoBehaviour
{
    public GameObject player;
    public GameObject explode;
    public Transform ParticleEffectParent;
    float timer_float = 0f;
    bool haveTriggered = false;
    int timer_int = 0;
    public int destroyAfter = 20;
    // Update is called once per frame
    void Update()
    {
        timer_float += Time.deltaTime;
        timer_int = (int)timer_float;
        if (timer_int > destroyAfter && haveTriggered == false)
        {
            haveTriggered = true;
            GameObject explodeTemp = Instantiate(explode, transform.position, transform.rotation);
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            Destroy(explodeTemp, 3f);
            Destroy(gameObject,3f);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == PlayerManager.instance.Player)
        {
            GameObject explodeTemp = Instantiate(explode, transform.position, transform.rotation);
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            
            Destroy(explodeTemp, 3f);
            Destroy(gameObject, 3f);
            int function = Random.Range(0,2);
            switch (function)
            {
                case 0:
                    Global.Ammo += 1;
                    break;
                case 1:
                    collision.gameObject.GetComponent<Health>().isShield = true;
                    break;
            }
            
        }
        
      
    }

}
