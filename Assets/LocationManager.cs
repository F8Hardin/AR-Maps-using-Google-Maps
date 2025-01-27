using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Android Location Services
public class LocationManager : MonoBehaviour
{
    public UnityEvent<bool, float, float> onLocationResult;

    private float locationServiceWait = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetLocation());
    }

   IEnumerator GetLocation()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Location services are not enabled on the device.");
            onLocationResult.Invoke(false, 0, 0);
            yield break;
        }

        Input.location.Start();

        float currentWait = 0;

        while (Input.location.status == LocationServiceStatus.Initializing && currentWait < locationServiceWait)
        {
            currentWait++;
            yield return new WaitForSeconds(1);
        }

        if (currentWait > locationServiceWait || Input.location.status == LocationServiceStatus.Failed)
        {
            onLocationResult.Invoke(false, 0, 0);
            yield break;
        }
        else
        {
            onLocationResult.Invoke(true, Input.location.lastData.latitude, Input.location.lastData.longitude);
            yield break;
        }
    }
}
