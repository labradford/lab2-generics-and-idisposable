using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;

namespace Lab2
{
    public class DynamicArray<T> : IEnumerable<T>, IDisposable where T : new()
    {
        #region fields

        private bool _Disposed = false;
        private T[] _Items;

        #endregion fields

        #region constructors

        public DynamicArray(int size)
        {
            _Items = new T[size];
            Console.WriteLine($"Creating DynamicArray from thread {Thread.CurrentThread.ManagedThreadId}");
        }

        public DynamicArray(IEnumerable<T> items)
        {
            _Items = items.ToArray();
            Console.WriteLine($"Creating DynamicArray from thread {Thread.CurrentThread.ManagedThreadId}");
        }

        #endregion contructors

        #region members

        public T this[int index]
        {
            get

            {
                return _Items[index];
            }
            set

            {
                _Items[index] = value;
            }
        }

        public void Resize(int size)
        {
            T[] items = new T[size];
            for (int i = 0; i < size -1; ++i)
            {
                items[i] = new T();
                if (_Items[i] != null)
                {
                    items[i] = _Items[i];
                }
            }
            _Items = items;
        }

        public int GetLength()
        {
            return _Items.Length;
        }

        #endregion members

        #region IEnumerable Support

        public IEnumerator<T> GetEnumerator()
        {
            return (_Items as IEnumerable<T>).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        #endregion IEnumberable Support

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    _Items = null;
                }
            }
            _Disposed = true;
        }

        // override a finalizer
        ~DynamicArray()
         {
            Console.WriteLine($"Finalizing DyanamicArray from thread {Thread.CurrentThread.ManagedThreadId}");
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Console.WriteLine($"Disposing DynamicArray from thread {Thread.CurrentThread.ManagedThreadId}");
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
