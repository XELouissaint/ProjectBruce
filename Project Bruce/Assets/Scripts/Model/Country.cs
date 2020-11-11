using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Country
    {
        public Country()
        {
            mapColor = new Color(Random.value, Random.value, Random.value);
            Units = new HashSet<Unit>();
            Population = new Population();
            Settlements = new List<Settlement>();
        }

        
        public void RefreshPopulation()
        {
            Debug.Log("Country Refresh");
            Population.RefreshPopulation();
        }
        


        public string Name;
        public FlagData flagData;
        public Color mapColor;
        public Population Population;
        public List<Settlement> Settlements;
        public HashSet<Unit> Units;

        public void Tick()
        {
            foreach (Unit unit in Units)
            {
                unit.Tick();
            }

            foreach (Settlement settlement in Settlements)
            {
                settlement.Tick();
            }
        }

        public bool isActive()
        {
            return Settlements.Count > 0;
        }

        public void AddSettlement(Settlement settlement)
        {
            Settlements.Add(settlement);
            settlement.Population.superPop = this.Population;
            this.Population.subPops.Add(settlement.Population);
        }

        public void RemoveSettlement(Settlement settlement)
        {
            Settlements.Remove(settlement);
            this.Population.subPops.Remove(settlement.Population);
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