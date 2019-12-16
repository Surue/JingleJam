using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    [SerializeField] float speed = 5;
    [SerializeField] float lifeTime = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update() {
        transform.position += speed * Time.deltaTime * Vector3.left;
    }
}
