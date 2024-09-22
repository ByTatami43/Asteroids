using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    //System.Serializable permite que la clase sea visible en el inspector
    [System.Serializable]
    //La clase sirve para crear un pool de objetos
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    //Lista de pools
    public List<Pool> pools;
    //Dictionary es una estructura de datos que permite almacenar datos en pares clave-valor
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    //Creo un singleton para poder acceder a la clase desde cualquier script,
    //garantiza que tan solo exista un objeto de su tipo y proporciona un único punto de acceso a él para cualquier otro código
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {  
        //Creo un diccionario de pools
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    //Accedo a cada pool y creo una cola de objetos
        foreach (Pool pool in pools)
        {
            //Creo una cola de objetos
            Queue<GameObject> objectPool = new Queue<GameObject>();
            //Por cada pool, creo su size en GameObjects, los pongo en inactivo y los añaado a la cola
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            //Añado la cola al diccionario
            poolDictionary.Add(pool.tag, objectPool);
        }

    }

    //Funcion que permite sacar un objeto de la cola
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation){

            if(!poolDictionary.ContainsKey(tag)){
                throw new System.Exception("Pool with tag " + tag + " doesn't exist.");
            }
            //Saco el primer objeto de la cola con el tag correspondiente
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            //Pongo el objeto sacado en activo y lo coloco en la posicion y rotacion que se le pase
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;   
            objectToSpawn.transform.rotation = rotation;
            //Pongo la velocidad del RigidBody a 0 porque sino al desactivarse y activarse mantiene la aceleración
            Rigidbody rb = objectToSpawn.GetComponent<Rigidbody>();
            if (rb != null){
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }            
            //Vuelvo a añadir el objeto a la cola
            poolDictionary[tag].Enqueue(objectToSpawn);
            
            return objectToSpawn;
    }

}
