using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {
    
    [SerializeField] List<Transform> lvl1;
    [SerializeField] List<Transform> lvl2;
    [SerializeField] List<Transform> lvl3;
    [SerializeField] List<Transform> lvl4;

    [SerializeField] float speed1 = 1;
    [SerializeField] float speed2 = 1.1f;
    [SerializeField] float speed3 = 1.2f;
    [SerializeField] float speed4 = 1.3f;

    List<float> speeds = new List<float>{1, 1.1f, 1.2f, 1.3f, 1.4f};
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform transform1 in lvl1) {
            transform1.position += Vector3.left * speeds[0] * Time.deltaTime;

            if (transform1.position.x < -20.48f) {
                transform1.position = new Vector3(20.48f, transform1.position.y, transform1.position.z);
            }
        }
        
        foreach (Transform transform1 in lvl2) {
            transform1.position += Vector3.left * speeds[1] * Time.deltaTime;
            
            if (transform1.position.x < -20.48f) {
                transform1.position = new Vector3(20.48f, transform1.position.y, transform1.position.z);
            }
        }
        
        foreach (Transform transform1 in lvl3) {
            transform1.position += Vector3.left * speeds[3] * Time.deltaTime;
            
            if (transform1.position.x < -20.48f) {
                transform1.position = new Vector3(20.48f, transform1.position.y, transform1.position.z);
            }
        }
        
        foreach (Transform transform1 in lvl4) {
            transform1.position += Vector3.left * speeds[4] * Time.deltaTime;
            
            if (transform1.position.x < -20.48f) {
                transform1.position = new Vector3(20.48f, transform1.position.y, transform1.position.z);
            }
        }
    }
}
