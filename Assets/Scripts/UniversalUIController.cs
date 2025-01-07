using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class UniversalUIController : MonoBehaviour, IPointerChecker, IOnButtonReleasedHandler
{
    PointerChecker pointerChecker;
    ButtonPressHandler buttonPressHandler;
    ObjectInfo target;
    [Header("General Info UI Elements")]
    public GameObject generalInfoPanel;
    void Start()
    {
        pointerChecker = GameObject.FindGameObjectWithTag("Pointer").GetComponent<PointerChecker>();
        pointerChecker.setListener(this);
        buttonPressHandler = GameObject.FindGameObjectWithTag("Touch Handler").GetComponent<ButtonPressHandler>();
        buttonPressHandler.setListener(this);
    }

    void Update()
    {

    }
    void IPointerChecker.onEnterZone()
    {
        Debug.Log("OnEnterZone");
    }
    void IPointerChecker.onEnterZone(ObjectInfo objectInfo)
    {
        Debug.Log("OnEnterZone with ObjectInfo");
    }
    void IPointerChecker.onExitZone()
    {
        Debug.Log("OnExitZone");
    }
    void IOnButtonReleasedHandler.OnButtonClicked()
    {
        Debug.Log("OnButtonClicked");
    }
    void IOnButtonReleasedHandler.OnButtonHeld()
    {
        Debug.Log("OnButtonHeld");
    }
    void changeGeneralInfo()
    {

    }
    void changeGeneralInfo(ObjectInfo target)
    {
        this.target = target;
        changeGeneralInfo();
    }
}
