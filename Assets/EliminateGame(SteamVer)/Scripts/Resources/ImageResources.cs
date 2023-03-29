using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageResources
{
    private ImageResources() { }
    private static Sprite[] _numberPics;
    public static Sprite[] numberPics
    {
        get
        {
            if (_numberPics == null || _numberPics.Length <= 0)
            {
                _numberPics = Resources.LoadAll<Sprite>("Nums");
            }
            return _numberPics;
        }
    }
}