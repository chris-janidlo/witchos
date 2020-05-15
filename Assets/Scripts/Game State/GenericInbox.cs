using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInbox<T>
    where T : class
{
    public struct Entry
    {
        public T Value;
        public bool Read;
    }

    private List<Entry> entries;

    public IReadOnlyList<Entry> Entries => entries.AsReadOnly();

    public int Count => entries.Count;
    public int UnreadCount => entries.Where(i => !i.Read).Count();

    public GenericInbox ()
    {
        entries = new List<Entry>();
    }

    public bool Contains (T item) => entries.Any(i => i.Value == item);

    public void Add (T item, bool read = false)
    {
        if (Contains(item)) return;

        entries.Add(new Entry { Value = item, Read = read });
    }

    public bool Remove (T item)
    {
        if (!Contains(item)) return false;

        entries.RemoveAll(i => i.Value == item);
        return true;
    }

    public bool GetReadState (T item)
    {
        return entries.First(i => i.Value == item).Read;
    }

    public void SetReadState (T item, bool value)
    {
        if (!Contains(item)) throw new InvalidOperationException("Can't set read state on item that doesn't exist");

        int index = entries.FindIndex(i => i.Value == item);
        entries[index] = new Entry { Value = item, Read = value };
    }
}
