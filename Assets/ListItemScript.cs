using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItemScript : MonoBehaviour
{
    public static List<ListItemScript> allItems = new List<ListItemScript>();
    public string listName;

    [SerializeField] AnimationCurve curve;
    Vector2 startMax;
    Vector2 startMin;

    [SerializeField] Vector2 diff;
    [SerializeField] float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        startMax = GetComponent<RectTransform>().anchorMax;
        startMin = GetComponent<RectTransform>().anchorMin;

        int count = 0;
        foreach (ListItemScript items in allItems)
            count += items.listName == listName ? 1 : 0;

        StartCoroutine("moveSelection", count);

        allItems.Add(this);
    }

    float fps = 0.016f;

    IEnumerator moveSelection(int count)
    {
        float time = 0;

        while (time < maxTime)
        {
            time += fps;

            GetComponent<RectTransform>().anchorMax = Vector2.LerpUnclamped(
                startMax, startMax + (count * diff),
                curve.Evaluate(Mathf.InverseLerp(0,maxTime,time))
                );
            GetComponent<RectTransform>().anchorMin = Vector2.LerpUnclamped(
                startMin, startMin + (count * diff),
                curve.Evaluate(Mathf.InverseLerp(0, maxTime, time))
                );

            yield return new WaitForSeconds(fps);
        }
    }

    private void OnDisable()
    {
        allItems.Remove(this);
    }

    private void OnDestroy()
    {
        allItems.Remove(this);
    }
}
