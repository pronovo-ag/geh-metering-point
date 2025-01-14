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

using Energinet.DataHub.MeteringPoints.Application.Common;
using Energinet.DataHub.MeteringPoints.Application.Transport;

namespace Energinet.DataHub.MeteringPoints.Application
{
    public record CreateMeteringPoint(
            Address InstallationLocationAddress,
            string GsrnNumber = "",
            string TypeOfMeteringPoint = "",
            string SubTypeOfMeteringPoint = "",
            string MeterReadingOccurrence = "",
            int MaximumCurrent = 0,
            int MaximumPower = 0,
            string MeteringGridArea = "",
            string PowerPlant = "",
            string LocationDescription = "",
            string ParentRelatedMeteringPoint = "",
            string SettlementMethod = "",
            string UnitType = "",
            string DisconnectionType = "",
            string OccurenceDate = "",
            string MeterNumber = "",
            string TransactionId = "",
            string PhysicalStatusOfMeteringPoint = "",
            string NetSettlementGroup = "",
            string ConnectionType = "",
            string AssetType = "")
        : IBusinessRequest,
            IOutboundMessage,
            IInboundMessage;
}
