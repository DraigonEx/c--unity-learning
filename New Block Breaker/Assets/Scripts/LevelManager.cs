using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField] int breakableBlocks;

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    internal void RemoveBreakableBlock()
    {
        breakableBlocks--;
    }
}
