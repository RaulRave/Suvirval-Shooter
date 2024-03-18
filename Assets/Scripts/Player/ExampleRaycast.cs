using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleRaycast : MonoBehaviour
{

    public GameObject _object;

    public float rayLength; //Longitud del rayo

    Ray ray;//el rayo que apunta

    RaycastHit hit;//Donde se va a guardar informacion del choque del rayo
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Configuracion del rayo de apuntar
        ray.origin = transform.position;

        ray.direction = transform.forward;

        //Lanzando el rayo
        if(Physics.Raycast(ray, out hit, rayLength))
        {
            _object = hit.collider.gameObject;
            Debug.Log("Estoy chocando con algo " + hit.collider.name);
        }

        else
        {
            Debug.Log("No estoy chocando con nada");
        }

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
    }
}


