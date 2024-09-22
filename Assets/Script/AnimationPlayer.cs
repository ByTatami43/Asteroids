using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    public int currentSprite;



    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start(){
        //InvokeRepeating hace que se ejecute una funcion cada cierto tiempo
        InvokeRepeating(nameof(animateSprite), 0.15f, 0.15f);
    }

    private void animateSprite(){
        currentSprite++;

        if(currentSprite >= sprites.Length){
            currentSprite = 0;
        }

        spriteRenderer.sprite = sprites[currentSprite];
    }
}