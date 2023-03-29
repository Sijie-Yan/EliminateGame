using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button btn;

    private void Start()
    {
        this.btn = this.GetComponent<Button>();
        if (this.btn == null)
        {
            Debug.Log("请把ButtonScale脚本挂在Button下!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("?");
        LeanTween.scale(gameObject, new Vector3(0.7f, 0.7f, 1f), 0.05f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.05f);
    }
}