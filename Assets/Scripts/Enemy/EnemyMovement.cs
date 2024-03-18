using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Creamos el gameobject privado del enemigo por que lo instanciamos desde codigo al ser un prefab, en la etiqueta del player cambiamos por la etiqueta player
    GameObject player;

    NavMeshAgent agent;//Preferencia etiqueta agent

    Animator anim;//para modelar las animaciones

    EnemyHealth enemyHealth;
    void Start()
    {
        //Busca entre todos los gameobject con la etiqueta player 
        player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();

        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si player no es nulo  puedes seguir al jugador
        if(player !=null && enemyHealth.isDead == false)
        {
            agent.SetDestination(player.transform.position);
        }

        Animating();
    }
    void Animating()
    {
        if (agent.velocity.magnitude !=0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

    }

}
