using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Bruce;

public enum MouseMode
{
    Select,
    ExpandTerritory
}
public class MouseController : MonoBehaviour
{
    public MapController mapController;
    public static MouseController Instance;

    

    public MouseMode mouseMode;
    private void Awake()
    {
        Instance = this;
        mouseMode = MouseMode.Select;
    }
    public MapComponent SelectedObject;
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;


            if (Physics.Raycast(ray, out hitInfo))
            {
                switch (mouseMode)
                {
                    case MouseMode.Select:
                        Select(hitInfo);
                        break;
                    case MouseMode.ExpandTerritory:
                        SelectHexToExpand(hitInfo);
                        break;
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("Target");
                Target(hitInfo);
            }
        }
    }

    void Select(RaycastHit hitInfo)
    {
        if (hitInfo.collider.gameObject.GetComponentInParent<MapComponent>() != null)
        {
            MapComponent mapComp = hitInfo.collider.gameObject.GetComponentInParent<MapComponent>();

            if (mapComp is HexComponent hexComp)
            {
                if (hexComp.Hex.Settlement != null)
                {
                    UI.SelectedSettlement = hexComp.Hex.Settlement;
                }
                UI.SelectedHex = hexComp.Hex;
                SelectedObject = hexComp;

                UIController.DisplayUI(UIController.Instance.UIHex,() => { UI.SelectedHex = hexComp.Hex; });
   
            } 
            else if (mapComp is PopUnitComponent unitComp)
            {
                UI.SelectedUnit = unitComp.Unit;
                SelectedObject = unitComp;
            }
        }
    }

    void Target(RaycastHit hitInfo)
    {
        MapComponent mapComp = hitInfo.collider.gameObject.GetComponentInParent<MapComponent>();

        if (mapComp is AnimalUnitComponent animalUnitComp)
        {
            Debug.Log(animalUnitComp);
            if(SelectedObject is PopUnitComponent popUnitComp)
            {
                Debug.Log("Enqueue");
                popUnitComp.Unit.Orders.Enqueue(UnitOrderFactory.HuntAnimal(popUnitComp.Unit, animalUnitComp.Unit));
            }
        }
    }

    void SelectHexToExpand(RaycastHit hitInfo)
    {
        if (hitInfo.collider.gameObject.GetComponentInParent<HexComponent>() != null)
        {
            HexComponent hexComp = hitInfo.collider.gameObject.GetComponentInParent<HexComponent>();
            if(hexComp.Hex.Owner == null && PotentialExpansions.Contains(hexComp.Hex))
            {
                UI.SelectedSettlement.AddTerritory(hexComp.Hex);
                EndExpandTerritoryMode(UI.SelectedSettlement);
                BeginExpandTerritoryMode(UI.SelectedSettlement);
            }
        }
    }

    List<Hex> PotentialExpansions = new List<Hex>();

    public void BeginExpandTerritoryMode(Settlement settlement)
    {
        mouseMode = MouseMode.ExpandTerritory;
        mapController.SetMapMode(1);
        SelectedObject = null;
        UI.SelectedSettlement = settlement;

        foreach(Hex hexInTerritory in settlement.Territory)
        {
            HexComponent currentHexComp = mapController.GetHexComponentFromHex(hexInTerritory);
            AdjustLineForExpandTerritory(currentHexComp);
            PotentialExpansions.Add(hexInTerritory);
            foreach (Hex neighbor in hexInTerritory.Neighbors())
            {
                if (settlement.Territory.Contains(neighbor))
                {
                    continue;
                }

                HexComponent neighborHexComp = mapController.GetHexComponentFromHex(neighbor);
                AdjustLineForExpandTerritory(neighborHexComp);
                PotentialExpansions.Add(neighbor);
            }
        }
    }

    void AdjustLineForExpandTerritory(HexComponent hexComp)
    {
        if (mouseMode == MouseMode.ExpandTerritory)
        {
            LineRenderer lineRenderer = hexComp.GetComponentInChildren<LineRenderer>();
            lineRenderer.material.SetColor("_Color", Color.yellow);
            lineRenderer.transform.localPosition = new Vector3(lineRenderer.transform.localPosition.x, .06f, lineRenderer.transform.localPosition.z);
        }
    }

    public void EndExpandTerritoryMode(Settlement settlement)
    {
        mouseMode = MouseMode.Select;

        foreach(Hex hex in PotentialExpansions)
        {
            HexComponent hexComp = mapController.GetHexComponentFromHex(hex);

            LineRenderer lineRenderer = hexComp.GetComponentInChildren<LineRenderer>();
            lineRenderer.material.SetColor("_Color", Color.grey);
            lineRenderer.transform.localPosition = new Vector3(lineRenderer.transform.localPosition.x, 0, lineRenderer.transform.localPosition.z);
        }

        PotentialExpansions = new List<Hex>();
    }
}
