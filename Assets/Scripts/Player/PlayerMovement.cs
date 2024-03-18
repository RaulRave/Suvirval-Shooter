using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
    // Movimiento
{
    public int speed;

    public LayerMask layerFloor;//Capa donde va estar el suelo de la escena
    
    //Movimiento a traves e las fisicas
    Rigidbody rb;

    //Animar
    Animator anim;

    //Vamos a guardar la direccion de movimiento para guirar con el raton
    Vector3 movement;
    float horizontal;
    float vertical;

    public float rangoX = 16.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputPlayer();
    }

    void FixedUpdate()
    {
        Move();

        Turning();

        Animating();
    }
    void InputPlayer()
    {
        
        horizontal = Input.GetAxis("Horizontal");

        vertical = Input.GetAxis("Vertical");
    }

    //mover mediante fisica
    void Move()
    {
        movement = new Vector3(horizontal, 0, vertical);//direccion de movimiento a traves de los imputs

        movement.Normalize();//Normalizare el vector, es decir su modulo(longitud) vale 1 

        rb.MovePosition(transform.position + (movement * speed * Time.deltaTime));

     

    }

    //Movimiento raton
    void Turning()
    {
        //Raycast que va desde el curso del raton en coordenadas de pantalla y con direccion hacia la escena
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerFloor))
        {
            Vector3 playerToMouse = hit.point - transform.position; //Vector morado imagen

            playerToMouse.y = 0;

            //Calculamos la rotacion 
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //Aplicamos la rotacion es decir rotar el objeto
            rb.MoveRotation(newRotation);
        }

        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
    }

    //Para manejar las animaciones
    void Animating()
    {
        //Si horizontal es distinto de 0 o vertical distotno de 0
        if(horizontal !=0 || vertical !=0)
        {
            anim.SetBool("IsMoving", true);
        }

        // Si se mete aqui significa que horizontal y vertical son ambos 0
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}

