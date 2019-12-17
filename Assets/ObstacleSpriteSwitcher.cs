using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpriteSwitcher : MonoBehaviour {
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite snowySprite;

    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = defaultSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            spriteRenderer.sprite = snowySprite;
        }
    }
}
