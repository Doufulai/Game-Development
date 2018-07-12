using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SweetSpots : MonoBehaviour {

    public Vector2 randomSpot;
    public int levelOfDifficulty;

    float randomSpotsinDeg;

    void Start () {
        OnTouchRandomRange();
    }
    
    void Update() {

        foreach (Touch touch in Input.touches) {

            int id = touch.fingerId;

            if (EventSystem.current.IsPointerOverGameObject(id)) {
                OnTouchRandomRange();
            }
        }
    }

    public void OnTouchRandomRange() { 

            randomSpotsinDeg = Mathf.RoundToInt(Random.Range(-1f, 2f) * 180f);

            randomSpot = new Vector2(randomSpotsinDeg - levelOfDifficulty, randomSpotsinDeg + levelOfDifficulty);

    }
}


