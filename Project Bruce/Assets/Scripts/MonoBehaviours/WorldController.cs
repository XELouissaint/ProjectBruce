using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        currentMap = new Map();
        world = new World(currentMap);
        currentMap = world.Map;
        mapController.Init();
        unitController.Init();
        mapController.GenerateMapGraphics(currentMap);
    }
    void Start()
    {

        world.Countries.First().RefreshPopulation();
        UIController.Instance.UIPopulation.Initialize(world.Countries.First().Settlements.First().Population);
        UIController.Instance.UISettlement.Initialize(world.Countries.First().Settlements.First());


        Debug.Log(string.Format("{0},{1}", world.Countries.First().Settlements.First().hex.gridX, world.Countries.First().Settlements.First().hex.gridZ));
    }
    Unit unit;
    bool assigned = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!assigned)
            {
                Settlement settlement = world.Countries.First().Settlements.First();

                Pop randomPop = settlement.Population.Pops.ToList()[World.RNG.Next(settlement.Population.Pops.Count)];
                settlement.JobManager.AddPopToJob(settlement.JobManager.JobDictionary.Keys.First(), randomPop);

                assigned = true;
            }

            world.Tick();

            UIController.Instance.UISettlement.Initialize(world.Countries.First().Settlements.First());

        }
    }

    public Map currentMap;

    public MapController mapController;
    public UnitController unitController;
    public World world;
    public static WorldController Instance;
}
