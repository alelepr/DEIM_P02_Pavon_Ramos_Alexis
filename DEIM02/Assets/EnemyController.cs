using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerController; // Referencia al PlayerController

    public bool sePuedeMover;

    void Start()
    {
        sePuedeMover = false; // El NPC no se mueve inicialmente
    }

    void Update()
    {
        // Comprobar si el panel del jugador ha sido cerrado
        if (playerController.panelCerrado)
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

            // L�gica para las animaciones de subir o bajar si el NPC est� sobre un OffMeshLink
            if (agent.isOnOffMeshLink)
            {
                if (agent.velocity.y > 0)
                {
                    Debug.Log("Subiendo");
                    // Animaci�n de subir
                }
                else
                {
                    Debug.Log("Bajando");
                    // Animaci�n de bajar
                }
            }
        }
        else
        {
            // El NPC no puede moverse hasta que el panel est� cerrado
            agent.isStopped = true;
        }
    }
}
