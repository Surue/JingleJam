using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour, LevelManager.IPausedListener {
    [Serializable]
    class ParallaxGroup{
        public List<Transform> images;
        [HideInInspector] public  List<Transform> imagesGhost;
        public float speed;
    }

    [SerializeField] List<ParallaxGroup> parallaxGroups;

    [SerializeField] float sizeX = 20.48f;

    AudioPeer audioPeer;
    float beatValue = 0;
    
    bool isPaused = false;

    float playerStartPosX;
    float playerOffsetPosX;
    PlayerController player;
    Rigidbody2D playerBody;

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
        
        LevelManager.Instance.AddPauseListener(this);

        player = LevelManager.PlayerController;
        playerBody = player.GetComponent<Rigidbody2D>();
        
        playerStartPosX = player.transform.position.x;
        transform.position = new Vector3(playerStartPosX, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        if (isPaused) return;

        playerOffsetPosX = player.transform.position.x - playerStartPosX;
        
//        if (playerBody.velocity.x < 0.1f) return;
        
        float tmpValue = audioPeer.freqBand.Sum();

        tmpValue /= audioPeer.freqBand.Length;

        bool isBeat = false;
        if (tmpValue > beatValue) {
            beatValue = tmpValue;
            isBeat = true;
        }
        
        transform.position = new Vector3(playerStartPosX + playerOffsetPosX, 0, 0);
        
        foreach (ParallaxGroup parallaxGroup in parallaxGroups) {
            Vector3 displacementVector = (Time.deltaTime * parallaxGroup.speed * Vector3.left) * playerBody.velocity.x;

            for (int i = 0; i < parallaxGroup.images.Count; i++) {
                parallaxGroup.images[i].localPosition += displacementVector;
                parallaxGroup.imagesGhost[i].localPosition += displacementVector;

                //Beat effect
                if (isBeat) {
                    parallaxGroup.imagesGhost[i].localPosition += Vector3.up * beatValue; 
                } else if(parallaxGroup.imagesGhost[i].localPosition.y > 0) {
                    parallaxGroup.imagesGhost[i].localPosition += Vector3.down * Time.deltaTime; 
                    beatValue = parallaxGroup.imagesGhost[i].localPosition.y;
                }
                
                //If outside bounds, move it 
                if (!(parallaxGroup.images[i].localPosition.x < -sizeX)) continue;
                parallaxGroup.images[i].localPosition = new Vector3(
                    parallaxGroup.images[i].localPosition.x + (2 * sizeX),
                    parallaxGroup.images[i].localPosition.y, 
                    parallaxGroup.images[i].localPosition.z);
                    
                parallaxGroup.imagesGhost[i].localPosition = new Vector3(
                    parallaxGroup.imagesGhost[i].localPosition.x + (2 * sizeX), 
                    parallaxGroup.imagesGhost[i].localPosition.y,
                    parallaxGroup.imagesGhost[i].localPosition.z);
            }
        }
    }

    public void OnPaused() {
        isPaused = true;
    }

    public void OnUnpaused() {
        isPaused = false;
    }
}
