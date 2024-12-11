using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    public float Cooldown;
    [SerializeField] float thisCooldown;
    [Header("DO NOT CHANGE MANUALLY")]
    ItemInfo QueuedItem = null;
    public EntryPoint GivingPoint;
    [SerializeField] bool isEmpty;
    void Start()
    {
        isEmpty = QueuedItem == null;
        restartCooldown();
    }
    void Update()
    {
        isEmpty = QueuedItem == null;
        if (thisCooldown > 0) thisCooldown -= Time.deltaTime;
        if(GivingPoint != null)
        {
           if(readyToGive())
           {
               if(!GivingPoint.notEmpty())
               {
                   restartCooldown();
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
        return item;
    }
    public void ClearQueue()
    {
        QueuedItem = null;
    }
}
