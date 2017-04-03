using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DevNetPbt
{
    internal class MySet<T> : ISet<T>
    {
        private IList<T> _items = new List<T>();
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item)
        {
        }

        void ISet<T>.UnionWith(IEnumerable<T> other)
        {
            var self = this as ISet<T>;
            foreach (var i in other)
                self.Add(i);
        }

        void ISet<T>.IntersectWith(IEnumerable<T> other)
        {
            var list = _items.Intersect(other).ToList();
            _items = list;
        }

        void ISet<T>.ExceptWith(IEnumerable<T> other)
        {
            var list = _items.Except(other).ToList();
            _items = list;
        }

        void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        bool ISet<T>.IsSubsetOf(IEnumerable<T> other) => !_items.Except(other).Any();

        bool ISet<T>.IsSupersetOf(IEnumerable<T> other) => !other.Except(_items).Any();

        bool ISet<T>.IsProperSupersetOf(IEnumerable<T> other) => !other.Except(_items).Any();

        bool ISet<T>.IsProperSubsetOf(IEnumerable<T> other) => !_items.Except(other).Any();

        public bool Overlaps(IEnumerable<T> other) => _items.Any(other.Contains);

        public bool SetEquals(IEnumerable<T> other) => throw new NotImplementedException();

        bool ISet<T>.Add(T item)
        {
            if (_items.Contains(item))
                return false;
            _items.Add(item);
            return true;
        }

        void ICollection<T>.Clear() => _items.Clear();

        bool ICollection<T>.Contains(T item) => _items.Contains(item);

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        bool ICollection<T>.Remove(T item) => _items.Remove(item);

        int ICollection<T>.Count => _items.Count;
        bool ICollection<T>.IsReadOnly => _items.IsReadOnly;
    }
}