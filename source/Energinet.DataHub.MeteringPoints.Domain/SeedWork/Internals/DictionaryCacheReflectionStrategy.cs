﻿// Copyright 2020 Energinet DataHub A/S
//
// Licensed under the Apache License, Version 2.0 (the "License2");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Energinet.DataHub.MeteringPoints.Domain.SeedWork.Internals
{
    internal class DictionaryCacheReflectionStrategy : ReflectionStrategy
    {
        private static int _typeIndex = -1;
        private readonly int _cacheSizeIncrement;
        private readonly object _resizeLock = new();
        private EnumerationReflection[] _cache;

        /// <summary>
        /// Create a <see cref="DictionaryCacheReflectionStrategy"/> with a default cache size increment of 64
        /// </summary>
        internal DictionaryCacheReflectionStrategy()
            : this(64) { }

        /// <summary>
        /// Create a <see cref="DictionaryCacheReflectionStrategy"/> with a custom cache size
        /// </summary>
        /// <param name="cacheSizeIncrement">custom cache size increment</param>
        /// <exception cref="ArgumentOutOfRangeException">if cache is less then 1</exception>
        internal DictionaryCacheReflectionStrategy(int cacheSizeIncrement)
        {
            if (cacheSizeIncrement <= 0) throw new ArgumentOutOfRangeException(nameof(cacheSizeIncrement));
            _cacheSizeIncrement = cacheSizeIncrement;
            _cache = new EnumerationReflection[cacheSizeIncrement];
        }

        internal int CacheSize => _cache.Length;

        internal override IEnumerable<T> GetAll<T>()
        {
            return GetFromCache<T>().GetAll().Cast<T>();
        }

        internal override T FromName<T>(string name)
        {
            return (T)GetFromCache<T>().FromName(name);
        }

        internal override T FromValue<T>(int value)
        {
            return (T)GetFromCache<T>().FromValue(value);
        }

        private EnumerationReflection GetFromCache<T>()
            where T : EnumerationType
        {
            var index = KeyType<T>.Index;
            if (index < _cache.Length) return _cache[index] ??= EnumerationReflection.Create<T>();

            lock (_resizeLock)
            {
                if (index >= _cache.Length) Array.Resize(ref _cache, index + _cacheSizeIncrement);
            }

            return _cache[index] ??= EnumerationReflection.Create<T>();
        }

        private static class KeyType<T>
        {
            internal static readonly int Index = Interlocked.Increment(ref _typeIndex);
        }
    }
}
