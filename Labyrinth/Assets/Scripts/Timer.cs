using System;
using UnityEngine;
using UniRx;

public class Timer : IDisposable
{
    public float timer;
    private Action actionComplete;
    private float finishTime;
    private bool isRunning;
    private IDisposable observerDisposable;

    public Timer(float finishTime, Action actionComplete)
    {
        this.finishTime = finishTime;
        this.actionComplete = actionComplete;
        observerDisposable = Observable.EveryGameObjectUpdate().
            Where(x => isRunning).Subscribe(_ => Update());

    }
    public void Restart()
    {
        isRunning = true;
        timer = 0.0f;
    }
    public void ShutDown()
    {
        isRunning = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < finishTime)
            return;
        timer = 0.0f;
        //isRunning = false;
        actionComplete();
    }
    public void Dispose()
    {
        observerDisposable.Dispose();
        observerDisposable = null;
        actionComplete = null;
    }
}
