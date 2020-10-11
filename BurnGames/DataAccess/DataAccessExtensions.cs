using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEditor;

namespace BurnGames.DataAccess
{
    public static class DataAccessExtensions
    {

        public static List<T> GetPage<T>(this IEnumerable<T> data, int pageSize = 50, int page = 1)
        {

            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 1;

            return data.Skip(pageSize * (page - 1)).Take(pageSize).ToList();

        }

        public static int CountPages<T>(this IEnumerable<T> data, int pageSize = 50)
        {

            if (pageSize <= 0) pageSize = 1;

            return (int)Math.Ceiling((float)data.Count() / (float)pageSize);

        }

    }
}
