using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    MasterManager masterManager;
    public float Cooldown;
    [SerializeField] float thisCooldown;
    [Header("DO NOT CHANGE MANUALLY")]
    int QueuedItem = -1;
    public EntryPoint GivingPoint;
    [SerializeField] bool isEmpty;
    void Start()
    {
        masterManager = GameObject.FindGameObjectWithTag("MasterManager").GetComponent<MasterManager>();
        isEmpty = QueuedItem == -1;
        restartCooldown();
    }
    void Update()
    {
        isEmpty = QueuedItem == -1;
        if (thisCooldown > 0) thisCooldown -= Time.deltaTime;
        if (GivingPoint != null && readyToGive() && !GivingPoint.notEmpty())
        {
            int item = GiveItem();
            GivingPoint.AddToQueue(item);
        }
    }
    void restartCooldown()
    {
        thisCooldown = Cooldown;
    }
    public bool readyToGive()
    {
        if (notEmpty() && thisCooldown <= 0)
        {
            return true;
        }
        return false;
    }
    public bool notEmpty()
    {
        return (QueuedItem != -1);
    }
    public bool AddToQueue(int itemID)
    {
        if (notEmpty())
        {
            Debug.Log("NOT EMPTY!!!");
            return false;
        }
        QueuedItem = itemID;
        return true;
    }
    public int GiveItem()
    {
        if (!readyToGive())
        {
            return -1;
        }
        int item = this.QueuedItem;
        Debug.Log("Giving item: " + masterManager.Items[item].Name);
        QueuedItem = -1;
        restartCooldown();
        return item;
    }
    public void ClearQueue()
    {
        QueuedItem = -1;
    }
}
