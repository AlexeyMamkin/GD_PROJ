using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Cargo : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public int weight;
    public float speed = 0.5f;
    public bool first = false;
    private TextMeshPro textMeshPro;
    //private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateText();
    }

    public void UpdateText()
    {
        textMeshPro.text = weight.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!first)
            Utils.EventManager<Cargo>.Trigger("RemoveCargo", this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!first)
            transform.Translate(eventData.delta * Time.deltaTime);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!first)
            if (transform.position.y > -4)
            {
                if (transform.position.x > 0)
                {
                    Utils.EventManager<Cargo>.Trigger("AddRightLever", this);
                }
                else
                {
                    Utils.EventManager<Cargo>.Trigger("AddLeftLever", this);
                }
            }
            else
            {
                Utils.EventManager<Cargo>.Trigger("AddPanel", this);
            }
    }
}
