using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Powerup
{
    public PowerupsEnum powerupType;
    public Sprite sprite;
    public int numCharges;
    public string powerupName;
    public string powerupDescription;
    public string effects;
}