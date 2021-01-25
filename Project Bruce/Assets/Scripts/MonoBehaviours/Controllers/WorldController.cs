using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bruce;
public class WorldController : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        currentMap = new Map();
        world = new World(currentMap);
        playerCountry = world.Countries.First();
        currentMap = world.Map;
        mapController.Init();
        unitController.Init();
        mapController.GenerateMapGraphics(currentMap);

        playerCountry.RefreshPopulation();



        Action settlementSetter = () => { UI.SelectedSettlement = playerCountry.Settlements.First(); };
        UIController.Instance.UISettlement.Initialize(settlementSetter);

        CreateUnitButton.onClick.AddListener(() => { CreateUnit(); });
        world.SpawnStartAnimals();

        Debug.Log(string.Format("{0},{1}", playerCountry.Settlements.First().hex.gridX, playerCountry.Settlements.First().hex.gridZ));

        if (world.Nature.UnitManager.Units.First() is AnimalUnit animalUnit)
        {
            animalUnit.EvadedHunt();
        }
    }

    public void CreateUnit()
    {
        int rand = 0;

        Pop selected = playerCountry.Settlements.First().Population.Pops.ToList()[rand];

        playerCountry.UnitManager.CreateUnitFromPop(selected, playerCountry.Settlements.First().hex);
    }

    private void Update()
    {
        UIController.RefreshUI(UIController.Instance.UIGameTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            dummyTimer = 0;
        }
        if (dummyTimer <= 0)
        {

            world.Tick();

            Action settlementSetter = () => { UI.SelectedSettlement = playerCountry.Settlements.First(); };
            UIController.Instance.UISettlement.Initialize(settlementSetter);
            dummyTimer = .2f;
        }
        
        //dummyTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.B))
        {
            Settlement first = world.Countries.First().Settlements.First();

            first.BeginConstructBuilding(BuildingFactory.NewFirePit);

            Debug.Log(world.CalendarSystem.DateString() + ":" + world.CalendarSystem.GetFutureDateString(365));
        }
    }

    public Map currentMap;
    public Country playerCountry;
    public Button CreateUnitButton;

    float dummyTimer = .2f;

    public MapController mapController;
    public UnitController unitController;
    public World world;
    public static WorldController Instance;

}
