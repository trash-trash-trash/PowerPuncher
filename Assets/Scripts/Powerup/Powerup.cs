using System;
using System.Collections.Generic;

[Serializable]
public class Powerup
{
    public PowerupsEnum powerupType;
    public string powerupName;
    public string powerupDescription;
    public List<float> effectAmount;
}