using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public int KingPearl;
    public bool netHasBeenSpawned;
    public float netSpawnedTime;


    public void Clear()
    {
        KingPearl = 0;
        netHasBeenSpawned = false;
    }
}
