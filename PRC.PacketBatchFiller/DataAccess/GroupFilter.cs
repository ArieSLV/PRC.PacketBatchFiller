﻿using System;
using System.Collections.Generic;

namespace PRC.PacketBatchFiller
{
    public class GroupFilter
    {
        private readonly List<Predicate<object>> _filters;

        public Predicate<object> Filter { get; private set; }

        public GroupFilter()
        {
            _filters = new List<Predicate<object>>();
            Filter = InternalFilter;
        }

        private bool InternalFilter(object o)
        {
            foreach (var filter in _filters)
            {
                if (!filter(o))
                {
                    return false;
                }
            }

            return true;
        }

        public void AddFilter(Predicate<object> filter)
        {
            _filters.Add(filter);
        }

        public void RemoveFilter(Predicate<object> filter)
        {
            if (_filters.Contains(filter))
            {
                _filters.Remove(filter);
            }
        }
    }
}