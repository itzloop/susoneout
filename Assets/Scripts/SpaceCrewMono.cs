using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SpaceCrewMono : MonoBehaviour
{
    [SerializeField] private Image alive;
    [SerializeField] private Image dead;
    private bool _isSus;
    private bool _hasClicked;
    private SpaceCrew _spaceCrew;

    public Action<bool> onClickAction;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        alive.color = Color.white;
        dead.color = Color.clear;
    }

    public void Init(bool isSus, SpaceCrew spaceCrew)
    {
        _spaceCrew = spaceCrew;
        alive.sprite = _spaceCrew.alive;
        dead.sprite = _spaceCrew.dead;
        
        _isSus = isSus;
    }

    private void OnClick()
    {
        if (_hasClicked) return;
        _hasClicked = true;
       
        alive.color = Color.clear;
        dead.color = Color.white;

        onClickAction?.Invoke(_isSus);
    }
    
}