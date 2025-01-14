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
using System.Reflection;

namespace Energinet.DataHub.MeteringPoints.Domain.SeedWork.Internals
{
    internal class EnumerationReflection
    {
        private readonly EnumerationType[] _types;
        private readonly Dictionary<string, EnumerationType> _nameLookup;
        private readonly Dictionary<int, EnumerationType> _valueLookup;

        private EnumerationReflection(
            EnumerationType[] types)
        {
            _types = types;
            _nameLookup = types.ToDictionary(f => f.Name.ToLowerInvariant());
            _valueLookup = types.ToDictionary(f => f.Id);
        }

        public IEnumerable<EnumerationType> GetAll()
        {
            return _types;
        }

        public EnumerationType FromName(string name)
        {
            if (_nameLookup.TryGetValue(name.ToLowerInvariant(), out var type)) return type;

            throw new InvalidOperationException($"EnumerationType not found for: {name}, actually available values: {string.Join(", ", _nameLookup.Values)}");
        }

        public EnumerationType FromValue(int value)
        {
            if (_valueLookup.TryGetValue(value, out var type)) return type;

            throw new InvalidOperationException();
        }

        internal static EnumerationReflection Create<T>()
            where T : EnumerationType
        {
            var fieldInfos = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            var fields = fieldInfos.Select(f => f.GetValue(null)).Cast<EnumerationType>().ToArray();

            return new EnumerationReflection(fields);
        }
    }
}
