using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] Button closeButton;
    Animator animator;
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
    }
    public void OpenPanel()
    {
        animator.Play("Panel Open");
    }
}
