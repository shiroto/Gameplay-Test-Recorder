using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CanvasResolutionUtility : MonoBehaviour
{
    public Vector2Int resolution;
    private Canvas[] canvases;

    private void Awake()
    {
        canvases = GameObject.FindObjectsOfType<Canvas>(true).Where(c => c.renderMode == RenderMode.ScreenSpaceOverlay).ToArray();
    }

    private void Update()
    {
        foreach (Canvas canvas in canvases)
        {
            RectTransform t = (RectTransform)canvas.transform;
            t.sizeDelta = resolution;
        }
    }
}