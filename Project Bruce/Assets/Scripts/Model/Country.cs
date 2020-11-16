using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Country : IPopulationContainer
    {
        public Country()
        {
            mapColor = new Color(Random.value, Random.value, Random.value);
            Population = new Population(this, null);
            UnitManager = new CountryUnitManager(this);
            Settlements = new List<Settlement>();

            Population.RegisterOnPopAdded(OnPopAdded);
            Population.RegisterOnPopRemoved(OnPopRemoved);
        }




        public string Name;
        public FlagData flagData;
        public Color mapColor;

        public CountryUnitManager UnitManager;
        public Population Population { get; set; }
        public List<Settlement> Settlements;

        public void RefreshPopulation()
        {
            //Debug.Log("Country Refresh");
            Population.RefreshPopulation();

            foreach (Pop unitPop in UnitManager.Population.Pops)
            {
                foreach(Settlement settlement in Settlements)
                {
                    Debug.Log(settlement);
                    if (settlement.Population.Pops.Contains(unitPop))
                    {
                        //Debug.Log("RemovePop");
                        settlement.RemovePop(unitPop);
                    }
                }
            }
        }
        

        public void Tick()
        {
            UnitManager.Tick();
            foreach (Settlement settlement in Settlements)
            {
                settlement.Tick();
            }
            RefreshPopulation();

        }

        public void OnPopAdded(Pop pop)
        {
            RefreshPopulation();
        }
        public void OnPopRemoved(Pop pop)
        {
            RefreshPopulation();

        }

        public bool isActive()
        {
            return Settlements.Count > 0;
        }

        public void AddSettlement(Settlement settlement)
        {
            Settlements.Add(settlement);
            this.Population.subPops.Add(settlement.Population);
        }

        public void RemoveSettlement(Settlement settlement)
        {
            Settlements.Remove(settlement);
            this.Population.subPops.Remove(settlement.Population);
        }

        
    }

    public class CountryUnitManager : IPopulationContainer
    {
        public CountryUnitManager(Country country)
        {
            this.Country = country;
            Units = new HashSet<MapUnit>();
            Population = new Population(this, Country.Population);

            Population.RegisterOnPopAdded(OnPopAdded);
            Population.RegisterOnPopRemoved(OnPopRemoved);

            country.Population.subPops.Add(this.Population);
        }

        public Country Country;
        public Population Population { get; set; }
        HashSet<MapUnit> Units;

        public void AddUnit(MapUnit unit)
        {
            Units.Add(unit);
            if (unit is PopUnit popUnit)
            {
                Population.AddPop(popUnit.Pop);
            }
        }

        public void RemoveUnits(MapUnit unit)
        {
            Units.Remove(unit);
            if (unit is PopUnit popUnit)
            {
                Population.RemovePop(popUnit.Pop);
            }
        }

        public void Tick()
        {
            foreach (MapUnit unit in Units)
            {
                unit.Tick();
            }
        }

        public void CreateUnitFromPop(Pop pop, Hex hex)
        {
            PopUnit unit = new PopUnit(Country, hex);

            unit.Pop = pop;

            AddUnit(unit);
        }

        public void OnPopAdded(Pop pop) 
        {
            RefreshPopulation();
        }
        public void OnPopRemoved(Pop pop) 
        {
            RefreshPopulation();
        }

        public void RefreshPopulation()
        {
            Population.RefreshPopulation();
        }
    }

    public class FlagData
    {
        public int color1;
        public int color2;
        public int color3;
        public int background;
    }
}