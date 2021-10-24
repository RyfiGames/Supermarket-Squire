using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageBar : MonoBehaviour
{

    public float maxWidth;
    private float percent;
    public RectTransform bar;

    public void setPercent(float p)
    {
        percent = p;
        bar.sizeDelta = new Vector2(maxWidth * percent, bar.sizeDelta.y);
    }

}
