using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed = false;
    float deltaTime = 0.0f;
    public float holdThreshold = 0.7f;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        Debug.Log("Button Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        Debug.Log("Button Released");
        deltaTime = 0.0f;
    }
    void Update()
    {
        if (isPressed)
        {
            deltaTime += Time.deltaTime;
        }
    }
}

