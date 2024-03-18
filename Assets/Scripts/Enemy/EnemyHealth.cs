using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;//Maxima salud del enemigo

    public int currentHealt;//Salud actual

    public float sinkSpeed;//Velocidad de desaparicion del enemigo este es hundimiento

    public int scoreValue;//Los puntos que va a dar al juggador una vez  el enemigo sea destruido

    public bool isDead;//Nos diga si esta muerto o no

    public AudioClip deathClip;

    AudioSource audioS;

    public ParticleSystem hitParticles;

    Animator anim;//Variable privada referente al animator

    bool isSinking;//Para saber si el enemigo "Se esta hundiendo"
    void Start()
    {
        //La salud inicial es la salud maxima
        currentHealt = maxHealth;

        anim = GetComponent<Animator>();//Para hacer la referencia al componente animator

        audioS = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isSinking == true) //Si isSinking es igual igual a true hazme un transfor tralaete hacia abajo 
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    //Funcion publica por que voy a llamar a esta desde el script de disparo del player
    public void TakeDamage(int amount, Vector3 point)
    {
        //Si el valor de la booleana isDead es true, me salgo de la funcion 
        if (isDead) return;

        currentHealt -= amount;
        //currentHealt = currentHealt - amount;

        audioS.Play();

        //Situo el sistema de particulas de impacto del Raycast con el enemigo
        hitParticles.transform.position = point;

        hitParticles.Play();


        if (currentHealt <= 0) Death();//Si la vida es menor o igual a Cero gestiono la muerte del enemigo
    }

    void Death()
    {
        audioS.clip = deathClip;

        audioS.Play();

        isDead = true;

        anim.SetTrigger("Death");//Ejecute la animacion de muerte

        // Destroy(gameObject);//Para comprobar que funciona

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ScoreEnemy(scoreValue);
    }

    //Metodo publico que voy a llamar desde la animacion de Death
    public void StartSinking()
    {
        isSinking = true;

        //GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;//Desahabilito el componente NavMeshagent del enemigo

        GetComponent<NavMeshAgent>().enabled = false;

        Destroy(gameObject, 2);//en 2 segundos
    }
}
