using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damePerShot;//Daño por disparo que va hacer el jugador

    public float timeBetweenBullets;//Tiempo entre disparos

    public float range;//Longitud del raycast. que significa hasta que distancia puede disparar el player

    public LayerMask shootableMask;//Capa de objetos a la que vamos a poder disparar

    float timer;//Variabble que voy a usar de contador de tiempo

    Ray ray;

    RaycastHit hit;

    LineRenderer lineRenderer;

    Light gunLigth;

    AudioSource audioS;

    float effectsDisplayTime = 0.2f; //Variable que va a determinar  cuanto tiempo van a estar los efectos en pantalla
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        gunLigth = GetComponent<Light>();

        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;//Contador de tiempo

       if(Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets)
        {
            Shoot();
        }

       //Desabilidar los efectos
       if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            lineRenderer.enabled = false;

            gunLigth.enabled = false;
        }
    }
        
    void Shoot()
    {

        timer = 0;

        audioS.Play();

        //Habilitamos el componente LineRenderer
        lineRenderer.enabled = true;

        gunLigth.enabled = true;

        //Establezco el primer punto del LineRenderer
        lineRenderer.SetPosition(0, transform.position);

        ray.origin = transform.position;

        ray.direction = transform.forward;

        if(Physics.Raycast(ray, out hit, range, shootableMask))
        {
            //Voy a quitarle vida al enemigo

            //Me guardo en una variable (local) el gameobject con el que estoy chocando

            GameObject _object = hit.collider.gameObject;

            //Commpruebo si ese gameobject es el enemigo

            if(_object.GetComponent<EnemyHealth>())
            {
                _object.GetComponent<EnemyHealth>().TakeDamage(damePerShot, hit.point);
            }

            lineRenderer.SetPosition(1, hit.point);
        }

        else
        {
            //Estoy estableciendo el segundo punto del lineRenderer a una distacia range desde el origen del raycast
            lineRenderer.SetPosition(1, ray.origin + (ray.direction * range));
        }
    }
}

