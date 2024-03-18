using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Paso 1
    //Salud del Personaje vamos GAME OBJECT UI SLIDER y trabajamos en el para ponerle la escala que queremos y luego anchar donde lo queremos
    //En Slider en componente VALUE para que se mueva la barra de de vida de derecha a izquierda MAX VALUE es el nivel maximo de vida que podemos ponerle
    //Paso 2
    //Crear una imagen de daño que se ponga en rojo como que recibes daño
    //En Canvas boton secundario UI IMAGEN  y le ponemos el nombrre DAMEGEIMAGE para que ocupe toda la pantalla pulsamos SHITH y ALT y le cambiamos el color a rojo
    //Añadimos el Script a Player
    //Paso 3
    //Configurar todo desde el editor
    //Vamos a Player  Maxima salud le ponemos la vida 100
    //Arrastramos el SLIDER dentro de Slideer y DAMAGEIMAGE en Damage Image
    //Flash Speed ponemos 1
    //Flas Colur ponemos el color y la trasnsparencia

    public int maxHealth;//Maxima salud del enemigo

    public int currentHealt;//Salud actual

    public Slider slider;//Para hacer referencia al Componente 

    public Image damegeImage;//Para meterle la imagen del daño recivido

    public float flashSpeed;//Va ser la velocidad en la que desaparezca la imagen del daño

    public Color flashcolour;//Va ser el color a mostrar

    public GameManager gameManager;

    public AudioClip deathClip;

    AudioSource audioS;

    Animator anim; //Para tener acceso a las animaciones

    //Para Gestionar la parte del GAME OVER que el personaje no pueda hacer nada
    PlayerMovement playerMovement;

    PlayerShooting playerShooting;

    bool isDead;//Si hemos muerto

    bool damaged;//Si hemos sido dañado


    void Start()
    {
        //La salud inicial es la salud maxima
        currentHealt = maxHealth;

        slider.maxValue = maxHealth;// La Barra de vida empieze con el nivel maximo

        slider.value = maxHealth;// Value es el valor actual que le hemos dicho en el editor

        audioS = GetComponent<AudioSource>();


        anim = GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();

        playerShooting = GetComponentInChildren<PlayerShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged == true)
        {
            damegeImage.color = flashcolour;
        }

        else
        {
            damegeImage.color = Color.Lerp(damegeImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    //Funcion publica que voy a llamar desde el script del enmigo
    public void TakeDamage(int amount)//Amount la cantidad de vida que nos va aquitar al jugador
    {
        if (isDead == true) return;//Si el player ha muerto se sale de la funcion

        audioS.Play();

        //el daño es verdad currrent la vida es menor o igual al daño que me han quitado y el valor del slider va ser igual a la salud actual del actual
        damaged = true;

        currentHealt -= amount;

        slider.value = currentHealt;

        if (currentHealt <= 0) Death();
    }

    void Death()
    {
        audioS.clip = deathClip;

        audioS.Play();

        isDead = true;

        anim.SetTrigger("Death");

        //Deshabilitar los componentes para que el player no pueda moverse ni disparar
        playerMovement.enabled = false;

        playerShooting.enabled = false;
    }

    //Funcion publica que va como evento en la animacion de Death
    public void DeathComplete()
    {
        gameManager.GameOver();
    }
}
