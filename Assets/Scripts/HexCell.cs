﻿using UnityEngine;
using EasyButtons;

public class HexCell : MonoBehaviour
{
    [Range(0, 3)]
    public int Elevation;

    public HexCoordinates coordinates;
    public HexMaterial material;

    [HideInInspector, SerializeField]
    HexCell[] neighbors = new HexCell[6];

    internal RectTransform uiRect;
    internal HexGrids grid;
    internal int gridIndex;

    [Button]
    public void Refresh()
    {
        grid.Refresh(this);
    }

    public void SetColor(HexMaterial material)
    {
        this.material = material;

        GetComponent<MeshRenderer>().material = material.GetMaterial();
    }

    public void SetElevation(int elev)
    {
        Elevation = elev;

        Vector3 uiPosition = uiRect.localPosition;
        uiPosition.z = Elevation * -HexMetrics.elevationStep;
        uiRect.localPosition = uiPosition;
    }

    public HexCell GetNeighbor(HexDirection direction)
    {
        if (direction < HexDirection.NE || direction > HexDirection.NW)
            return null;
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

    public override string ToString()
    {
        return name + ", " + coordinates.ToString();
    }
}