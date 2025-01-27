using Oculus.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System.Drawing;

//Google Maps API Calls
public class GoogleMapsClient : MonoBehaviour
{
    public UnityEvent<Texture2D> OnMapLoaded;


    private float zoomLevel = 15;
    private float currentLat = 37;
    private float currentLong = -122;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initializing google maps connection...");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadMap(float latitude, float longitude)
    {
        string url = getMapLocationBaseUrl +  "?center=" + latitude + "," + longitude + "&zoom=" + zoomLevel + "&size=1920x1080&key=" + API_KEY;
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        Debug.Log("Requesting map data from " + url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D jsonData = DownloadHandlerTexture.GetContent(request);
            OnMapLoaded.Invoke(jsonData);
            Debug.Log("Successfully received map data from google maps API.");
        } else
        {
            Debug.LogError("Failed to retreive map data from google maps API. Error: " + request.error);
        }
    }

    public void RequestMapLocation(float latitude, float longitude)
    {
        Debug.Log("Requesting map location.");
        StartCoroutine(LoadMap(latitude, longitude));
    }
}
