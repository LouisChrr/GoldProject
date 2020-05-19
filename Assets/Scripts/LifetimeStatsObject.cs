using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LifetimeStatsObject", menuName = "LifetimeStatsObject", order = 1)]
public class LifetimeStatsObject : ScriptableObject
{
    public int LifetimeScore;
    public int LifetimeDistance;
    public int LifetimeCoinsPickedUp;
    public int LifetimeMoney;
    public int LifetimeObstaclesDodged;
    public int LifetimeDeaths;
    public int LifetimeLevelsPassed;


}
