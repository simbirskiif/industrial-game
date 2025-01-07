using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed = false;
    float deltaTime = 0.0f;
    public float holdThreshold = 0.7f;
    public IOnButtonReleasedHandler listener;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        Debug.Log("Button Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        Debug.Log("Button Released");
        if (deltaTime > holdThreshold)
        {
            if (listener != null)
            {
                listener.OnButtonHeld();
            }
        }
        else
        {
            if (listener != null)
            {
                listener.OnButtonClicked();
            }
        }
        deltaTime = 0.0f;
    }
    void Update()
    {
        if (isPressed)
        {
            deltaTime += Time.deltaTime;
        }
    }
    public void setListener(IOnButtonReleasedHandler listener)
    {
        this.listener = listener;
    }
}

