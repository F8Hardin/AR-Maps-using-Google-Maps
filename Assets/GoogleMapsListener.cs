using Meta.XR.ImmersiveDebugger.UserInterface;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GoogleMapsView : MonoBehaviour
{
    public RawImage MapImage;
    public UnityEvent<float, float> RequestMapLocation;

    private bool obtainedInitialLocation = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMapLoaded(Texture2D mapTexture)
    {
        MapImage.texture = mapTexture;
    }

    public void OnLocationObtained(bool value, float latitude, float longitude)
    {
        if (value)
        {
            Debug.Log("Successfully obtained device location.");

            if (!obtainedInitialLocation)
            {
                obtainedInitialLocation = true;
                RequestMapLocation.Invoke(latitude, longitude);
            }
        }
        else
        {
            Debug.LogError("Error: Unable to obtain device location.");
        }
    }
}
