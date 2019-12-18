using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrokenPlayer : MonoBehaviour {
    List<SpriteRenderer> sprites;

    [SerializeField] float initialTimer = 2;
    float timer = 2;

    Color defaultColor;
    
    // Start is called before the first frame update
    void Start() {
        sprites = GetComponentsInChildren<SpriteRenderer>().ToList();

        timer = initialTimer;
        defaultColor = sprites[0].color;
        
        Destroy(gameObject, initialTimer);
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;

        foreach (SpriteRenderer spriteRenderer in sprites) {
            spriteRenderer.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, Mathf.Lerp(0, 1, timer / initialTimer));
        }
    }
}
