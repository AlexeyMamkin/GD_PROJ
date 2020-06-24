using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int _leftLeverCount;
    [SerializeField] private int _rightLeverCount;
    [SerializeField] private int _basicCargoWeight;
    [SerializeField] private Sprite _leverSprite3;
    [SerializeField] private Sprite _leverSprite4;
    [SerializeField] private Sprite _leverSprite5;
    [SerializeField] private Sprite _leverSprite6;
    [SerializeField] private Sprite _leverSprite7;
    [SerializeField] private Sprite _leverSprite8;
    [SerializeField] private Sprite _leverSprite9;

    private Lever m_lever;
    private Stand m_stand;
    private CargoPanel m_cargoPanel;
    private Mode m_mode = Mode.Normal;
    private int m_level = 0;


    private void OnEnable()
    {
        Utils.EventManager<Vector2Int>.StartListening("CheckWeights", CheckWeights);
        Utils.EventManager.StartListening("NormalMode", SetNormalMode);
        Utils.EventManager.StartListening("HardMode", SetHardMode);
        Utils.EventManager.StartListening("LoadNextLevel", LoadNextLevel);
        Utils.EventManager.StartListening("LoadCurrentLevel", RestartCurrentLevel);
    }

    private void OnDisable()
    {
        Utils.EventManager<Vector2Int>.StopListening("CheckWeights", CheckWeights);
        Utils.EventManager.StopListening("NormalMode", SetNormalMode);
        Utils.EventManager.StopListening("HardMode", SetHardMode);
        Utils.EventManager.StopListening("LoadNextLevel", LoadNextLevel);
        Utils.EventManager.StopListening("LoadCurrentLevel", RestartCurrentLevel);
    }

    private void Start()
    {
        m_lever = GameObject.Find("Lever").GetComponent<Lever>();
        m_stand = GameObject.Find("Stand").GetComponent<Stand>();
        m_cargoPanel = GameObject.Find("CargoPanel").GetComponent<CargoPanel>();
        
        m_level = 1;
    }

    private void Init(int leftLeverCount, int rightLeverCount, int basicCargoWeight)
    {
        if (leftLeverCount == rightLeverCount)
        {
            Debug.Log("Unexpected input data!");
        }
        else
        {
            SetSprite(leftLeverCount + rightLeverCount);
            m_stand.SetPositionFor(leftLeverCount, rightLeverCount);
            m_lever.SetBasicCargo(basicCargoWeight);
            m_cargoPanel.SetCargosFor(basicCargoWeight, leftLeverCount, rightLeverCount);
        }
    }

    private void Init(Mode mode)
    {
        int leftLeverCount;
        int rightLeverCount;
        int basicCargoWeight;

        do
        {
            List<int> list = GetValuesFor(mode);
            int i;
            if (mode == Mode.Hard)
            {
                i = Random.Range(0, list.Count);
                leftLeverCount = list[i];
                list.RemoveAt(i);
                i = Random.Range(0, list.Count);
                rightLeverCount = list[i];
                basicCargoWeight = Random.Range(1, 3);
            }
            else
            {
                i = Random.Range(0, list.Count);
                leftLeverCount = list[i];
                list.RemoveAt(i);
                i = Random.Range(0, list.Count);
                rightLeverCount = list[i];
                basicCargoWeight = Random.Range(1, 3);
            }
        }
        while (leftLeverCount == rightLeverCount);

        Debug.Log(leftLeverCount + " <- l " + rightLeverCount + " <- r " + basicCargoWeight + " <- b ");
        _leftLeverCount = leftLeverCount;
        _rightLeverCount = rightLeverCount;
        _basicCargoWeight = basicCargoWeight;

        SetSprite(leftLeverCount + rightLeverCount);
        m_stand.SetPositionFor(leftLeverCount, rightLeverCount);
        m_lever.SetBasicCargo(basicCargoWeight);
        m_cargoPanel.SetCargosFor(basicCargoWeight, leftLeverCount, rightLeverCount);

        Utils.EventManager.Trigger("StartGame");
        Utils.EventManager<int>.Trigger("ShowLevel", m_level);
    }

    private void InitCurr()
    {
        SetSprite(_leftLeverCount + _rightLeverCount);
        m_stand.SetPositionFor(_leftLeverCount, _rightLeverCount);
        m_lever.SetBasicCargo(_basicCargoWeight);
        m_cargoPanel.SetCargosFor(_basicCargoWeight, _leftLeverCount, _rightLeverCount);

        Utils.EventManager.Trigger("StartGame");
        Utils.EventManager<int>.Trigger("ShowLevel", m_level);
    }

    private List<int> GetValuesFor(Mode mode)
    {
        List<int> list = new List<int>();
        if (mode == Mode.Hard)
        {
            list.Add(3);
            list.Add(4);
            list.Add(5);
        }
        else
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);
        }
        return list;
    }

    private void CheckWeights(Vector2Int weights)
    {
        int leftStrength = weights.x * _leftLeverCount;
        int rightStrength = weights.y * _rightLeverCount;

        Debug.Log(leftStrength + " on the left " + rightStrength + " on the right");

        if (leftStrength == rightStrength)
        {
            Utils.EventManager.Trigger("Win");
            Debug.Log(m_level + " is passed");
        }
    }

    private void SetSprite(int leverCount)
    {
        switch (leverCount)
        {
            case 3:
                m_lever.SetSprite(_leverSprite3);
                break;
            case 4:
                m_lever.SetSprite(_leverSprite4);
                break;
            case 5:
                m_lever.SetSprite(_leverSprite5);
                break;
            case 6:
                m_lever.SetSprite(_leverSprite6);
                break;
            case 7:
                m_lever.SetSprite(_leverSprite7);
                break;
            case 8:
                m_lever.SetSprite(_leverSprite8);
                break;
            case 9:
                m_lever.SetSprite(_leverSprite9);
                break;
            default:
                Debug.Log("You know that moment I'm foked up.");
                break;
        }

    }

    private void SetNormalMode()
    {
        m_level = 1;
        m_mode = Mode.Normal;
        Init(m_mode);
    }

    private void SetHardMode()
    {
        m_level = 1;
        m_mode = Mode.Hard;
        Init(m_mode);
    }

    private void LoadNextLevel()
    {
        Utils.EventManager.Trigger("ClearField");
        m_level++;
        Init(m_mode);
        Debug.Log("next level");
    }

    private void RestartCurrentLevel()
    {
        Utils.EventManager.Trigger("ClearField");
        InitCurr();
        Debug.Log("current level");
    }
}
