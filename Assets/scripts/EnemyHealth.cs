using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    public int maxhealth;
    public int health;
    public Transform pivot;
    public ParticleSystem explode;
    bool once;
    // Update is called once per frame
    private void Start()
    {
        once = true;
    }
    void Update()
    {
        healthBar.fillAmount = (float)health / (float)maxhealth;

        pivot.LookAt(Camera.main.transform.position);
        if(health <= 0 && once)
        {
            once = false;
            Died();
        }
    }
    void Died()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<EnemyControl>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        ParticleSystem temp = Instantiate(explode, transform.position, transform.rotation);
        temp.transform.SetParent(gameObject.transform);
        Destroy(gameObject, 1.5f);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
