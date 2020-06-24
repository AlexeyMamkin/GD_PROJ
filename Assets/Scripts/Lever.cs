using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Transform m_leftCargosParent;
    private Transform m_rightCargosParent;
    private SpriteRenderer m_spriteRenderer;
    private List<Cargo> m_leftCargos;
    private List<Cargo> m_rightCargos;
    private int m_leftWeight = 0;
    private int m_rightWeight = 0;

    [SerializeField] private Cargo cargoPref;

    private void OnEnable()
    {
        Utils.EventManager<Cargo>.StartListening("AddRightLever", AddRight);
        Utils.EventManager<Cargo>.StartListening("AddLeftLever", AddLeft);
        Utils.EventManager<Cargo>.StartListening("RemoveCargo", RemoveCargo);
        Utils.EventManager.StartListening("ClearField", ClearField);
    }

    private void OnDisable()
    {
        Utils.EventManager<Cargo>.StopListening("AddRightLever", AddRight);
        Utils.EventManager<Cargo>.StopListening("AddLeftLever", AddLeft);
        Utils.EventManager<Cargo>.StopListening("RemoveCargo", RemoveCargo);
        Utils.EventManager.StopListening("ClearField", ClearField);
    }

    private void Start()
    {
        m_leftCargos = new List<Cargo>();
        m_rightCargos = new List<Cargo>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_leftCargosParent = GameObject.Find("LeftCargos").GetComponent<Transform>();
        m_rightCargosParent = GameObject.Find("RightCargos").GetComponent<Transform>();
    }

    public void SetSprite(Sprite sprite)
    {
        m_spriteRenderer.sprite = sprite;
    }

    public void SetBasicCargo(int basicCargoWeight)
    {
        Cargo cargo = Instantiate(cargoPref, m_leftCargosParent);
        cargo.weight = basicCargoWeight;
        cargo.first = true;
        m_leftCargos.Add(cargo);
        m_leftWeight += cargo.weight;
    }

    private void AddRight(Cargo cargo)
    {
        m_rightCargos.Add(cargo);
        m_rightWeight += cargo.weight;
        UpdateLevers();
        Utils.EventManager<Vector2Int>.Trigger("CheckWeights", new Vector2Int(m_leftWeight, m_rightWeight));
    }

    private void AddLeft(Cargo cargo)
    {
        m_leftCargos.Add(cargo);
        m_leftWeight += cargo.weight;
        UpdateLevers();
        Utils.EventManager<Vector2Int>.Trigger("CheckWeights", new Vector2Int(m_leftWeight, m_rightWeight));
    }

    private void UpdateLevers()
    {
        Vector3 pos = m_leftCargosParent.position;

        float offset = -1f;

        for (int i = 0; i < m_leftCargos.Count; i++)
        {
            m_leftCargos[i].transform.position = pos;
            pos.y += offset;
        }

        pos = m_rightCargosParent.position;

        for (int i = 0; i < m_rightCargos.Count; i++)
        {
            m_rightCargos[i].transform.position = pos;
            pos.y += offset;
        }
    }

    private void RemoveCargo(Cargo cargo)
    {
        if (m_leftCargos.Remove(cargo))
        {
            m_leftWeight -= cargo.weight;
        }
        if (m_rightCargos.Remove(cargo))
        {
            m_rightWeight -= cargo.weight;
        }

        UpdateLevers();
        Utils.EventManager<Vector2Int>.Trigger("CheckWeights", new Vector2Int(m_leftWeight, m_rightWeight));
    }

    private void ClearField()
    {
        for (int i = 0; i < m_leftCargos.Count; i++)
        {
            Destroy(m_leftCargos[i].gameObject);
        }
        for (int i = 0; i < m_rightCargos.Count; i++)
        {
            Destroy(m_rightCargos[i].gameObject);
        }
        m_leftCargos.Clear();
        m_rightCargos.Clear();
        UpdateLevers();
        m_leftWeight = 0;
        m_rightWeight = 0;
    }
}
