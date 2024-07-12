using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public int KingPear;
    public bool netHasBeenSpawned;
    public float netSpawnedTime;


    public void Clear()
    {
        KingPear = 0;
        netHasBeenSpawned = false;
    }
}
