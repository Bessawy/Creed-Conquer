﻿// * Created by Senior Mido
// * Copyright © 2018-2019
// * Dark|Light - Project

namespace System.Threading
{
    public class LazyDelegate : TimerRule
    {
        public LazyDelegate(Action<int> action, int dueTime, ThreadPriority priority = ThreadPriority.Normal)
            : base(action, dueTime, priority)
        {
            this.bool_0 = false;
        }
    }
}