using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnobController : MonoBehaviour {

    public Transform indicator;
    public Text rotationScale;
    
    float deltaAngleinDeg;
    Vector3 objectCenter;

	// Use this for initialization
	void Start () {

        indicator.parent = transform;
        objectCenter = Camera.main.WorldToScreenPoint(transform.position);

	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.touchCount > 0) {
            deltaAngleinDeg = getRotationinDeg(objectCenter, Input.GetTouch(0).position);

            if (Input.GetTouch(0).position.y >= objectCenter.y) {
                transform.eulerAngles = new Vector3(0, 0, -deltaAngleinDeg);

                if (Input.GetTouch(0).position.x > objectCenter.x) {
                    rotationScale.text = Mathf.RoundToInt(deltaAngleinDeg).ToString() + "\u00B0";
                }
                else {
                    rotationScale.text = Mathf.RoundToInt(deltaAngleinDeg + 360f).ToString() + "\u00B0";
                }
            }else {
                transform.eulerAngles = new Vector3(0, 0, 180f-deltaAngleinDeg);

                rotationScale.text = (180 + Mathf.RoundToInt(deltaAngleinDeg)).ToString() + "\u00B0";
            }
            
            Debug.Log(deltaAngleinDeg);
        }
    }

    float getRotationinDeg(Vector3 objectCenter, Vector2 touchCoor) {
        return Mathf.Rad2Deg * Mathf.Atan((touchCoor.x - objectCenter.x) / (touchCoor.y - objectCenter.y));
    }
}
