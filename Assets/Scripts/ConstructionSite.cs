using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite
{
    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }

    public SiteLevel Level { get; private set; }
    public TowerType TowerType { get; private set; }

    private GameObject tower;

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        TilePosition = tilePosition;
        WorldPosition = new Vector3(worldPosition.x , worldPosition.y + 0.5f, worldPosition.z);
        tower = null;
    }

    public void SetTower(GameObject obj, SiteLevel level, TowerType type)
    {
        if (tower != null)
        {
            GameObject.Destroy(tower);
        }

        tower = obj;
        Level = level;
        TowerType = type;
    }
}
