using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Transform m_leftCargosParent;
    private Transform m_rightCargosParent;
    private SpriteRenderer m_spriteRenderer;
    private int rightLength = 0;
    private int leftLength = 0;
    private List<Cargo> m_leftCargos;
    private List<Cargo> m_rightCargos;
    private int m_leftWeight = 0;
    private int m_rightWeight = 0;
    private float speed = 0;
    private float maxSpeed = 70;
    private float acceleration = 70;
    private GameObject rotatingDot;
    [SerializeField] private bool toLeft = false;
    [SerializeField] private bool toRight = false;
    [SerializeField] private bool toEqual = false;

    [SerializeField] private Cargo cargoPref;

    private void Update()
    {
        if (toLeft)
        {
            UpdateLevers();
            if (this.transform.rotation.eulerAngles.z < 25 || this.transform.rotation.eulerAngles.z > 334)
            {
                if (speed < maxSpeed)
                {
                    speed = speed + acceleration * Time.deltaTime;
                }
                this.transform.RotateAround(rotatingDot.transform.position, new Vector3(0, 0, 1), speed * Time.deltaTime);
                m_leftCargos[0].transform.rotation = Quaternion.identity;
            }
            else
            {
                speed = 0;
            }
        }
        if (toRight)
        {
            UpdateLevers();
            if (this.transform.rotation.eulerAngles.z > 335 || this.transform.rotation.eulerAngles.z < 26)
            {
                if (speed < maxSpeed)
                {
                    speed = speed + acceleration * Time.deltaTime;
                }
                this.transform.RotateAround(rotatingDot.transform.position, new Vector3(0, 0, -1), speed * Time.deltaTime);
                m_leftCargos[0].transform.rotation = Quaternion.identity;
            }
            else
            {
                speed = 0;
            }
            
        }
        if (toEqual)
        {
            UpdateLevers();
            if (((int) this.transform.rotation.eulerAngles.z == 0 || (int) this.transform.rotation.eulerAngles.z == 359) && m_leftWeight != 0)
            {                
                Utils.EventManager<Vector2Int>.Trigger("CheckWeights", new Vector2Int(m_leftWeight, m_rightWeight));
            }

            if (this.transform.rotation.eulerAngles.z > 334 && (int) this.transform.rotation.eulerAngles.z != 360)
            {
                if (speed < maxSpeed)
                {
                    speed = speed + acceleration * Time.deltaTime;
                }
                this.transform.RotateAround(rotatingDot.transform.position, new Vector3(0, 0, 1), speed * Time.deltaTime);
            }
            else if (this.transform.rotation.eulerAngles.z < 26 && (int) this.transform.rotation.eulerAngles.z != 0)
            {
                if (speed < maxSpeed)
                {
                    speed = speed + acceleration * Time.deltaTime;
                }
                this.transform.RotateAround(rotatingDot.transform.position, new Vector3(0, 0, -1), speed * Time.deltaTime);
            }
            else
            {
                speed = 0;
            }
        }
        if (m_rightWeight * rightLength == m_leftWeight * leftLength)
        {
            toEqual = true;
            toLeft = false;
            toRight = false;
        }
        if (m_rightWeight * rightLength < m_leftWeight * leftLength)
        {
            toEqual = false;
            toLeft = true;
            toRight = false;
        }
        if (m_rightWeight * rightLength > m_leftWeight * leftLength)
        {
            toEqual = false;
            toLeft = false;
            toRight = true;
        }
    }

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
        rotatingDot = GameObject.Find("Dot");
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
        //Utils.EventManager<Vector2Int>.Trigger("CheckWeights", new Vector2Int(m_leftWeight, m_rightWeight));
    }

    private void AddLeft(Cargo cargo)
    {
        m_leftCargos.Add(cargo);
        m_leftWeight += cargo.weight;
        UpdateLevers();
        //Utils.EventManager<Vector2Int>.Trigger("CheckWeights", new Vector2Int(m_leftWeight, m_rightWeight));
    }

    private void UpdateLevers()
    {
        Vector3 pos = m_leftCargosParent.position;
        leftLength = GameObject.Find("Game").GetComponent<GameManager>()._leftLeverCount;
        rightLength = GameObject.Find("Game").GetComponent<GameManager>()._rightLeverCount;
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
        //Utils.EventManager<Vector2Int>.Trigger("CheckWeights", new Vector2Int(m_leftWeight, m_rightWeight));
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
