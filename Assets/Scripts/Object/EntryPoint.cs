using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public float Cooldown;
    [SerializeField]float thisCooldown;
    [Header("DO NOT CHANGE MANUALLY")]
    int QueuedItem = -1;
    public ExitPoint PickupPoint;
    [SerializeField] bool isEmpty;
    void Start()
    {
        isEmpty = QueuedItem == -1;
        restartCooldown();
    }
    void Update()
    {
        isEmpty = QueuedItem == -1;
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
        return QueuedItem != -1;
    }
    public bool AddToQueue(int item)
    {
        if (notEmpty())
        {
            return false;
        }
        QueuedItem = item;
        return true;
    }
    public int GiveItem()
    {
        if (!readyToGive())
        {
            return -1;
        }
        int item = this.QueuedItem;
        QueuedItem = -1;
        restartCooldown();
        return item;
    }
    public int WhatItem()
    {
        if (notEmpty())
        {
            return QueuedItem;
        }
        return -1;
    }
    public void ClearQueue()
    {
        QueuedItem = -1;
    }
}
