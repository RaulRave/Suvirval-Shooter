using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;//Seguimiento al player que siga la camara

    public float smoothing;//Velocidad de seguimiento de la camara al player

    Vector3 offset;//Distancia inicial entre camara y player
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetCamPos = player.position + offset;//Calculo la posicion a la que quiero mover la camara

        //Lerp hace que interrpola linealmente  entre 2 puntos "Vamos a editor, Main Camera, ponemos el script y donde pone player metemos el player y podemos quitar los enemigos de la pantalla 
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
