using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static bool ValidateCheckEmptyString(Object thisObject, string fileName, string stringToCheck)
    {
        if (stringToCheck == "")
        {
            Debug.Log(fileName + " is empty and contain a value in object" + thisObject.name.ToString());
            return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// 检查列表是否有空值或者列表为空
    /// </summary>
    /// <param name="thisObject"></param>
    /// <param name="fileName"></param>
    /// <param name="enumerableObjectToCheck"></param>
    /// <returns></returns>
    public static bool ValidateCheckEnumerableValues(Object thisObject, string fileName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

        foreach (var item in enumerableObjectToCheck)
        {
            if (item == null)
            {
                Debug.Log(fileName + " has null values in object" + thisObject.name.ToString());
                error = true;
                
            }
            else
            {
                count++;
            }
        }

        if (count == 0)
        {
            Debug.Log(fileName + " has no values in object" + thisObject.name.ToString());
            error = true;
        }
        
        return error;
    }
}
