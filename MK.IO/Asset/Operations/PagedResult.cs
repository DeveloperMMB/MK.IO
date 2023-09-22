﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    public class PagedResult<T>
    {
        public List<T> Results { get; set; }
        public string? NextPageLink { get; set; }
    }
}