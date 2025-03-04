using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        if(Input.GetKey(KeyCode.Y))
        {
            agent.speed = 3;

        }
        else
        { 
        
            agent.speed = 1;
            //agent.isStopped = true;
        }

        if(agent.isOnOffMeshLink)
        {
            if (agent.velocity.y > 0)
            {
                Debug.Log("Subiendo");
                //animacion de subir

            }
            else
            {
                Debug.Log("Bajando");
                //animacion de bajar
            }
        }
        
    }
}
