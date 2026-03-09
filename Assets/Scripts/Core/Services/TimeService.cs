using UnityEngine;

namespace overhealer.Core
{
    public class TimeService :
            Service
    {
        public void StopTime()
        {
            SetTimeScale(0f);
        }

        public void StartTime()
        {
            SetTimeScale(1f);
        }

        public void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }
    }
}