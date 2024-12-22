using UnityEngine;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour
{
    public Text title;
    [SerializeField] Toggle toggle;
    public bool isChecked = false;
    IOnChangeCheckbox listener;
    bool getIsChecked()
    {
        return isChecked;
    }
    public void setData(string title, bool isChecked)
    {
        this.title.text = title;
        this.isChecked = isChecked;
        toggle.isOn = isChecked;

    }
    public void setListener(IOnChangeCheckbox listener)
    {
        this.listener = listener;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onValueChanged()
    {
        listener.OnChangeCheckbox(toggle.isOn);
        this.isChecked = toggle.isOn;
    }
}
