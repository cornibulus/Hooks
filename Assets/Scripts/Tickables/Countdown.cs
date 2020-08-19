using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : Tickable {

    public NumberDisplay numberDisplay;
    public UnityEvent timeUpEvent = new UnityEvent();
    private bool hasFired = false;

    public override void Tick()
    {
        if(numberDisplay.number > 0)
        {
            numberDisplay.number--;
        }
    }

    public void Update()
    {
        if(!hasFired && numberDisplay != null && numberDisplay.number <= 0)
        {
            hasFired = true;
            if (MusicManager.Instance != null)
                MusicManager.Instance.PlayWinAudio();

            Player.Instance.InterruptChildCoroutines();

            timeUpEvent.Invoke();
        }
    }
}
