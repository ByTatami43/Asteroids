using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //array de sprites 
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    public int currentSprite;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //actualiza el Sprite actual
    public void animateSprite(){
        spriteRenderer.sprite = sprites[currentSprite];
    }    
    public bool perderVida(){
        currentSprite++;
        return currentSprite < sprites.Length;
    }
}
