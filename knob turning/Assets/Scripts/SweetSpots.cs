using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SweetSpots : MonoBehaviour {

    public Vector2 sweetSpot;
    public int levelOfDifficulty;

    public float aveRandomRange;

    void Start () {
        OnTouchRandomRange();
    }
    
    void Update() {

        if (Input.touchCount > 0) {

            if(Input.GetTouch(0).phase == TouchPhase.Began) {

                int id = Input.GetTouch(0).fingerId;

                if (EventSystem.current.IsPointerOverGameObject(id)) {
                    OnTouchRandomRange();
                }
            }
        }
    }

    public void OnTouchRandomRange() {

        // Get Random Mean value for the range
        aveRandomRange = Random.Range(0, 360);

        // Output the random range including the preset level of difficulty
        sweetSpot = new Vector3(aveRandomRange - levelOfDifficulty, aveRandomRange + levelOfDifficulty, aveRandomRange);

    }
}


