using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSelector : MonoBehaviour {

    [Tooltip("For when not near any specific place")]
    public GameObject defaultObject;
    [Tooltip("For location based readings")]
    public List<LocationObjectPair> ARObjectList;

    private LocationInfo location;

    [System.Serializable]
    public class LocationObjectPair
    {
        public GameObject ARObject;
        public Vector2 Coordinates;
    }

	private void Awake ()
    {
        Input.location.Start();

        ActivateObjectByClosestLocation();
    }
	
	void OnApplicationFocus (bool hasFocus)
    {
        ActivateObjectByClosestLocation();
    }

    // O(n)
    private void ActivateObjectByClosestLocation()
    {
        location = Input.location.lastData;

        for (int i = 0; i < ARObjectList.Count; i++)
            ARObjectList[i].ARObject.SetActive(false);

        for (int i = 0; i < ARObjectList.Count; i++)
        {
            if(Mathf.Abs(ARObjectList[i].Coordinates.x - location.longitude) <= 0.01f &&
                Mathf.Abs(ARObjectList[i].Coordinates.y - location.latitude) <= 0.01f)
            {
                ARObjectList[i].ARObject.SetActive(true);
                break;
            }
        }
    }
}
