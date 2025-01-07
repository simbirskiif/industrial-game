using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointerChecker : MonoBehaviour
{
    IPointerChecker pointerCheckerListener;
    public int ObjectLayerMask = 31;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void setListener(IPointerChecker pointerChecker)
    {
        pointerCheckerListener = pointerChecker;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
        if (other.gameObject.GetComponent<ObjectInfo>() != null)
        {
            if (other.gameObject.layer == ObjectLayerMask)
            {
                pointerCheckerListener.onEnterZone(other.gameObject.GetComponent<ObjectInfo>());
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        // Debug.Log("OnTriggerExit");
        if (other.gameObject.GetComponent<ObjectInfo>() != null)
        {
            if (other.gameObject.layer == ObjectLayerMask)
            {
                pointerCheckerListener.onExitZone();
            }
        }
    }
}
