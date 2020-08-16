using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Tickable {

    public LayerMask enemyLayerMask;

    public LayerMask keyLayerMask;

    public UnityEvent gameOverEvent = new UnityEvent();

    public int Keys { get; set; }
    public NumberDisplay keyDisplay;

    void Start()
    {
        Keys = 0;
    }

    public override void Tick()
    {
        Collider2D enemy = Physics2D.OverlapPoint(
               new Vector2(transform.position.x, transform.position.y),
               enemyLayerMask);

        if(enemy != null)
        {
            if (MusicManager.Instance != null)
                MusicManager.Instance.PlayFailAudio();

            gameOverEvent.Invoke();
        }

        Collider2D key = Physics2D.OverlapPoint(
               new Vector2(transform.position.x, transform.position.y),
               keyLayerMask);

        if(key != null)
        {
            Destroy(key.gameObject);
            Keys++;
        }
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
