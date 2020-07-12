﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Room : MonoBehaviour {
    private bool solved;

    public bool Solved { get => solved; set { solved = value; if (solved) GameManager.ActivateCorridor(CorridorToActivate); } }

    public int CorridorToActivate { get; set; }

    public GameManager GameManager;
}
