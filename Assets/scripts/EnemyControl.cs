using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyControl : MonoBehaviour
{
    public float lookRadius = 10f;
    public float attackRadies = 5f;

    
    public GameObject shell;
    public Transform fireBase;

    Transform target;
    NavMeshAgent agent;
    

    float distance;
    


    public float timeBetweenShoot;
    

    bool alreadyAttack = false;
    bool lookTo = false;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        Invoke("EnemyStart", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.enabled == true)
        {
            distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {


                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget(5f);
                }
                if (distance <= attackRadies)
                {
                    Attack();

                }
            }
            else
            {
                lookTo = false;
            }
        }
        if(!PlayerManager.instance.Player.GetComponent<Health>().alive)
        {
            agent.enabled = false;
        }
        

    }
    void EnemyStart()
    {
        agent.enabled = true;
    }
    void FaceTarget(float rotataSpeed)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Debug.Log(direction);
        Quaternion lookRotation;
        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotataSpeed);
    }
    void Attack()
    {
        if(!lookTo)
        {
            transform.LookAt(target);
            lookTo = true;
        }
        else
        {
            FaceTarget(5f);
        }
        if(!alreadyAttack)
        {
            alreadyAttack = true;
            GameObject TempShell = Instantiate(shell, fireBase.position, fireBase.rotation);
            TempShell.GetComponent<ShellExplode>().isEnemyFire = true;
            TempShell.GetComponent<Rigidbody>().AddForce((PlayerManager.instance.Player.transform .position
                - transform.position).normalized *20,ForceMode.Impulse);

           
            Invoke(nameof(ResetAttack), timeBetweenShoot);

        }
    }
    

    void ResetAttack()
    {
        alreadyAttack = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);

    }

    
}
