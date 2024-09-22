using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteoritos : MonoBehaviour
{

    public float spawnRate = 45f;
    public float dificultad = 1f;

    private float spawnNext = 0f;
    
    public float sumaDificultad = 3f;

    public float borde = 7.5f;

    public float probabilidadRick;

    public AudioSource audioSource1;
    public AudioSource audioSource2;

    // Update is called once per frame

    void Update()
    {
        if(Time.time>spawnNext){
            spawnNext = Time.time + 60 / (spawnRate + dificultad);
            if(Player.score % 10 == 0){
                dificultad += sumaDificultad;
                }
            
            var spawnPoint = new Vector2(Random.Range(-borde,borde),7.5f);
            //En vez de instanciar directamente uno nuevo, uso la clase ObjectPooler y utilizo un GameObject ya creado
            if(Random.value < probabilidadRick){
                ObjectPooler.Instance.SpawnFromPool("Rick",spawnPoint,Quaternion.identity);
            }else{
                ObjectPooler.Instance.SpawnFromPool("Meteorito",spawnPoint,Quaternion.identity);
                }
            }
        
    }
    
    //crea dos meteoritos mas pequeños
    public void DividirMeteorito(Vector3 position){
        ObjectPooler.Instance.SpawnFromPool("MeteoritoChikito", position + new Vector3(Random.Range(-1.5f, 1.5f), 0f,0f), transform.rotation);
        ObjectPooler.Instance.SpawnFromPool("MeteoritoChikito", position + new Vector3(Random.Range(1.5f, 1.5f) , 0f,0f)
         + new Vector3(Random.Range(1.5f, 1.5f) , 0f,0f), transform.rotation);
    }


    //lo mismo de arriba pero con rick
    public void DividirRick(Vector3 position){
        ObjectPooler.Instance.SpawnFromPool("RickChikito", position + new Vector3(Random.Range(-1.5f, 1.5f), 0f,0f), transform.rotation);
        ObjectPooler.Instance.SpawnFromPool("RickChikito", position + new Vector3(Random.Range(1.5f, 1.5f), 0f,0f)
         + new Vector3(Random.Range(1.5f, 1.5f) , 0f,0f), transform.rotation);
    }    

    //compara las tags del objeto y reproduce el sonido correspondiente
    public void PlaySoundMeteorito()
    {
            audioSource2.Play();
    }

    public void PlaySoundRick()
    {
            audioSource1.Play();
    }
}
