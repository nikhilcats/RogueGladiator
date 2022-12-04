using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
  // The name of the scene that you want to load
  public string sceneName;

  public void ChangeScene()
  {
    // Load the scene with the specified name
    SceneManager.LoadScene(sceneName);
  }
}
