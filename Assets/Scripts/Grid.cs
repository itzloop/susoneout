using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    private GridLayoutGroup _layout;

    private void Awake()
    {
        _layout = GetComponent<GridLayoutGroup>();
    }

    public void SetGridSize(int n)
    {
        _layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _layout.constraintCount = n;
    }
}