using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BurnGames.DependencyInjection;

namespace BurnGames.DataAccess
{
    public abstract class Model : ScriptableObject
    {

        /// <summary>
        /// Model's objects collection storage
        /// </summary>
        public static ModelCollection<TModel> Objects<TModel>() where TModel : Model
        {
            return new ModelCollection<TModel>();
        }

        /// <summary>
        /// Model's objects collection storage
        /// </summary>
        public static ModelCollection<Model> Objects(Type modelType)
        {
            return new ModelCollection<Model>(modelType);
        }

        public static TModel NewModelInstance<TModel>() where TModel : Model
        {
            return CreateInstance<TModel>();
        }
        
        [SerializeField] bool deleted;
        public bool Deleted { get => deleted; set => deleted = value; }

        public string Name { get => name; set => name = value; }

        public static bool operator ==(Model model1, Model model2)
        {

            ScriptableObject instance1 = (ScriptableObject)model1;
            ScriptableObject instance2 = (ScriptableObject)model2;

            if(instance1 != null)
            {
                return model1.Equals(model2);
            }
            else if(instance2 != null)
            {
                return model2.Equals(model1);
            }
            else
            {
                return instance1 == instance2;
            }

        }

        public static bool operator !=(Model model1, Model model2)
        {
            return !(model1 == model2);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((Model)obj).Name == Name;

        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void Save()
        {
            Objects(GetType()).Add(this);
        }

    }
}
