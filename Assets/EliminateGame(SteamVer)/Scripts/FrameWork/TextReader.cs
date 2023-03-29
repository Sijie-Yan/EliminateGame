using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReader
{
    private TextReader() { }

    // 逐行读取txt文件，输出每一行内容
    private static string[] readTXTbyLine(string path)
    {
        string[] strs = null;
        TextAsset text = Resources.Load(path) as TextAsset;
        string tempStr = text.text;
        tempStr = TextReader.deleteAllSpace(tempStr);
        strs = tempStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        return strs;
    }

    // 删除字符串中所有空格
    public static string deleteAllSpace(string str)
    {
        string result = "";
        str.Trim();
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] != ' ')
            {
                result += str[i];
            }
        }
        return result;
    }
}