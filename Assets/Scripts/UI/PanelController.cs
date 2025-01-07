using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] Button closeButton;
    Animator animator;
    IOnClosePanel closeListener;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ClosePanel()
    {
        Debug.Log("ClosePanel");
        animator.Play("Panel Close");
        if (closeListener != null)
        {
            closeListener.OnClosePanel();
        }
    }
    public void OpenPanel()
    {
        animator.Play("Panel Open");
    }
    public void setCloseListener(IOnClosePanel listener)
    {
        closeListener = listener;
    }
}
