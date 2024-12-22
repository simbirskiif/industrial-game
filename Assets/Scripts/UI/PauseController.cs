using UnityEngine;

public class PauseController : MonoBehaviour
{
    GameObject pauseMenu;
    Animator animator;
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("Pause Panel");
        animator = pauseMenu.GetComponent<Animator>();
        animator.Play("Pause Close Idle");
    }
    public void Pause()
    {
        animator.Play("Pause Open");
    }
    public void Resume()
    {
        animator.Play("Pause Close");
    }
}
