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

namespace Energinet.DataHub.MeteringPoints.Application
{
    /// <summary>
    /// Context for the current scope identified by a correlation id.
    /// </summary>
    public interface ICorrelationContext
    {
        /// <summary>
        /// Get the current correlation id.
        /// </summary>
        string GetCorrelationId();

        /// <summary>
        /// Set the current correlation id.
        /// </summary>
        void SetCorrelationId(string correlationId);
    }
}
