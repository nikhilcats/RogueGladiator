using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TogglePauseOverlayScript : MonoBehaviour
{
    public Canvas overlayCanvas;
    public string seed;
    public TextMeshProUGUI textElement;

    public TMP_InputField tmpInput;
    private int seedLength = 16;
    private List<int> seedList = new List<int>();

    private void Start()
    {
        

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
          if (overlayCanvas.gameObject.activeInHierarchy) {
            overlayCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
          }
          //Shows pause
          else {
            createSeed();
            tmpInput.text = seed;
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

    public void createSeed()
    {
        string seedString = "Seed: ";
        seedList = new List<int>();

        for (int i = 0; i < seedLength; i++)
        {
            seedList.Add(Random.Range(0, 9));
        }
        
        for (int listSize = 0; listSize < seedList.Count; listSize++)
        {
            string currentSeedLoc = seedList[listSize].ToString();
            seedString = seedString + currentSeedLoc;
        }
        print(seedString);
        seed = seedString;
        
    }
}
