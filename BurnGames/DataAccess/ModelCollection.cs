using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BurnGames.DependencyInjection;

namespace BurnGames.DataAccess
{

    public interface ICollectionStrategy<TModel> : ICollection<TModel> where TModel : Model
    {
        void SaveModel(TModel model);
        void OverridesModelType(Type model);
    }

    public class ModelCollection<TModel> : ICollection<TModel>
        where TModel : Model
    {

        private ICollectionStrategy<TModel> strategy;

        private void CheckModelName(TModel model)
        {
            if (model.name == null || model.name?.Trim() == string.Empty)
            {
                model.name = $"New Model ({Guid.NewGuid()})";
            }
        }

        public bool IsReadOnly => strategy.IsReadOnly;

        public int Count
        {
            get
            {
                return strategy.Count;
            }
        }

        public void Add(TModel item)
        {
            if (!Contains(item))
            {
                item.Deleted = false;
                CheckModelName(item);
                strategy.Add(item);
            }
            else
            {
                SaveModel(item);
            }
        }

        public void Clear()
        {
            strategy.Clear();
        }

        public bool Contains(TModel item)
        {
            return strategy.Contains(item);
        }

        public bool Remove(TModel item)
        {
            bool deleted = strategy.Remove(item);
            item.Deleted = deleted;
            item = null;
            return deleted;
        }

        public void CopyTo(TModel[] array, int arrayIndex)
        {
            strategy.CopyTo(array, arrayIndex);
        }

        public IEnumerator GetEnumerator()
        {
            return strategy.GetEnumerator();
        }

        IEnumerator<TModel> IEnumerable<TModel>.GetEnumerator()
        {
            return strategy.GetEnumerator();
        }

        public void ChangeDataStrategy(ICollectionStrategy<TModel> newStrategy)
        {
            strategy = newStrategy;
        }

        public void SaveModel(TModel model)
        {
            CheckModelName(model);
            strategy.SaveModel(model);
        }

        private ICollectionStrategy<TModel> BuildStrategy(Type modelType)
        {

            var dataAttr = modelType.GetCustomAttribute<DataProviderAttribute>();

            if (dataAttr != null)
            {
                strategy = dataAttr.DataProvider.InstantiateAs<ICollectionStrategy<TModel>>();
            }

            else
            {
                strategy = new UnityEditorModelCollection<TModel>();
            }

            return strategy;

        }

        public ModelCollection(Type modelType)
        {
            var implementation = BuildStrategy(modelType);
            implementation.OverridesModelType(modelType);
        }

        public ModelCollection()
        {
            BuildStrategy(typeof(TModel));
        }

    }

}
