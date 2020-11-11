using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;

public class MouseController : MonoBehaviour
{
    public MapController mapController;
    public static MouseController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                SelectSettlement(hitInfo);
            }
        }
    }

    void SelectSettlement(RaycastHit hitInfo)
    {
        if (hitInfo.collider.gameObject.GetComponentInParent<HexComponent>() != null)
        {
            HexComponent hexComp = hitInfo.collider.gameObject.GetComponentInParent<HexComponent>();

            if(hexComp.Hex.Settlement != null)
            {
                UI.SelectedSettlement = hexComp.Hex.Settlement;
            }
        }
    }

    bool expandingTerritory = false;
    List<Hex> PotentialExpansions = new List<Hex>();

    public void BeginExpandTerritoryMode(Settlement settlement)
    {
        expandingTerritory = true;


        foreach(Hex hexInTerritory in settlement.Territory)
        {
            HexComponent currentHexComp = mapController.GetHexComponentFromHex(hexInTerritory);
            AdjustLineForExpandTerritory(currentHexComp);
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
        if (expandingTerritory)
        {
            LineRenderer lineRenderer = hexComp.GetComponentInChildren<LineRenderer>();
            lineRenderer.material.SetColor("_Color", Color.yellow);
            lineRenderer.transform.localPosition = new Vector3(lineRenderer.transform.localPosition.x, .06f, lineRenderer.transform.localPosition.z);
        }
    }

    public void EndExpandTerritoryMode(Settlement settlement)
    {
        expandingTerritory = false;
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
