using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace FileExplorerMultiTask.Models
{
    public class EntityList<T> : IList<T>
    {
        private IList<T> _elist = new List<T>();
        public T this[int index] { get => _elist[index]; set => _elist[index] = value; }

        public int Count => _elist.Count;

        public bool IsReadOnly => _elist.IsReadOnly;

        public void Add(T item)
        {
            _elist.Add(item);
        }

        public void Clear()
        {
            _elist.Clear();
        }

        public bool Contains(T item)
        {
            return _elist.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _elist.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _elist.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _elist.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _elist.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _elist.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _elist.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elist.GetEnumerator();
        }
    }
}
