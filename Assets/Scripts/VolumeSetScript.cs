using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetScript : MonoBehaviour
{
  // Get the value of the slider
  public string volumeType;
  public Slider mySlider;

  void Start() {
    mySlider.value = PlayerPrefs.GetFloat(volumeType, 10f);
    Debug.Log("gen vol: " + PlayerPrefs.GetFloat(volumeType));
  }

  public void ChangeVol()
  {
    // Save the value to PlayerPrefs using a unique key
    PlayerPrefs.SetFloat(volumeType, mySlider.value);
  }
}
