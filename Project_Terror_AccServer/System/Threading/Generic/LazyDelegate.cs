﻿// * Created by Senior Mido
// * Copyright © 2018-2019
// * Dark|Light - Project

namespace System.Threading.Generic
{
    public class LazyDelegate<T> : TimerRule<T>
    {
        public LazyDelegate(Action<T, int> action, int dueTime, ThreadPriority priority = ThreadPriority.Normal)
            : base(action, dueTime, priority)
        {
            this.bool_0 = false;
        }
    }
}