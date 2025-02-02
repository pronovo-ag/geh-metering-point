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
using System.Threading;
using System.Threading.Tasks;
using Energinet.DataHub.MeteringPoints.Application.Common;
using Energinet.DataHub.MeteringPoints.Application.Common.DomainEvents;
using Energinet.DataHub.MeteringPoints.Domain.GridAreas;
using Energinet.DataHub.MeteringPoints.Domain.MeteringPoints;
using Energinet.DataHub.MeteringPoints.Domain.SeedWork;
using MediatR;
using NodaTime;

namespace Energinet.DataHub.MeteringPoints.Application
{
    public class CreateMeteringPointHandler : IRequestHandler<CreateMeteringPoint, BusinessProcessResult>
    {
        private readonly IMeteringPointRepository _meteringPointRepository;

        public CreateMeteringPointHandler(IMeteringPointRepository meteringPointRepository)
        {
            _meteringPointRepository = meteringPointRepository ?? throw new ArgumentNullException(nameof(meteringPointRepository));
        }

        public Task<BusinessProcessResult> Handle(CreateMeteringPoint request, CancellationToken cancellationToken)
        {
            var meteringPoint = new ConsumptionMeteringPoint(
                MeteringPointId.New(),
                GsrnNumber.Create(request.GsrnNumber),
                CreateAddress(request),
                request.InstallationLocationAddress.IsWashable,
                PhysicalState.New,
                EnumerationType.FromName<MeteringPointSubType>(request.SubTypeOfMeteringPoint),
                EnumerationType.FromName<MeteringPointType>(request.TypeOfMeteringPoint),
                new GridAreaId(Guid.NewGuid()),
                GsrnNumber.Create(request.PowerPlant),
                request.LocationDescription,
                request.ParentRelatedMeteringPoint,
                EnumerationType.FromName<MeasurementUnitType>(request.UnitType),
                request.MeterNumber,
                EnumerationType.FromName<ReadingOccurrence>(request.MeterReadingOccurrence),
                request.MaximumCurrent,
                request.MaximumPower,
                SystemClock.Instance.GetCurrentInstant(), // TODO: Parse date in correct format when implemented in Input Validation
                EnumerationType.FromName<SettlementMethod>(request.SettlementMethod),
                request.NetSettlementGroup,
                EnumerationType.FromName<DisconnectionType>(request.DisconnectionType),
                EnumerationType.FromName<ConnectionType>(request.ConnectionType),
                EnumerationType.FromName<AssetType>(request.AssetType));

            _meteringPointRepository.Add(meteringPoint);

            return Task.FromResult(BusinessProcessResult.Ok(request.TransactionId));
        }

        private Domain.MeteringPoints.Address CreateAddress(CreateMeteringPoint request)
        {
            return Domain.MeteringPoints.Address.Create(
                request.InstallationLocationAddress.StreetName,
                request.InstallationLocationAddress.PostCode,
                request.InstallationLocationAddress.CityName,
                request.InstallationLocationAddress.CountryCode);
        }
    }
}
