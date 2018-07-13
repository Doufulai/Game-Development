using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyUpController : MonoBehaviour {

    Text frequency;

	// Use this for initialization
	void Start () {
        frequency = GetComponent<Text>();
    }
	
	// Update is called once per frame
    public void frequencyUpdate(float value) {
        frequency.text = value.ToString() + " s";
    }
}
