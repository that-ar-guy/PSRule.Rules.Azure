// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace PSRule.Rules.Azure.Data;

/// <summary>
/// Defines an index of Azure resource provider types.
/// </summary>
internal sealed class TypeIndex(IReadOnlyDictionary<string, TypeIndexEntry> resources)
{

    /// <summary>
    /// Available resource types, indexed by resource type name.
    /// </summary>
    public IReadOnlyDictionary<string, TypeIndexEntry> Resources { get; } = resources;

    public static string GetRelativePath(string provider)
    {
        return string.Concat("providers/", provider, "/types.min.json.deflated");
    }
}
