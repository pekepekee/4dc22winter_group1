using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _32ba
{
    public class CsvReader
    {
        /// <summary>
        /// CSVを読み込み、配列に書き出し、結果をboolで返す関数
        /// </summary>
        /// <param name="csvFile">読み込みたいCSVファイル</param>
        /// <param name="list">書き出される配列</param>
        /// <param name="separator">区切り文字(オプション)</param>
        /// <returns></returns>
        public static bool Read(TextAsset csvFile, List<string[]> list, char separator = ',')
        {
            try
            {
                StringReader reader = new StringReader(csvFile.text);
                while (reader.Peek() != -1)//ファイル末尾になるまで
                {
                    string line = reader.ReadLine();//一行ずつ読む
                    if (line.Contains("\\n")) line = line.Replace(@"\n", Environment.NewLine);//改行文字で改行
                    list.Add(line.Split(separator));//リストへ追加
                }
            }
            catch (Exception e)//なんかエラーが起きたら
            {
                Debug.LogError(e);
                return false;
            }

            return true;
        }
    }
}

