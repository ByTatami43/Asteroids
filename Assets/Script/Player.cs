using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    public static int score = 0;
    private Rigidbody rigid;

    //Parametros nave
    public float fEmpuje = 100f; //thrustForce
    public float rSpeed = 120f; //rotationSpeed

    public int vida = 3;
    // Cañon de meteoritos
    public GameObject canonDeMeteoritos,balaBill;

    public AudioSource audioSource;

    // Configuracion inicial de la nave
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        if(newPos.x > 7.5f){
            newPos.x = -7.5f+1;
        }
        else if(newPos.x < -7.5f){
            newPos.x = 7.5f-1;
        }
        else if(newPos.y > 7.5f){
            newPos.y = -7.5f+1;
        }
        else if(newPos.y < -7.5f){
            newPos.y = 7.5f-1;
        }
        transform.position = newPos;


        //Generar movimiento vertical
        float empuje =Input.GetAxis("Vertical") * Time.deltaTime; //Get axis nos permite leer el teclado, mediante etiquetas
        //Deltatime permite generar "igualdad" en el timing independientemente de los fps
        Vector3 eDireccion = transform.right; //thrustDirection
        rigid.AddForce(eDireccion * empuje * fEmpuje);

        //Generar movimiento rotatorio
        float rotacion =Input.GetAxis("Horizontal") * Time.deltaTime; 
        transform.Rotate(Vector3.forward,(-1) * rotacion * rSpeed); //rotateDirection

        //Disparo
        if(Input.GetKeyDown(KeyCode.Space)){
            //En vez de instanciar directamente uno nuevo, uso la clase ObjectPooler y utilizo un GameObject ya creado
            GameObject bala = ObjectPooler.Instance.SpawnFromPool("Bala",canonDeMeteoritos.transform.position,Quaternion.identity);
            //Para generar una bala (o cualquier elemento), tengo que describir el prefab (balaBill), donde aparece (canonDeMeteoritos posicion)
            //y por ultimo la rotacion aplicada al elemento ??
            audioSource.Play();
            LaBALA balaScript = bala.GetComponent<LaBALA>();
            balaScript.targetVector = transform.right; //Le cambiamos la rotacion al elemento en base a la rotacion propia de la nave
        }
    }


    //Se activa cuando la zona de colisión del meteorito entra en contacto con la zona de colisión de la nave se   
    private void OnTriggerEnter(Collider chocasion){
        if(chocasion.gameObject.CompareTag("Meteorito")||chocasion.gameObject.CompareTag("Rick")||
        chocasion.gameObject.CompareTag("MeteoritoChikito")||chocasion.gameObject.CompareTag("RickChikito"))
        {  
            if(vida > 0){
                vida--;
                //animación de los corazones
                HealthManager healthManager = FindObjectOfType<HealthManager>();
                //if para que no haya IndexOutOfBounds en el array de sprites de vida
                if(healthManager.perderVida()){
                    healthManager.animateSprite();
                }//fin animación

                //"Elimina el meteorito"
                chocasion.gameObject.SetActive(false);
                //reinicia sprite de vida, escena y puntuación
                if(vida == 0){
                    score = 0;
                    healthManager.currentSprite = 0;
                    healthManager.animateSprite();
                    (FindObjectOfType<SistemaPausa>()).gameOver();
                }
            }
        }
    }

}
