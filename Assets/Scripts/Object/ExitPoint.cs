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
        masterManager = GameObject.Find("MasterManager").GetComponent<MasterManager>();
        isEmpty = QueuedItem == null;
        restartCooldown();
    }
    void Update()
    {
        isEmpty = QueuedItem == null;
        if (thisCooldown > 0) thisCooldown -= Time.deltaTime;
        if (GivingPoint != null)
        {
            if (readyToGive())
            {
                if (!GivingPoint.notEmpty())
                {
                    ItemInfo item = GiveItem();
                    GivingPoint.AddToQueue(item);
                }
            }
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
        return (QueuedItem != null);
    }
    public bool AddToQueue(ItemInfo item)
    {
        if (notEmpty())
        {
            Debug.Log("NOT EMPTY!!!");
            return false;
        }
        QueuedItem = item;
        return true;
    }
    public ItemInfo GiveItem()
    {
        if (!readyToGive())
        {
            return null;
        }
        ItemInfo item = this.QueuedItem;
        Debug.Log("Giving item: " + item.Name);
        QueuedItem = null;
        restartCooldown();
        return item;
    }
    public void ClearQueue()
    {
        QueuedItem = null;
    }
}
