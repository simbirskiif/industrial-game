using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointerChecker : MonoBehaviour
{
    public int ObjectLayerMask = 31;
    [Header("UI Elements")]
    public Text ObjectName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
                ObjectName.text = other.gameObject.GetComponent<ObjectInfo>().itemName;
                other.gameObject.GetComponent<Outline>().enabled = true;
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
                other.gameObject.GetComponent<Outline>().enabled = false;
            }
        }
        ObjectName.text = "";
    }
}
