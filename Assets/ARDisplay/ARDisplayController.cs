using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

//Controls the animations and toggling of the AR Displa
//View and notifies the model when opening and closing occurs for that view
public class ARDisplayController : MonoBehaviour
{
    //View
    [SerializeField] private ARDisplayView _ARDisplayView;
    //Animator
    [SerializeField] private Animator _ARDisplayAnimator;
    [SerializeField] private Animator _ARDisplayGrabUIAnimator;

    //UI opening/closing events that trigger when the UIs opening and closing animations are finished
    public UnityEvent OnDisplayViewOpen = new UnityEvent();
    public UnityEvent OnDisplayViewClose = new UnityEvent();

    //start open or closed
    public bool closeOnStart = true;
    
    //for tracking state of UI
    private bool _isDisplaying = false;
    private bool _isAnimating = false;

    private void Awake()
    {
        if (_ARDisplayView != null)
        {
            _ARDisplayView.displayPanel.SetActive(false);
            OnDisplayViewClose.AddListener(() => { _isAnimating = false; _isDisplaying = false; });
            OnDisplayViewOpen.AddListener(() => { _isAnimating = false; _isDisplaying = true; });
        }
    }

    private void Start()
    {
        if (_ARDisplayView != null)
        {
            _ARDisplayView.OnToggleButtonPressed.AddListener(OnToggleButtonPressed);
        }

        if (closeOnStart)
        {
            StartClosing(); //need to close instantly so player doesn't see the animation
        }
    }

    private void OnToggleButtonPressed()
    {
        if (_ARDisplayView.displayPanel != null)
        {
            if (!_isDisplaying && !_isAnimating)
            {
                StartOpening();
            }
            else if (!_isAnimating)
            {
                StartClosing();
            }
        }
    }

    private void StartOpening()
    {
        _isAnimating = true;
        if (_ARDisplayAnimator != null)
        {
            _ARDisplayAnimator.SetBool("UIIsOpen", true);
            _ARDisplayGrabUIAnimator.SetBool("UIIsOpen", true);
        }
        OnDisplayViewOpen.Invoke();
    }

    private void StartClosing()
    {
        _isAnimating = true;
        if (_ARDisplayAnimator != null)
        {
            _ARDisplayAnimator.SetBool("UIIsOpen", false);
            _ARDisplayGrabUIAnimator.SetBool("UIIsOpen", false);
        }
        OnDisplayViewClose.Invoke();
    }

    public void TriggerToggle()
    {
        OnToggleButtonPressed();
    }
}
