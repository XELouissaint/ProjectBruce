using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;

public class UnitComponent : MapComponent
{

    protected Unit baseUnit;

    bool hasMoved = false;
    public float speed = 4;
    public Vector3 newPosition;
    public Vector3 oldPosition;
    public string currentHex;
    private void Start()
    {
        newPosition = transform.position;
        oldPosition = transform.position;


    }

    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        currentHex = baseUnit != null ? string.Format("{0},{1}", baseUnit.CurrentHex.gridX, baseUnit.CurrentHex.gridZ) : string.Empty;


        if (hasMoved)
        {
            transform.position = Vector3.Lerp(this.transform.position, newPosition, speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, newPosition) <= .1)
        {
            transform.position = newPosition;
            hasMoved = false;
        }
    }
    public virtual void BeginMoveToHex(Hex hex)
    {
        newPosition = hex.Position;
        Debug.Log("New Position");
        hasMoved = true;
    }

    
}


