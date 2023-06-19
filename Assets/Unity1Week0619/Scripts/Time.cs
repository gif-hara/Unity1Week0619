using System.Collections.Generic;

namespace Unity1Week0619
{
    /// <summary>
    /// 時間に関するクラス
    /// </summary>
    public sealed class Time
    {
        private readonly Time parent;

        private readonly List<Time> children = new();

        private float _timeScale = 1.0f;

        public float timeScale
        {
            set
            {
                this._timeScale = value;
                MessageBroker.GetPublisher<Time, TimeEvents.UpdatedTimeScale>()
                    .Publish(this, TimeEvents.UpdatedTimeScale.Get());
                foreach (var child in this.children)
                {
                    MessageBroker.GetPublisher<Time, TimeEvents.UpdatedTimeScale>()
                        .Publish(child, TimeEvents.UpdatedTimeScale.Get());
                }
            }
            get => this._timeScale;
        }

        public float totalTimeScale => this.GetTimeScaleRecursive(1.0f);

        private float GetTimeScaleRecursive(float value)
        {
            if (this.parent != null)
            {
                return this.parent.GetTimeScaleRecursive(value * this.timeScale);
            }
            else
            {
                return value * this.timeScale;
            }
        }

        public float deltaTime => UnityEngine.Time.deltaTime * this.totalTimeScale;

        public Time(Time parent = null)
        {
            this.parent = parent;
            this.parent?.children.Add(this);
        }

        ~Time()
        {
            this.parent?.children.Remove(this);
        }
    }
}
