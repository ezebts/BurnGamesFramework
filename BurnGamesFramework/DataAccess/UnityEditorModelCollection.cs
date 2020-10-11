using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BurnGames.DataAccess
{

    public class UnityEditorModelCollection<TModel> : ICollectionStrategy<TModel>
        where TModel : Model
    {

        Type modelType;

        public bool IsReadOnly { get; private set; } = false;

        private string GetDataFolderPath()
        {
            return $"{UnityEngine.Application.dataPath}/StaticData/{modelType.Name}";
        }

        private string GetModelPath(TModel model)
        {
            return $"{GetDataFolderPath()}/{model.Name + ".asset"}";
        }

        private string CheckDirectory()
        {
            var realPath = GetDataFolderPath().Replace('/', Path.DirectorySeparatorChar);

            if (!Directory.Exists(realPath))
            {
                Directory.CreateDirectory(realPath);
            }

            return realPath;
        }

        private string RelativeToProject(string fullFilePath)
        {
            var pathStart = fullFilePath.IndexOf("Assets");
            return fullFilePath.Substring(pathStart).Replace(Path.DirectorySeparatorChar, '/');
        }

        private IEnumerable<TModel> LoadModels()
        {

            var assetsRealPath = CheckDirectory();

            var assets = Directory.EnumerateFiles(assetsRealPath);

            foreach(var file in assets)
            {
            
                var asset = AssetDatabase.LoadAssetAtPath<TModel>(RelativeToProject(file));

                if(asset != null)
                {
                    yield return asset;
                }

            }

        }

        private TModel LoadModelAsset(TModel model)
        {

            if(model != null)
            {
                CheckDirectory();
                var modelAsset = GetModelPath(model);
                var modelLoaded = AssetDatabase.LoadAssetAtPath<TModel>(RelativeToProject(modelAsset));
            
                if(modelLoaded != null && (modelLoaded == model))
                {
                    return modelLoaded;
                }
            }

            return null;

        }

        public int Count
        {
            get
            {
                return LoadModels().Count();
            }
        }

        public void Add(TModel item)
        {
            AssetDatabase.CreateAsset(item, RelativeToProject(GetModelPath(item)));
        }

        public void Clear()
        {
            AssetDatabase.DeleteAsset(RelativeToProject(GetDataFolderPath()));
        }

        public bool Contains(TModel item)
        {
            return LoadModelAsset(item) != null;
        }

        public bool Remove(TModel item)
        {

            var model = LoadModelAsset(item);

            if (model != null)
            {
                AssetDatabase.DeleteAsset(RelativeToProject(GetModelPath(model)));
                AssetDatabase.Refresh();
                return true;
            }

            return false;

        }

        public void CopyTo(TModel[] array, int arrayIndex)
        {
            new List<TModel>(LoadModels()).CopyTo(array, arrayIndex);
        }

        public IEnumerator GetEnumerator()
        {
            return LoadModels().GetEnumerator();
        }

        public void OverridesModelType(Type model)
        {
            modelType = model;
        }

        IEnumerator<TModel> IEnumerable<TModel>.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator<TModel>;
        }

        public void SaveModel(TModel model)
        {
            var assetPath = RelativeToProject(GetModelPath(model));
            // Hackish workarround to preserve unity assets compatibility
            // TODO: Look for an alternative way
            AssetDatabase.MoveAsset(assetPath, assetPath + ".temp");
            AssetDatabase.MoveAsset(assetPath + ".temp", assetPath);
        }

        public UnityEditorModelCollection()
        {
            modelType = typeof(TModel);
        }

    }

}
