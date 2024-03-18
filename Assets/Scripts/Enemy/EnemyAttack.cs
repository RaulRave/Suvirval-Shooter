using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Daño por acercamiento

    public float timeBetweenAttacks;//Limitar el tiempo de ataque

    public int attackDamage;//Cuanto daño le quitamos al Player

    GameObject player;

    PlayerHealth playerHealth;

    EnemyHealth enemyHealth;

    bool playerInRange;//Si el Player esta en el rango para atacar

    float timer;//Contador de tiempo
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerHealth = player.GetComponent<PlayerHealth>();

        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Colision Trigger
    private void OnTriggerEnter(Collider other)
    {
        //Si ese otro colaider que estoy tocando es el player
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Para saber si tenemos al player dentro de mi rango de ataque o no
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;//Contador de tiempo para controlar cada cuanto tiempo ataco al jugador
        
        //Tiempo entre ataques y player esta en rango y el enemigo no esta muerto para si esta desapareciendo cruzemos y nos quiten vida

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.isDead == false)
        {
            Attack();//Ataco
        }
    }

    //Funcion de referencia al Script PlayerHealth
    void Attack()
    {
        timer = 0;//Reseteamos el tiempo

        playerHealth.TakeDamage(attackDamage);//Quitar vida
    }
}
