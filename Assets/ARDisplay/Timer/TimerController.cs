using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private TimerControlView ControlView;
    [SerializeField]
    private List<TimerClockView> ClockViews;

    private float targetTime = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
    }
}
