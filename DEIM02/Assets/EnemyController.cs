using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;


    public bool sePuedeMover;



    void Start()
    {
        sePuedeMover = false;
    }

    // Update is called once per frame
    void Update()
    {

        EnemyMovement();
        
        
    }

    private void EnemyMovement()
    {


        if (sePuedeMover)
        {

            agent.SetDestination(player.position);
            
            agent.isStopped = false;

            if (agent.isOnOffMeshLink)
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
        else
        {
            agent.isStopped = true;
        }
       
        
    }
}
