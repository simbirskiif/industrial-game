using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
