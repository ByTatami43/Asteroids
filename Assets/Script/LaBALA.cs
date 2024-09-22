using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LaBALA : MonoBehaviour
{
    public float speed = 10f;
    public float vida = 3f;//Se puede borrar?
    public Vector3 targetVector;
    public int puntos = 0;

    // Update is called once per frame
    void Update(){
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Meteorito"))
        {
            IncreaseScore();
            //GameObject.Find busca Main Camera porque ahi es donde esta el script Meteoritos
            //toca el sonido correspondiente
            GameObject.Find("Main Camera").GetComponent<Meteoritos>().PlaySoundMeteorito();
            //Hace aparecer dos meteoritos mas pequeños en la posicion del grande
            GameObject.Find("Main Camera").GetComponent<Meteoritos>().DividirMeteorito(collision.transform.position);
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            UpdateText();
        }
        if(collision.gameObject.CompareTag("Rick")) //seguramente haya una forma mas correcta de hacer esto pero son las 3 de la mañana y no se me ocurre
        {
            IncreaseScore();
            GameObject.Find("Main Camera").GetComponent<Meteoritos>().PlaySoundRick();
            GameObject.Find("Main Camera").GetComponent<Meteoritos>().DividirRick(collision.transform.position);
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            UpdateText();
        }
        if(collision.gameObject.CompareTag("MeteoritoChikito"))
        {
            GameObject.Find("Main Camera").GetComponent<Meteoritos>().PlaySoundMeteorito();
            IncreaseScore();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            UpdateText();
        }
        if(collision.gameObject.CompareTag("RickChikito"))
        {
            GameObject.Find("Main Camera").GetComponent<Meteoritos>().PlaySoundRick();
            IncreaseScore();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            UpdateText();
        }
    }


    private void IncreaseScore(){
        Player.score++;
    }

    private void UpdateText(){
        GameObject texto = GameObject.Find("Puntuacion");
        texto.GetComponent<UnityEngine.UI.Text>().text = "Puntos: " + Player.score;
    }

}
