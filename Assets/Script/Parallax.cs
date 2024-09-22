using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{

    public AudioSource audioSource;

    public MeshRenderer meshRenderer;
    // Start is called before the first frame update

    public float animSpeed = 0.5f;
    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource.Play();
    }

    //Cada frame mueve un poco el offset del fondo
    private void Update(){
        meshRenderer.material.mainTextureOffset += new Vector2(animSpeed*Time.deltaTime, 0);
        
    }
}
