using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Interop.CC.Models.Tests
{
    public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity> where TEntity : class
    {
        ObservableCollection<TEntity> _data;
        IQueryable _query;
        private readonly Func<TEntity, object>[] _identitySelectors;
        public TestDbSet(params Func<TEntity, object>[] identitySelectors)
        {
            _identitySelectors = identitySelectors;
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public override TEntity Add(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
            {
                _data.Add(item);
            }
            return _data.ToList();
        }
        public override TEntity Remove(TEntity item)
        {
            _data.Remove(item);
            return item;
        }


        public override TEntity Attach(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }
        System.Linq.IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }
        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        public override TEntity Find(params object[] keyValues)
        {
            if (this._identitySelectors != null && keyValues != null)
            {
                if (_identitySelectors.Length != keyValues.Length)
                {
                    throw new Exception("Number of identifiers does not match with number of identifiers added to constructor.");
                }


                List<TEntity> list = Local.Select(o => o).ToList();

                for (int i = 0; i < _identitySelectors.Length; i++)
                {
                    list = list.Where(e => _identitySelectors[i].Invoke(e).Equals(keyValues[i])).ToList();
                }

                return list.FirstOrDefault();
            }

            throw new NotImplementedException("Derive from DbSetDouble and override Find.");
        }

        //IQueryProvider IQueryable.Provider
        //{
        //    get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
        //}

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
