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

using Energinet.DataHub.MeteringPoints.Domain.GridAreas;
using NodaTime;

namespace Energinet.DataHub.MeteringPoints.Domain.MeteringPoints
{
    public class ExchangeMeteringPoint : MeteringPoint
    {
        private string _fromGrid;
        private string _toGrid;

        public ExchangeMeteringPoint(
            MeteringPointId id,
            GsrnNumber gsrnNumber,
            Address address,
            bool isAddressWashable,
            PhysicalState physicalState,
            MeteringPointSubType meteringPointSubType,
            MeteringPointType meteringPointType,
            GridAreaId gridAreaId,
            GsrnNumber powerPlant,
            string locationDescription,
            string parentRelatedMeteringPoint,
            MeasurementUnitType unitType,
            string meterNumber,
            ReadingOccurrence meterReadingOccurrence,
            int maximumCurrent,
            int maximumPower,
            Instant? occurenceDate,
            string toGrid,
            string fromGrid)
            : base(
                id,
                gsrnNumber,
                address,
                isAddressWashable,
                physicalState,
                meteringPointSubType,
                meteringPointType,
                gridAreaId,
                powerPlant,
                locationDescription,
                parentRelatedMeteringPoint,
                unitType,
                meterNumber,
                meterReadingOccurrence,
                maximumCurrent,
                maximumPower,
                occurenceDate)
        {
            _toGrid = toGrid;
            _fromGrid = fromGrid;
            _productType = ProductType.EnergyReactive;
        }

        private ExchangeMeteringPoint(
            MeteringPointId id,
            GsrnNumber gsrnNumber,
            bool isAddressWashable,
            PhysicalState physicalState,
            MeteringPointSubType meteringPointSubType,
            MeteringPointType meteringPointType,
            GridAreaId gridAreaId,
            GsrnNumber powerPlantGsrnNumber,
            string locationDescription,
            string parentRelatedMeteringPoint,
            MeasurementUnitType unitType,
            string meterNumber,
            ReadingOccurrence meterReadingOccurrence,
            int maximumCurrent,
            int maximumPower,
            Instant? occurenceDate,
            string toGrid,
            string fromGrid)
            : base(
                id,
                gsrnNumber,
                isAddressWashable,
                physicalState,
                meteringPointSubType,
                meteringPointType,
                gridAreaId,
                powerPlantGsrnNumber,
                locationDescription,
                parentRelatedMeteringPoint,
                unitType,
                meterNumber,
                meterReadingOccurrence,
                maximumCurrent,
                maximumPower,
                occurenceDate)
        {
            _toGrid = toGrid;
            _fromGrid = fromGrid;
            _productType = ProductType.EnergyReactive;
        }
    }
}
