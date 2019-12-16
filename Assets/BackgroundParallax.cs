using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {
    [Serializable]
    class ParallaxGroup{
        public List<Transform> images;
        [HideInInspector] public  List<Transform> imagesGhost;
        public float speed;
    }

    [SerializeField] List<ParallaxGroup> parallaxGroups;

    [SerializeField] float sizeX = 20.48f;

    AudioPeer audioPeer;

    void Start() {
        foreach (ParallaxGroup parallaxGroup in parallaxGroups) {
            parallaxGroup.imagesGhost = new List<Transform>();
            
            for (int i = 0; i < parallaxGroup.images.Count; i++) {
                GameObject o = Instantiate(parallaxGroup.images[i].gameObject, parallaxGroup.images[i].parent, true);
                parallaxGroup.imagesGhost.Add(o.transform);
                parallaxGroup.imagesGhost[i].position += Vector3.up * 0.1f;

                parallaxGroup.imagesGhost[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
            }
        }

        audioPeer = LevelManager.AudioPeer;
    }

    float beatValue = 0;

    // Update is called once per frame
    void Update() {
        float tmpValue = 0;
        foreach (float f in audioPeer.freqBand) {
            tmpValue += f;
        }

        tmpValue /= audioPeer.freqBand.Length;

        bool isBeat = false;
        if (tmpValue > beatValue) {
            beatValue = tmpValue;
            isBeat = true;
        }
        
        foreach (ParallaxGroup parallaxGroup in parallaxGroups) {
            Vector3 displacementVector = Time.deltaTime * parallaxGroup.speed * Vector3.left;

            for (int i = 0; i < parallaxGroup.images.Count; i++) {
                parallaxGroup.images[i].position += displacementVector;
                parallaxGroup.imagesGhost[i].position += displacementVector;

                //Beat effect
                if (isBeat) {
                    parallaxGroup.imagesGhost[i].position += Vector3.up * beatValue; 
                } else if(parallaxGroup.imagesGhost[i].position.y > 0) {
                    parallaxGroup.imagesGhost[i].position += Vector3.down * Time.deltaTime; 
                    beatValue = parallaxGroup.imagesGhost[i].position.y;
                }
                
                //If outside bounds, move it 
                if (!(parallaxGroup.images[i].position.x < -sizeX)) continue;
                parallaxGroup.images[i].position = new Vector3(parallaxGroup.images[i].position.x + (2 * sizeX),
                    parallaxGroup.images[i].position.y, parallaxGroup.images[i].position.z);
                    
                parallaxGroup.imagesGhost[i].position = new Vector3(
                    parallaxGroup.imagesGhost[i].position.x + (2 * sizeX), parallaxGroup.imagesGhost[i].position.y,
                    parallaxGroup.imagesGhost[i].position.z);
            }
        }
    }
}
