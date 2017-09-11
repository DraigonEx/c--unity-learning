using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using thelab.mvc;

public class Turn : Model<TileApp>
{
    public Locations CurrentPlay { get; set; }

    public Turn()
    {
        CurrentPlay = Locations.NONE;

    }
}
