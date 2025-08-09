using System.IO;
using MasterMemory;
using UnityEngine;

namespace WhiteWorld.Data
{
    public static class BinaryLoader
    {
        /// <summary>
        /// バイナリデータをロードする
        /// </summary>
        /// <param name="path">バイナリデータのパス</param>
        /// <param name="tableName">MemoryTable属性でつけたテーブル名</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static TableBase<T> Load<T>(string path, string tableName) where T : class
        {
            var data =  Resources.Load(path) as TextAsset;

            if (data != null)
            {
                var memoryDataBase = new MemoryDatabase(data.bytes);
                var table = MemoryDatabase.GetTable(memoryDataBase, tableName);

                return (TableBase<T>)table;
            }
            else
            {
                throw new FileNotFoundException("バイナリデータが見つかりませんでした。", path);
            }
        }
    }
}