using UnityEngine;
using UnityEngine.UI;

public class CircleProgressbar : MonoBehaviour
{
    float progress = 0;
    [SerializeField] Text text;
    public void set(float value)
    {
        progress = value;
        gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = progress;
        setText();
    }
    public void set(float value, float max)
    {
        progress = value / max;
        gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = progress;
        setText();
    }
    void setText()
    {
        text.text = (progress * 100).ToString("0") + "%";
    }
}