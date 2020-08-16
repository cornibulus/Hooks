using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : Tickable {

    public NumberDisplay numberDisplay;
    public UnityEvent timeUpEvent = new UnityEvent();

    public override void Tick()
    {
        if(numberDisplay.number > 0)
        {
            numberDisplay.number--;
        }
    }

    public void Update()
    {
        if(numberDisplay != null && numberDisplay.number <= 0)
        {
            if (MusicManager.Instance != null)
                MusicManager.Instance.PlayWinAudio();

            Player.Instance.InterruptChildCoroutines();

            timeUpEvent.Invoke();
        }
    }
}
