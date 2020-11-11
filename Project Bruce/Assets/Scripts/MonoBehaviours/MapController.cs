using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;
public class MapController : MonoBehaviour
{
    public List<HexComponent> HexComponents;
    public List<GameObject> MapObjects;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void Init()
    {

    }

    private void Update()
    {
        foreach(HexComponent hexComp in HexComponents)
        {
        //    float noiseValue = Noise.GenerateNoiseMap(10, 10, 0, ElevationScale, 8, 12, 12, Vector3.zero)[hexComp.Hex.gridX, hexComp.Hex.gridZ];
        //    CurrentMap.AssignHexSoilTypeBasedOnNoise(hexComp.Hex, noiseValue);
        //    SetHexColor_Soil(hexComp);

            foreach (SpaceGridObject gridObject in hexComp.ObjectGrid)
            {
                Vector3 newPosition = gridObject.Position * GridScale;
                if(gridObject.GO != null)
                {

                    gridObject.GO.transform.localScale = new Vector3(GridObjectScale, GridObjectScale, GridObjectScale);
                    gridObject.GO.transform.localPosition = newPosition;
                }
            }
        }

        
    }

    [Range(.001f, 1f)]
    public float GridScale = 0.135f;
    [Range(.001f, 1f)]
    public float GridObjectScale = 0.4f;
    [Range(.0001f,1f)]
    public float ElevationScale = 0.6f;

    public HexComponent HexPrefab;
    public Transform MapGraphicsHolder;
    public static MapController Instance;

    public GameObject Tree1Prefab;
    public GameObject Tree2Prefab;
    public GameObject SettlementPrefab;

    public Color ClayColor;
    public Color SandColor;
    public Color LoamColor;


    public Hex CenterHex
    {
        get { return CurrentMap.HexGrid[CurrentMap.Width / 2, CurrentMap.Height / 2]; }
    }
    public Map CurrentMap;
    public void GenerateMapGraphics(Map map)
    {
        CurrentMap = map;

        foreach (Hex hex in map.HexGrid)
        {
            HexComponent hexComp = Instantiate(HexPrefab, MapGraphicsHolder);
            hexComp.Init(hex);
            hexComp.GetComponentInChildren<TextMesh>().text = string.Format("SV: {0} \n {1},{2}", hex.soilValue.ToString("0.00"), hex.gridX, hex.gridZ);
            SetHexColor_Soil(hexComp);
            HexComponents.Add(hexComp);

            if(hex.Settlement != null)
            {
                OnSettlementPassed(hex.Settlement);
                SetHexColor_Territory(hexComp);
            }

            LineRenderer lineRenderer = hexComp.GetComponentInChildren<LineRenderer>();
            lineRenderer.material.SetColor("_Color", Color.grey);

        }

        InstantiateTrees();
    }

    public void ClearMapGraphics()
    {
        for (int i = 0; i < HexComponents.Count; i++)
        {
            Destroy(HexComponents[i]);
        }
        HexComponents = new List<HexComponent>();
    }

    public HexComponent GetHexComponentFromHex(Hex hex)
    {
        foreach (HexComponent hexComp in HexComponents)
        {
            if(hexComp.Hex == hex)
            {
                return hexComp;
            }
        }

        return null;
    }

    public void OnOwnerChanged(Hex hex)
    {
        HexComponent hexComp = GetHexComponentFromHex(hex);
        if(hexComp == null)
        {
            return;
        }

        SetHexColor_Territory(hexComp);
    }

    public void OnSettlementPassed(Settlement settlement)
    {
        HexComponent hexComp = GetHexComponentFromHex(settlement.hex);

        GameObject campMapObject = Instantiate(SettlementPrefab, hexComp.MapObjectHolder);

        campMapObject.transform.position = hexComp.Hex.Position;
        MapObjects.Add(campMapObject);
    }

    public void InstantiateTrees()
    {
        foreach (HexComponent hexComp in HexComponents)
        {
            for (int i = 0; i < hexComp.Hex.Ecosystem.Trees[TreeBreed.Oak]; i++)
            {
                int randX = Random.Range(0, hexComp.ObjectGrid.GetLength(0));
                int randY = Random.Range(0, hexComp.ObjectGrid.GetLength(1));
                
                RecursivelyFillPlantGrid(hexComp, randX, randY, Tree1Prefab);
            }
            for (int i = 0; i < hexComp.Hex.Ecosystem.Trees[TreeBreed.Apple]; i++)
            {
                int randX = Random.Range(0, hexComp.ObjectGrid.GetLength(0));
                int randY = Random.Range(0, hexComp.ObjectGrid.GetLength(1));

                RecursivelyFillPlantGrid(hexComp, randX, randY, Tree2Prefab);
            }
        }
    }

    void RecursivelyFillPlantGrid(HexComponent hexComp, int randX, int randY, GameObject Prefab)
    {
        if (hexComp.ObjectGrid[randX, randY].GO == null)
        {
            GameObject newObject = Instantiate(Prefab, hexComp.transform);
            newObject.name = string.Format("({0},{1})", randX, randY);
            newObject.transform.localPosition = hexComp.ObjectGrid[randX, randY].Position;
            MapObjects.Add(newObject);
            hexComp.ObjectGrid[randX, randY].GO = newObject.gameObject;
        }
        else
        {
            if (randX >= hexComp.ObjectGrid.GetLength(0) - 1)
            {
                randX--;
            }
            else if (randX <= 0)
            {
                randX++;
            }
            else
            {
                int randXoffSet = Random.Range(-1, 2);
                randX += randXoffSet;
            }

            if (randY >= hexComp.ObjectGrid.GetLength(1) - 1)
            {
                randY--;
            }
            else if (randY <= 0)
            {
                randY++;
            }
            else
            {
                int randYoffSet = Random.Range(-1, 2);
                randY += randYoffSet;
            }
            RecursivelyFillPlantGrid(hexComp, randX, randY, Prefab);
        }
    }

    bool debugText = true;
    public void ToggleDebugText()
    {
        debugText = !debugText;
        foreach(HexComponent hexComp in HexComponents)
        {
            hexComp.DebugText.gameObject.SetActive(debugText);
        }
    }

    void SetHexColor_Soil(HexComponent hexComp)
    {
        var block = new MaterialPropertyBlock();

        switch (hexComp.Hex.Terrain.SoilType)
        {
            case SoilType.Clay:
                block.SetColor("_Color", ClayColor);
                break;
            case SoilType.Sand:
                block.SetColor("_Color", SandColor);
                break;
            case SoilType.Loam:
                block.SetColor("_Color", LoamColor);
                break;
            default:
                block.SetColor("_Color", Color.magenta);
                break;
        }

        hexComp.GetComponentInChildren<MeshRenderer>().SetPropertyBlock(block);
    }

    void SetHexColor_Territory(HexComponent hexComp)
    {
        var block = new MaterialPropertyBlock();

        if (hexComp.Hex.Owner != null)
        {
            block.SetColor("_Color", hexComp.Hex.Owner.mapColor);
            hexComp.GetComponentInChildren<MeshRenderer>().SetPropertyBlock(block);
        }
    }
}
