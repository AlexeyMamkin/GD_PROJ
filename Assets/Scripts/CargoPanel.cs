using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoPanel : MonoBehaviour
{
    [SerializeField] private Cargo cargoPref;
    private List<Cargo> cargos;

    private void Awake()
    {
        cargos = new List<Cargo>();
    }

    private void OnEnable()
    {
        Utils.EventManager<Cargo>.StartListening("RemoveCargo", RemoveCargo);
        Utils.EventManager<Cargo>.StartListening("AddPanel", AddCargo);
        Utils.EventManager.StartListening("ClearField", ClearField);
    }

    private void OnDisable()
    {
        Utils.EventManager<Cargo>.StopListening("RemoveCargo", RemoveCargo);
        Utils.EventManager<Cargo>.StopListening("AddPanel", AddCargo);
        Utils.EventManager.StopListening("ClearField", ClearField);
    }

    private void RemoveCargo(Cargo cargo)
    {
        cargos.Remove(cargo);
        UpdatePanel();
    }

    private void AddCargo(Cargo cargo)
    {
        cargos.Add(cargo);
        UpdatePanel();
    }

    private void UpdatePanel()
    {
        Vector3 pos = transform.position;
        pos.x = -3;

        float offset = SetOffset(cargos.Count);
        if (offset == 0) pos.x = 0;

        for (int i = 0; i < cargos.Count; i++)
        {
            cargos[i].transform.position = pos;
            pos.x += offset;
        }
    }

    private float SetOffset(int count)
    {
        float offset = 0;

        if (cargos.Count == 5)
        {
            offset = 1.5f;
        }
        else if (cargos.Count == 4)
        {
            offset = 2;
        }
        else if (cargos.Count == 3)
        {
            offset = 3;
        }
        else if (cargos.Count == 2)
        {
            offset = 6;
        }

        return offset;
    }

    public void SetCargosFor(int basicCargoWeight, int leftLeverCount, int rightLeverCount)
    {
        Vector3 pos = transform.position;
        pos.y = 0;
        pos.z = 0;
        Cargo cargo;
        pos.x = -3;
        cargo = Instantiate(cargoPref, transform);
        cargo.transform.Translate(pos);
        cargos.Add(cargo);
        pos.x = -1;
        cargo = Instantiate(cargoPref, transform);
        cargo.transform.Translate(pos);
        cargos.Add(cargo);
        pos.x = 1;
        cargo = Instantiate(cargoPref, transform);
        cargo.transform.Translate(pos);
        cargos.Add(cargo);
        pos.x = 3;
        cargo = Instantiate(cargoPref, transform);
        cargo.transform.Translate(pos);
        cargos.Add(cargo);

        int unnecessaryNum = basicCargoWeight * leftLeverCount / rightLeverCount;
        int offset = 1;

        for (int i = 0; i < cargos.Count; i++)
        {
            if ((i + offset != unnecessaryNum) || (basicCargoWeight * leftLeverCount % rightLeverCount != 0))
            {
                cargos[i].weight = i + offset;
                cargos[i].UpdateText();
            }
            else
            {
                offset++;
                cargos[i].weight = i + offset;
                cargos[i].UpdateText();
            }
        }
    }

    private void ClearField()
    {
        for (int i = 0; i < cargos.Count; i++)
        {
            Destroy(cargos[i].gameObject);
        }
        cargos.Clear();
        UpdatePanel();
    }
}
