using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePauseOverlayScript : MonoBehaviour
{
    public Canvas overlayCanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
          if (overlayCanvas.gameObject.activeInHierarchy) {
            overlayCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
          }
          else {
            overlayCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
          }
        }
    }

    //for resume button
    public void ResumeGame() {
      overlayCanvas.gameObject.SetActive(false);
      Time.timeScale = 1;
    }
}
