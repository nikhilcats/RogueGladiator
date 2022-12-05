using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleOverlaysScript : MonoBehaviour
{
    public Canvas overlayCanvas1;
    public Canvas overlayCanvas2;
    public GameObject translucent;
    public bool isTranslucent;

    //turn off 1 and turn on 2
    public void switchOverlay() {
      if (overlayCanvas1.gameObject.activeInHierarchy) {
        overlayCanvas1.gameObject.SetActive(false);
        overlayCanvas2.gameObject.SetActive(true);
      }
      else {
        overlayCanvas1.gameObject.SetActive(true);
        overlayCanvas2.gameObject.SetActive(false);
      }
      if (isTranslucent) {
        translucent.gameObject.SetActive(true);
      }
      else {
        translucent.gameObject.SetActive(false);
      }
    }
}
