#if UNITY_EDITOR

using System;
using System.IO;
using UnityEngine;
using AndanteTribe.Utils.Unity;
using MasterMemory;
using UnityEditor;

namespace WhiteWorld.Data
{
    /// <summary>
    /// csvファイルをバイナリに変換するクラス
    /// </summary>
    public class BinaryConverterKit : MonoBehaviour
    {
        /// <summary>
        /// 対象のcsvファイルのパス
        /// </summary>
        [SerializeField] private string _csvFilePath;

        /// <summary>
        /// 変換後のクラス名
        /// </summary>
        [SerializeField] private string _convertClassName;

        /// <summary>
        /// バイナリファイルの名前
        /// </summary>
        [SerializeField] private string _writeBinaryFileName;

        private const string NameSpacePath = "WhiteWorld.Domain.Entity.Runtime.Domain.Entity.";
        private const string WriteBinaryPath = "Assets/Resources/";

        [Button]
        public void BuildBinaryData()
        {
            var type = Type.GetType($"{NameSpacePath}{_convertClassName}, WhiteWorld.Domain.Entity");
            var csvText = File.ReadAllText(_csvFilePath);

            if (type == null)
            {
                Debug.Log("Typeが見つかりませんでした");
                return;
            }

            Debug.Log("Typeを取得しました");

            var deserialize = CsvTranslator.Deserialize(csvText, type);
            var builder = new DatabaseBuilder();

            builder.AnyAppend(deserialize);
            var bytes = builder.Build();
            File.WriteAllBytes($"{WriteBinaryPath}{_writeBinaryFileName}.bytes",bytes);

            AssetDatabase.Refresh();

            Debug.Log($"csvText:{csvText}, Type:{type}, bytes: {bytes}");
        }
    }
}

# endif