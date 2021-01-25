using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;


public class World
{
    public static System.Random RNG = new System.Random();

    public WorldInteractionSystem InteractionSystem;
    public List<Pop> Population;
    public int maxInteractions;
    public World(WorldController wc)
    {
        Population = new List<Pop>();
        maxInteractions = wc.InteractCount;
        InteractionSystem = new WorldInteractionSystem(this);

        for (int i = 0; i < wc.StartCount; i++)
        {
            Pop pop = PopFactory.RandomPop(RNG.Next(8, 47));
            Population.Add(pop);
        }
        foreach (Pop i in Population)
        {
            foreach (Pop j in Population)
            {
                if (i == j)
                {
                    continue;
                }
                i.Relations.ModifyOpinion(j, 50);
            }
        }

    }

    public void Tick()
    {
        InteractionSystem.Tick();

        foreach (Pop pop in Population)
        {
            pop.GoalSystem.Tick();
        }


    }
}
