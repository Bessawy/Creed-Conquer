// * Created by Senior Mido
// * Copyright © 2018-2019
// * Dark|Light - Project

namespace System.Threading
{
    public class TimerRule
    {
        internal Action<int> action_0;
        internal int int_0;
        internal bool bool_0;
        internal ThreadPriority threadPriority_0;

        public TimerRule(Action<int> action, int period, ThreadPriority priority = ThreadPriority.Normal)
        {
            this.action_0 = action;
            this.int_0 = period;
            this.bool_0 = true;
            this.threadPriority_0 = priority;
        }

        ~TimerRule()
        {
            this.action_0 = (Action<int>)null;
        }
    }
}