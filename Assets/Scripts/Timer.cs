using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [field: SerializeField] float TimerSeconds { get; set; }
    [SerializeField] UnityEvent _onTimerEnd;

    public void StartTimer()
    {
        StartTimer(TimerSeconds);
    }

    public void StartTimer(float seconds)
    {
        StartCoroutine(TimerRunner(seconds));

    }

    IEnumerator TimerRunner(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _onTimerEnd.Invoke();
    }
}
