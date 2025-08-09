using System;
using Csv;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MasterMemory;
using ZLinq;

namespace WhiteWorld.Data
{
    public static class CsvTranslator
    {
        /// <summary>
        /// 汎用デシアライザー
        /// </summary>
        /// <param name="csvText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Deserialize(string csvText, Type type)
        {
            // Deserialize<T> メソッドをリフレクションで取得
            var deserializeMethod = typeof(CsvSerializer).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .AsValueEnumerable()
                .FirstOrDefault
                (m =>
                    m.Name == "Deserialize" &&
                    m.IsGenericMethodDefinition &&
                    m.GetParameters().Length >= 1 &&
                    m.GetParameters()[0].ParameterType == typeof(string));

            // 指定された型でジェネリックメソッドを作成
            var genericDeserializeMethod = deserializeMethod?.MakeGenericMethod(type);

            // ジェネリックメソッドをCSVテキストで実行
            return genericDeserializeMethod!.Invoke(null, new object[] { csvText, null });
        }

        /// <summary>
        /// 汎用アペンド
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DatabaseBuilder AnyAppend(this DatabaseBuilder builder, object data)
        {
            if (data is IEnumerable)
            {
                // 配列の要素の型を取得
                var elementType = data.GetType().GetElementType();

                if (elementType != null)
                {
                    // IEnumerable<elementType> 型を作成
                    var enumerableType = typeof(IEnumerable<>).MakeGenericType(elementType);

                    // DatabaseBuilder の CustomAppend メソッド（ジェネリック）を取得
                    var appendMethod = typeof(DatabaseBuilder).GetMethod(
                        "Append",
                        new Type[] { enumerableType } // 作成した IEnumerable<elementType> 型を指定
                    );

                    // CustomAppend メソッドを実行
                    appendMethod?.Invoke(builder, new object[] { data });
                }
                else
                {
                    throw new Exception("デシリアライズされたデータは配列ではありません");
                }
            }
            else
            {
                throw new Exception("デシリアライズされたデータはコレクションではありません");
            }

            return builder;
        }
    }
}