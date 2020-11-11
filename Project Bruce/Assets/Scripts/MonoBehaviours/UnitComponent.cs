using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;

public class UnitComponent : MonoBehaviour
{
    public Unit Unit;

    bool moved = false;

    public float speed = 4;
    public Vector3 newPosition;
    private void Start()
    {
        newPosition = transform.position;
    }

    private void Update()
    {
        if (moved)
        {
            transform.position = Vector3.Lerp(this.transform.position, newPosition, speed * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position,newPosition) <= .1)
        {
            transform.position = newPosition;
            moved = false;
        }
    }

    public void MoveToHex(Hex hex)
    {
        newPosition = hex.Position;
        moved = true;
    }
}
