using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TogglePauseOverlayScript : MonoBehaviour
{
  public GameObject gameManager;
  public Canvas overlayCanvas;
  public Canvas optionsCanvas;

  private void Start()
  {

  }
  // Update is called once per frame
  void Update()
  {
    //exits pause
    if (Input.GetKeyDown(KeyCode.Escape)) {
      if (overlayCanvas.gameObject.activeInHierarchy) {
        overlayCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
      }
      //Shows pause
      else {
        //seed stuff
        if (!optionsCanvas.gameObject.activeInHierarchy) {
          overlayCanvas.gameObject.SetActive(true);
          Time.timeScale = 0;
        }
      }
    }
  }

  //for resume button
  public void ResumeGame()
  {
    overlayCanvas.gameObject.SetActive(false);
    Time.timeScale = 1;
  }

  //destroy Game Manager on quit
  public void EndGame()
  {
    UnityEngine.Object.Destroy(gameManager);
  }
}
/*
    private void createSeed()
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
*/

//SEED STUFF MOVED HERE
/*
public string seed;
public TextMeshProUGUI textElement;
public TMP_InputField tmpInput;
private int seedLength = 16;
private List<int> seedList = new List<int>();

  //seed stuff
  createSeed();
  tmpInput.text = seed;
  //end seed stuff


*/
