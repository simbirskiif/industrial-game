using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public float Cooldown;
    [SerializeField]float thisCooldown;
    [Header("DO NOT CHANGE MANUALLY")]
    ItemInfo QueuedItem;
    public ExitPoint PickupPoint;
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
        //!ТУТ НИХУЯ НЕТ, ПОРТЫ НЕ ВЗАИМОДЕЙСТВУЮТ
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
        return QueuedItem != null;
    }
    public bool AddToQueue(ItemInfo item)
    {
        if (notEmpty())
        {
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
        QueuedItem = null;
        return item;
    }
    public ItemInfo WhatItem()
    {
        if (notEmpty())
        {
            return QueuedItem;
        }
        return null;
    }
    public void ClearQueue()
    {
        QueuedItem = null;
    }
}
