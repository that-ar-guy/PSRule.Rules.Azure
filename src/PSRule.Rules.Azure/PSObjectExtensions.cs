// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections;
using System.IO;
using System.Management.Automation;

namespace PSRule.Rules.Azure;

internal static class PSObjectExtensions
{
    private const string JSON = ".json";

    internal static T GetPropertyValue<T>(this PSObject obj, string propertyName)
    {
        return (T)obj.Properties[propertyName]?.Value;
    }

    public static bool TryProperty<T>(this PSObject o, string name, out T value)
    {
        value = default;
        if (o.Properties[name]?.Value is T tValue)
        {
            value = tValue;
            return true;
        }
        return false;
    }

    internal static bool GetPath(this PSObject sourceObject, out string path)
    {
        path = null;
        if (sourceObject.BaseObject is string s)
        {
            path = s;
            return true;
        }
        if (sourceObject.BaseObject is FileInfo info && info.Extension == JSON)
        {
            path = info.FullName;
            return true;
        }
        return false;
    }

    internal static Hashtable ToHashtable(this PSObject o)
    {
        var result = new Hashtable();
        if (o.BaseObject is IDictionary d)
        {
            foreach (var k in d.Keys)
            {
                result[k.ToString()] = d[k];
            }
        }
        else
        {
            foreach (var p in o.Properties)
            {
                result[p.Name] = p.Value;
            }
        }
        return result;
    }
}
