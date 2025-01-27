using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//View with references to button and other UI elements for a default ARDisplay
public class ARDisplayView : MonoBehaviour
{
    //The button the toggles the Display
    [SerializeField] public Button toggleButton; //can change to interactable unity event wrapper and Add Listener to on select (see AR map set up)
    //The panel being shown that is animated when toggling the UI
    [SerializeField] public GameObject displayPanel;

    //Event saying toggle button pressed
    public UnityEvent OnToggleButtonPressed = new UnityEvent();

    private void Awake()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(() => { OnToggleButtonPressed.Invoke(); });
        }
    }
}
