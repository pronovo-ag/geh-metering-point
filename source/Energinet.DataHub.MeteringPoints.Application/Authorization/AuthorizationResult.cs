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
using Energinet.DataHub.MeteringPoints.Domain.SeedWork;

namespace Energinet.DataHub.MeteringPoints.Application.Authorization
{
    public class AuthorizationResult
    {
        public AuthorizationResult(List<ValidationError> errors)
        {
            Errors = errors;
        }

        public AuthorizationResult()
        {
            Errors = new List<ValidationError>();
        }

        public List<ValidationError> Errors { get; }

        public bool Success => !Errors.Any();

        public static AuthorizationResult Ok()
        {
            return new();
        }

        public static AuthorizationResult Error(string reason, Type type)
        {
            return new(new List<ValidationError>
            {
                new(reason, type),
            });
        }
    }
}