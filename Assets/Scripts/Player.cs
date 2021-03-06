﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    public LayerMask enemyLayerMask;

    public LayerMask keyLayerMask;

    public UnityEvent gameOverEvent = new UnityEvent();

    public int Keys { get; set; }
    public NumberDisplay keyDisplay;

    private bool isGameOver = false;

    void Start()
    {
        Keys = 0;
        if (this.keyDisplay == null && GameObject.FindGameObjectWithTag("KeyCount") != null)
            this.keyDisplay = GameObject.FindGameObjectWithTag("KeyCount").GetComponent<NumberDisplay>();
    }

    private void FixedUpdate()
    {
        if (isGameOver)
            return;

        Collider2D enemy = Physics2D.OverlapPoint(
        new Vector2(transform.position.x, transform.position.y),
        enemyLayerMask);

        if (enemy != null)
        {
            isGameOver = true;
            if (MusicManager.Instance != null)
                MusicManager.Instance.PlayFailAudio();

            InterruptChildCoroutines();

            gameOverEvent.Invoke();
        }

        Collider2D key = Physics2D.OverlapPoint(
               new Vector2(transform.position.x, transform.position.y),
               keyLayerMask);

        if (key != null)
        {
            Destroy(key.gameObject);
            Keys++;
        }
    }

    public void InterruptChildCoroutines()
    {
        BroadcastMessage("StopAllCoroutines");
    }

    private void Update()
    {
        if (keyDisplay != null)
            keyDisplay.number = Keys;
    }

    //Singleton boilerplate
    private static Player _instance;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
