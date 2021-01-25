using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bruce;

public class WorldController : MonoBehaviour
{
    public int StartCount;
    public int InteractCount;
    public Text TextPrefab;
    public RectTransform VerticalHolder;
    public World World;

    List<GameObject> Prefabs;
    // Start is called before the first frame update
    void Start()
    {
        Prefabs = new List<GameObject>();
        World = new World(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyPrefabs();

            World.Tick();
            foreach (Pop pop in World.Population)
            {
                Text text = Instantiate(TextPrefab, VerticalHolder);
                PersonalGoal goal = pop.GoalSystem.CurrentPersonalGoal;
                string desc = goal != null ? goal.InteractionDescription(pop, goal.PreviousInteraction) : "NO GOAL";
                text.text = string.Format("{0}: {1}: {2}", pop.name, pop.Relations.AverageOpinionOfThis(), desc);
                Prefabs.Add(text.gameObject);
            }
        }

       
    }

    void DestroyPrefabs()
    {
        for(int i = 0; i < Prefabs.Count; i++)
        {
            Destroy(Prefabs[i]);
        }

        Prefabs = new List<GameObject>();
    }
}
