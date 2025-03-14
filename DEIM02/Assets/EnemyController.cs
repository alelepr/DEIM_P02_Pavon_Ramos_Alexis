using System;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerController; // Referencia al PlayerController
    [SerializeField] public Animator anim;
    





    public bool sePuedeMover;

    void Start()
    {
        sePuedeMover = false; // El NPC no se mueve inicialmente
        anim = GetComponent<Animator>();
        



    }

    void Update()
    {
        // Comprobar si el panel del jugador ha sido cerrado
        if (playerController.npcPuedeMoverse)
        {
            sePuedeMover = true; // Permitir que el NPC se mueva si el panel ha sido cerrado
            
        }

               

        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (sePuedeMover)
        {
            // El NPC puede moverse hacia el jugador
            agent.SetDestination(player.position);
            agent.isStopped = false;
            anim.SetBool("moving", true);
            agent.speed = 5;

            // Lógica para las animaciones de subir o bajar si el NPC está sobre un OffMeshLink
            if (agent.isOnOffMeshLink)
            {
                if (agent.velocity.y > 0)
                {
                    Debug.Log("Subiendo");
                    // Animación de subir
                }
                else
                {
                    Debug.Log("Bajando");
                    // Animación de bajar
                }
            }
        }
        else
        {
            // El NPC no puede moverse hasta que el panel esté cerrado
            agent.isStopped = true;
            anim.SetBool("moving", false);
            agent.speed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ParadaNPC"))
        {
            agent.isStopped = true;
            anim.SetBool("moving", false);
            agent.speed = 0;
        }
        else
        {
            
            agent.SetDestination(player.position);
            agent.isStopped = false;
            anim.SetBool("moving", true);
            agent.speed = 5;

        }
    }
}
