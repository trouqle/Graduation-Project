using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Domain
{
    public class SmartTimer
    {
        float targetTime;
        float currentTime;
        bool isActive;
        public enum Mode { StopWhenDone, Repeating}
        public enum StopMode { ContinueLeft, Reset}
        Mode mode;
        StopMode stopMode;
        public event System.Action OnTimerFinish;
        public System.Func<bool> TickCondition;

        public SmartTimer(float targetTime, Mode mode = Mode.StopWhenDone,System.Func<bool> tickCondition = null,StopMode stopMode = StopMode.ContinueLeft)
        {
            this.targetTime = targetTime;
            this.mode = mode;
            this.stopMode = stopMode;
            this.TickCondition = tickCondition;
        }

        public void Tick(float deltaTime)
        {
            if (!isActive) return;
            if (TickCondition != null && !TickCondition.Invoke())
            {
                if (stopMode == StopMode.Reset) currentTime = 0;
                return;
            }

            currentTime += deltaTime;
            if (currentTime >= targetTime)
            {
                currentTime = 0;
                if (mode == Mode.StopWhenDone) isActive = false;

                OnTimerFinish?.Invoke();
            }
        }

        public void StartTimer()
        {
            isActive = true;
        }
        public void StopTimer()
        {
            isActive = false;
        }
        public void ResetTimer()
        {
            currentTime = 0;
        }
    }
}