using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Utilities.Collections
{
    public class GameObjectsList<T>
    {
        private List<T> items;

        private GameObjectsList(IEnumerable<T> items)
        {
            this.items = new List<T>(items);
        }

        public static GameObjectsList<T> CreateEmpty() => new GameObjectsList<T>(new T[0]);
        public static GameObjectsList<T> Create(IEnumerable<T> items) => new GameObjectsList<T>(items);

        public GameObjectsList<T> Add(T item)
        {
            items.Add(item);
            return Create(items);
        }

        public GameObjectsList<T> Remove(T item)
        {
            items.Remove(item);
            return Create(items);
        }

        public GameObjectsList<T> Where(Func<T, bool> predicate)
        {
            return Create(items.Where(predicate));
        }

        public GameObjectsList<T> IsEmpty(Action onIsEmpty)
        {
            if (items.Count == 0)
                onIsEmpty();
            return this;
        }

        public GameObjectsList<T> ForEach(Action<T> onItem)
        {
            items.ForEach(onItem);
            return this;
        }

        public GameObjectsList<T> RemoveAll(Predicate<T> predicate)
        {
            items.RemoveAll(predicate);
            return Create(items);
        }
    }
}
