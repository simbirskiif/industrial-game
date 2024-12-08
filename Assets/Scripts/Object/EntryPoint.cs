using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [Header("DO NOT CHANGE MANUALLY")]
    public ItemInfo QueuedItem;
    public ExitPoint PickupPoint;
    public bool notEmpty()
    {
        return QueuedItem != null;
    }
    public void AddToQueue(ItemInfo item)
    {
        if (notEmpty())
        {
            return;
        }
        QueuedItem = item;
    }
    public ItemInfo GiveItem()
    {
        if (notEmpty())
        {
            return null;
        }
        ItemInfo item = this.QueuedItem;
        QueuedItem = null;
        return item;
    }
    public void ClearQueue()
    {
        QueuedItem = null;
    }
    public ItemInfo ClearAndGet()
    {
        ItemInfo item = this.QueuedItem;
        QueuedItem = null;
        return item;
    }
}
