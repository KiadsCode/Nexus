using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.Framework
{
    public class GameComponentsCollection : IEnumerable, IEnumerable<GameComponent>, ICollection,
        ICollection<GameComponent>
    {
        #region Properties

        private ICollection _collectionOfItems => _items;

        private ICollection<GameComponent> _collectionOfT => _items;
        private IEnumerable<GameComponent> _enumerableT => _items;

        #endregion

        #region Fields

        public bool IsFixedSize = false;

        private int _currentElement;
        private int _size;
        private static readonly GameComponent[] s_emptyArray = new GameComponent[0];
        private GameComponent[] _items;

        #endregion

        public EventHandler<EventArgs> OnComponentAdded;

        public GameComponentsCollection()
        {
            _currentElement = 0;
            _size = 0;
            _items = new GameComponent[_size];
        }

        public GameComponentsCollection(int size)
        {
            _currentElement = 0;
            _size = size;
            _items = new GameComponent[_size];
        }

        public GameComponent this[int id]
        {
            get => _items[id];
            set => _items[id] = value;
        }

        public GameComponent[] ToArray()
        {
            if (_size == 0)
                return s_emptyArray;
            var array = new GameComponent[_size];
            Array.Copy(_items, array, _size);
            return array;
        }

        public void Add(GameComponent item)
        {
            if (_currentElement >= _size && IsFixedSize == false)
                _size++;
            var _itemsCopy = new GameComponent[_size];
            for (var i = 0; i < _items.Length; i++)
                _itemsCopy[i] = _items[i];
            _itemsCopy[_currentElement] = item;
            _items = _itemsCopy.ToArray();
            _currentElement++;
            item.Initialize();
        }

        public void Clear()
        {
            _currentElement = 0;
            _size = 0;
            _items = new GameComponent[_size];
        }

        public IEnumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator<GameComponent> IEnumerable<GameComponent>.GetEnumerator()
        {
            return _enumerableT.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            _items.CopyTo(array, index);
        }

        public bool Contains(GameComponent item)
        {
            return _collectionOfT.Contains(item);
        }

        public void CopyTo(GameComponent[] array, int arrayIndex)
        {
            _collectionOfT.CopyTo(array, arrayIndex);
        }

        public bool Remove(GameComponent item)
        {
            return _collectionOfT.Remove(item);
        }

        public int Count => _collectionOfItems.Count;
        public bool IsSynchronized => _items.IsSynchronized;
        public object SyncRoot => _items.SyncRoot;
        public bool IsReadOnly => _collectionOfT.IsReadOnly;

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            var stringTypeOfT = typeof(GameComponent).ToString();
            return stringTypeOfT;
            //return $"{{Size: {Count}, IsFixedSize: {IsFixedSize}, TypeOfElements: {stringTypeOfT} }}";
        }
    }
}