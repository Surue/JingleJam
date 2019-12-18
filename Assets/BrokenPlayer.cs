using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrokenPlayer : MonoBehaviour {
    List<Rigidbody2D> bodies;
    
    // Start is called before the first frame update
    void Start() {
        bodies = GetComponentsInChildren<Rigidbody2D>().ToList();

        foreach (Rigidbody2D body in bodies) {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
