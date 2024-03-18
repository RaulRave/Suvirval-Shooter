using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Paso 1
    //Posiciones y Enemigos Aleatorios, Creamos un objeto vacio en escena boton secundario Create Empty y lo renombamos GameManager
    //y lo ponemos en el 0 0 0 y le añadimos el scrip
    //Creamos otro objeto vacio y le nombramos PosEnemies que sera el padre para colocar las posciones de los enemigos aletorios que se clonaran
    //dentro de posEnemies creamos un Gameobject vacio  y le ponemos nombre 1 y una vez puesto en la zona y con los ejes Control D para duplicar

    //Paso 2
    //despues de int pos = Random.Range(0, positions.Length); " " int enemy = Random.Range(0, enemyPrefab.Length); " " Instantiate(enemyPrefab[enemy], positions[pos].position, positions[pos].rotation); " "  InvokeRepeating("CreateEnemy", time, time);
    // Vamos a GameManager para poner las posiciones bloqueamos el candado del inspector.
    //Cogemos los objetos que meteremos en el array POSITIONS pulsamos en la primera tecla mayuscula en la ultima y arrastramos
    //Carpeta prefab los enemgos lo mimos pero en la array ENEMY PREFAB y cuando esten todo quitamos el candado
    //en Time ponemos el tiempo guardamos y play

    //Paso 3
    //Para una buena practica creamos otro GameObject para que los clones POSENEMY salgan de forma jerarquica le ponemos PARENTENEMIES
    //Vamos a Game Manager y en parent metemos el objeto PARENTENEMIES en PARENTENEMIES

    public GameObject panelGameOver;

    public TextMeshProUGUI textScore;

    public Transform[] positions;//Array de posiciones "Empty objects"

    public GameObject[] enemyPrefab;//Aqui van los prefabs de los enemigos que vamos a instanciar en posiciones aletorias y enemigos

    public Transform parentEnemies;//El game object vacio que va a ser el padre de los clones enemigos

    public float time;//Cada cuanto tiemmpo voy a estar instanciando enemigos

    int score;// Puntuacuion total
    void Start()
    {

        //Invocamos un metodo de repeticion de crear enemigos para que salgan y darles el tiempo de salida
        InvokeRepeating("CreateEnemy", time, time);
    }

    void CreateEnemy()
    {
        int pos = Random.Range(0, positions.Length);// rango 0 y la posicion de la array y colocacion

        int enemy = Random.Range(0, enemyPrefab.Length);//lo mismo perro para el prefab de enemigos

        GameObject cloneEnemy = Instantiate(enemyPrefab[enemy], positions[pos].position, positions[pos].rotation);//Instanciar un enemigo como es un array metemos en la casilla Enemy, en Array pos para que nos ponga un enemigo aleatorio y la ultima array para rotacion

        cloneEnemy.transform.SetParent(parentEnemies);//para guardar los clones en el gameobject
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
    }

    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
    
    //Funcion publica que vamos a llamar desde el script de salud del enemigo cuando este muera
    public void ScoreEnemy(int scoreValue)
    {
        score += scoreValue;

        textScore.text = "Score: " + score.ToString();
    }
}
