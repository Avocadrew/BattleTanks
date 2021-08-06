using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplode : MonoBehaviour
{
    public GameObject explode;
    GameObject temp;
    bool canExplode = true;
    public AudioSource source;
    public AudioClip explodeSound;
    public bool isEnemyFire;
    private void Update()
    {
        if(transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(canExplode)
        {
            temp = Instantiate(explode, transform.position, transform.rotation);
            source.PlayOneShot(explodeSound);
            canExplode = false;
            Destroy(temp.gameObject, 1f);
            Destroy(gameObject, 1f);
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
        if (collision.gameObject == PlayerManager.instance.Player
            && !PlayerManager.instance.Player.GetComponent<Health>().isShield)
        {
            PlayerManager.instance.Player.GetComponent<Health>().health -= 1;
        }
        else if(collision.gameObject.GetComponent<EnemyHealth>()!= null && !isEnemyFire)
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }
}
