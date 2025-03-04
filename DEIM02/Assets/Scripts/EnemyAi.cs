using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;
using System.IO;
/*
public class WalkingEnemyAI : MonoBehaviour
{
    // Referencia a la vida del enemigo
    private EnemyController enemyController;

    [SerializeField] private ParticleSystem particles;

    // Referencia al agente
  private AIPath pathAgent;

    // Referencia al LivesController del jugador
    private LivesController playerLivesController;

    private Transform playerTrf;

    [SerializeField] private float followRange;
    [SerializeField] private LayerMask followLayerMask;

    // Clase enum
    public enum EnemyState { Iddle, Move, Follow, Attack, Dead }

    private EnemyState state;

    // Declarar la variable Animator
    private Animator animator;

    // Referencia al SpriteRenderer para hacer el flip
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerTrf = GameObject.Find("Player").GetComponent<Transform>();
        pathAgent = GetComponent<AIPath>();
        animator = GetComponent<Animator>(); // Obtener el Animator
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el SpriteRenderer
    }

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        state = EnemyState.Follow;
        // Asignar el LivesController del jugador
        playerLivesController = playerTrf.GetComponent<LivesController>();
    }

    private void Update()
    {
        // Si el estado no es muerto
        if (state != EnemyState.Dead)
        {
            // Comprobamos el valor de la vida del enemigo
            if (enemyController.enemyLives <= 0)
            {
                GoToDead();
            }
            else
            {
                switch (state)
                {
                    case EnemyState.Iddle:
                        if (InFollowRange())
                        {
                            GoToFollow();
                        }
                        else
                        {
                            GoToIddle();
                        }
                        break;

                    case EnemyState.Move:
                        // En caso de hacer que patrulle
                        break;

                    case EnemyState.Follow:
                        if (!InFollowRange())
                        {
                            GoToIddle();
                        }
                        else if (InAttackRange())
                        {
                            GoToAttack();
                        }
                        else
                        {
                            // Actualiza en cada frame diciendo que el destino es la posición del jugador
                            pathAgent.destination = playerTrf.position;
                            animator.SetBool("isMoving", true); // Activar animación de movimiento

                            // Gira el enemigo hacia el jugador (con flipX)
                            FlipSprite();
                        }
                        break;

                    case EnemyState.Attack:
                        if (!InAttackRange())
                        {
                            GoToFollow();
                        }
                        break;
                }
            }
        }
        else
        {
            animator.SetTrigger("Die"); // Cambiar a animación de muerte al entrar en estado muerto
        }
    }

    private void GoToDead()
    {
        state = EnemyState.Dead;
        enemyController.Morir();
        pathAgent.canMove = false; // Detener el movimiento al morir
    }

    private void GoToIddle()
    {
        state = EnemyState.Iddle;
        particles.Play();
        pathAgent.canMove = false;
        animator.SetBool("isMoving", false); // Desactivar animación de movimiento
        animator.SetBool("Attack", false); // Activar animación de ataque
    }

    private void GoToAttack()
    {
        state = EnemyState.Attack;
        pathAgent.canMove = true;
        animator.SetBool("Attack", true); // Activar animación de ataque
        animator.SetBool("isMoving", false); // Desactivar animación de movimiento
    }

    private void GoToFollow()
    {
        state = EnemyState.Follow;
        pathAgent.canMove = true;
        pathAgent.destination = playerTrf.position;
        animator.SetBool("isMoving", true); // Desactivar animación de movimiento
        animator.SetBool("Attack", false); // Activar animación de ataque
    }

    private bool InFollowRange()
    {
        // Comprobamos con qué choca, la posición del jugador - la posición del enemigo es igual al vector de la dirección del raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTrf.position - transform.position, followRange, followLayerMask);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private bool InAttackRange()
    {
        // Calcular la dirección hacia el jugador
        Vector2 directionToPlayer = playerTrf.position - transform.position;

        // Realizar el raycast hacia el jugador
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, 2f, followLayerMask);

        // Verificar si el raycast colisiona con el jugador
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private void Attack()
    {
        void OnCollisionEnter2D(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && state == EnemyState.Attack)
            {
                playerLivesController.EnemyDamage(1); // Llamar al método Attack solo si está atacando
            }
            else
            {
                GoToFollow();
            }
        }

        // Volver al estado de seguir después de atacar
        GoToFollow();
    }

    // Método para hacer flip del sprite dependiendo de la posición del jugador
    private void FlipSprite()
    {
        // Determina si el jugador está a la izquierda o derecha del enemigo
        if (playerTrf.position.x < transform.position.x)
        {
            // Si el jugador está a la izquierda, voltea el sprite
            spriteRenderer.flipX = true;
        }
        else
        {
            // Si el jugador está a la derecha, no lo voltea
            spriteRenderer.flipX = false;
        }
    }
}
   */