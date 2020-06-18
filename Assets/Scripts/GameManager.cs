using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int _leftLeverCount;
    [SerializeField] private int _rightLeverCount;
    [SerializeField] private int _basicCargoWeight;
    [SerializeField] private Sprite _leverSprite3;

    private Lever m_lever;
    private Stand m_stand;
    private CargoPanel m_cargoPanel;


    private void OnEnable()
    {
        Utils.EventManager<Vector2Int>.StartListening("CheckWeights", CheckWeights);
    }

    private void OnDisable()
    {
        Utils.EventManager<Vector2Int>.StopListening("CheckWeights", CheckWeights);
    }

    private void Start()
    {
        m_lever = GameObject.Find("Lever").GetComponent<Lever>();
        m_stand = GameObject.Find("Stand").GetComponent<Stand>();
        m_cargoPanel = GameObject.Find("CargoPanel").GetComponent<CargoPanel>();
        Init(_leftLeverCount, _rightLeverCount, _basicCargoWeight);
    }

    private void Init(int leftLeverCount, int rightLeverCount, int basicCargoWeight)
    {
        if (leftLeverCount == rightLeverCount)
        {
            Debug.Log("Unexpected input data!");
        } 
        else
        {
            if (leftLeverCount + rightLeverCount == 3)
            {
                m_lever.SetSprite(_leverSprite3);
            }
            m_stand.SetPositionFor(leftLeverCount, rightLeverCount);
            m_lever.SetBasicCargo(basicCargoWeight);
            m_cargoPanel.SetCargosFor(basicCargoWeight, leftLeverCount, rightLeverCount);
        }
    }

    private void CheckWeights(Vector2Int weights)
    {
        int leftStrength = weights.x * _leftLeverCount;
        int rightStrength = weights.y * _rightLeverCount;

        if (leftStrength == rightStrength)
        {
            Camera.main.GetComponent<GameEvents>().Win();
            Camera.main.GetComponent<Timer>().SetWin();
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!You win!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }
}
