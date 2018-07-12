using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KnobController : MonoBehaviour {

    public GameObject indicator;
    public Text rotationScale;
    public Text sweetSpotDisplay;
    public int difficulty;
    
    float deltaAngleinDeg;
    int rotationScaleHolder;
    bool firstTouchInsideObject;
    Vector3 randomSpotRange;
    Vector3 objectCenter;

	// Use this for initialization
	void Start () {

        // Tag the indicator to be a child under the knob
        indicator.transform.parent = transform;

        // Get the Gameobject center w.r.t the screen
        objectCenter = Camera.main.WorldToScreenPoint(transform.position);

    }
	
	// Update is called once per frame
	void Update () {
        
        // Detect touch
        if (Input.touchCount > 0) {

            randomSpotRange = FindObjectOfType<SweetSpots>().sweetSpot;

            sweetSpotDisplay.text = randomSpotRange.x.ToString() + " to " + randomSpotRange.y.ToString();

            // Store the direction where the finger points to Gameobject from the camera point of view
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            // Verify if touch.began lies within the object itself. If true, set firstTouchInsideObject to true
            if (Input.GetTouch(0).phase == TouchPhase.Began) {

                firstTouchInsideObject = Physics.Raycast(raycast) == true ? true : false;

            }
            
            // Verify if touch.began lies within the object itself. If true, object rotation will get activated
            if (firstTouchInsideObject) {

                // Define the direction of the finger with respect to the Gameobject center
                deltaAngleinDeg = getRotationinDeg(objectCenter, Input.GetTouch(0).position);

                // Detect if the touch falls in the 1st and 2nd quadrant
                if (Input.GetTouch(0).position.y >= objectCenter.y) {

                    // Point the object to where the touch is located
                    transform.eulerAngles = new Vector3(0, 0, -deltaAngleinDeg);

                    // Detect if the touch falls in the 1st quadrant
                    if (Input.GetTouch(0).position.x > objectCenter.x) {

                        // Display the rotation angle in the 1st quadrant
                        rotationScaleHolder = Mathf.RoundToInt(deltaAngleinDeg);
                    }
                    else {

                        // Display the rotation angle in the 2nd quadrant
                        rotationScaleHolder = Mathf.RoundToInt(deltaAngleinDeg + 360f);
                    }
                }else {

                    // Point the object to where the touch is located 
                    transform.eulerAngles = new Vector3(0, 0, 180f-deltaAngleinDeg);

                    // Display the rotation angle in the 3rd and 4th quadrant
                    rotationScaleHolder = 180 + Mathf.RoundToInt(deltaAngleinDeg);
                }
                
                rotationScale.text = rotationScaleHolder <= 180 ? (-rotationScaleHolder).ToString() + "\u00B0" : (360 - rotationScaleHolder).ToString() + "\u00B0";

                // Show the angle in degree in the console
                //Debug.Log(rotationScaleHolder);

                gameObject.GetComponent<Renderer>().material.color = new Color((randomSpotRange.z - rotationScaleHolder)/180*255, 0, 0);
                indicator.GetComponent<Renderer>().material.color = new Color(255, 0, 0);

                //if (isUnlocked(randomSpotRange, rotationScaleHolder)) {
                //    Debug.Log(isUnlocked(randomSpotRange, rotationScaleHolder));

                    

                //}
                //else {

                //    gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                //    indicator.GetComponent<Renderer>().material.color = new Color(0, 0, 0);

                //}

            }

        }
    }

    /**
     *  Objective: To calculate the angle of the touch w.r.t the Gameobject
     * 
     *  Argument:
     *  objectCenter    -- Vector3 of the Gameobject center
     *  touchCoor       -- Vector2 of the touch coordinate
    
     *  Returns:
     *  getRotationinDeg    -- Angle of the touch w.r.t the Gameobject in Degree
     **/
    float getRotationinDeg(Vector3 objectCenter, Vector2 touchCoor) {
        return Mathf.Rad2Deg * Mathf.Atan((touchCoor.x - objectCenter.x) / (touchCoor.y - objectCenter.y));
    }

    bool isUnlocked(Vector3 sweetSpot, int currentAngle) {
        return currentAngle < sweetSpot.y && currentAngle > sweetSpot.x ? true : false;
    }

}
