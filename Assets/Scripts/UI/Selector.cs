using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public Text title;
    public Text data;
    public GameObject dots;
    public int position;
    public SelectorOptions[] options;
    IOnChangePositionInSelector onChangePositionInSelector;
    [SerializeField] GameObject dotPrefab;
    public void SetData(string title, SelectorOptions[] options)
    {
        foreach (Transform child in dots.transform)
        {
            Destroy(child.gameObject);
        }
        position = 0;
        this.title.text = title;
        this.options = options;
        data.text = options[position].data;
        for (int i = 0; i < options.Length; i++)
        {
            GameObject dot = Instantiate(dotPrefab, dots.transform);
        }
        setDot(position);
    }
    public void setListener(IOnChangePositionInSelector listener)
    {
        onChangePositionInSelector = listener;
    }
    public void next()
    {
        if (position < options.Length - 1)
        {
            position++;
            data.text = options[position].data;
        }
        else
        {
            position = 0;
            data.text = options[position].data;
        }
        setDot(position);
        onChangePositionInSelector.OnChangePositionInSelector(getIndex());
    }
    public void previous()
    {
        if (position > 0)
        {
            position--;
            data.text = options[position].data;
        }
        else
        {
            position = options.Length - 1;
            data.text = options[position].data;
        }
        setDot(position);
        onChangePositionInSelector.OnChangePositionInSelector(getIndex());
    }
    void setDot(int index)
    {
        List<Transform> dotsList = new List<Transform>();
        foreach (Transform child in dots.transform)
        {
            dotsList.Add(child);
        }
        Color colorSelected = new Color(1, 1, 1, 1);
        Color colorUnselected = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        foreach (Transform dot in dotsList)
        {
            if (index != dotsList.IndexOf(dot))
            {
                dot.GetComponent<Image>().color = colorUnselected;
            }
            else
            {
                dot.GetComponent<Image>().color = colorSelected;
            }
        }
    }
    int getIndex()
    {
        return position;
    }
}
