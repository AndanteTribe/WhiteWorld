using System;
using System.IO;
using Csv.Annotations;
using MasterMemory;
using UnityEngine;
using ZLinq;

namespace WhiteWorld.Editor
{
    public class BinaryConverter : UnityEditor.EditorWindow
    {
        [UnityEditor.MenuItem("Tool/Build Master")]
        private static void ShowWindow()
        {
            // CsvObjectAttributeを持つクラスを全て取得
            var types = UnityEditor.TypeCache.GetTypesWithAttribute<CsvObjectAttribute>();
            // DatabaseBuilderを初期化
            var builder = new DatabaseBuilder();

            // Assets/CsvDataフォルダ内の全てのcsvファイルを取得
            var inputPath = Path.Combine(Application.dataPath, "Downloads", "CsvData");
            foreach (var csvPath in Directory.EnumerateFiles(inputPath, "*.csv", SearchOption.TopDirectoryOnly))
            {
                // csvPathから拡張子以降と、ファイル名尾"Data"という文字を抜いた部分
                var className = Path.GetFileNameWithoutExtension(csvPath.AsSpan())[..^4].ToString();
                var type = types.AsValueEnumerable()
                    .FirstOrDefault(x => x.Name.AsSpan().Contains(className, StringComparison.Ordinal));
                if (type != null)
                {
                    var bytes = File.ReadAllBytes(csvPath);
                    var data = CsvTranslator.Deserialize(bytes, type);
                    builder.AnyAppend(data);
                }
            }
            // ビルドしてバイナリファイルに書き出す
            var outputPath = Path.Combine(Application.dataPath, "Resources", "MasterData.bytes");
            File.WriteAllBytes(outputPath, builder.Build());
            // AssetDatabaseをリフレッシュして、Resourcesフォルダにあるバイナリファイルを認識させる
            UnityEditor.AssetDatabase.Refresh();
            Debug.Log($"MasterDataをビルドしました: {outputPath}");
        }
    }
}