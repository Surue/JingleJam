using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    bool isPause = false;

    [SerializeField] GameObject panelPause;
    
    // Start is called before the first frame update
    void Start()
    {
        panelPause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPause) {
                LevelManager.Instance.Unpause();
                panelPause.SetActive(false);
            } else {
                LevelManager.Instance.Pause();
                panelPause.SetActive(true);
            }

            isPause = !isPause;
        }
    }

    public void Unpause() {
        LevelManager.Instance.Unpause();
        panelPause.SetActive(false);
        isPause = false;
    }

    public void ReturnMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
    }
}
