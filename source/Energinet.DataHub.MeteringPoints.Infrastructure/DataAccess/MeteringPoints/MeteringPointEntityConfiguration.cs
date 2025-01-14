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
using Energinet.DataHub.MeteringPoints.Domain.GridAreas;
using Energinet.DataHub.MeteringPoints.Domain.MeteringPoints;
using Energinet.DataHub.MeteringPoints.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Energinet.DataHub.MeteringPoints.Infrastructure.DataAccess.MeteringPoints
{
#pragma warning disable SA1402 // All Configurations in this file is related to MeteringPoints
    public class MeteringPointEntityConfiguration : IEntityTypeConfiguration<MeteringPoint>
    {
        public void Configure(EntityTypeBuilder<MeteringPoint> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("MeteringPoints", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(
                    toDbValue => toDbValue.Value,
                    fromDbValue => new MeteringPointId(fromDbValue));

            builder.Property(x => x.GsrnNumber)
                .HasConversion(
                    toDbValue => toDbValue.Value,
                    fromDbValue => GsrnNumber.Create(fromDbValue));

            builder.OwnsOne<Address>("_address", y =>
            {
                y.Property(x => x.StreetName).HasColumnName("StreetName");
                y.Property(x => x.CityName).HasColumnName("CityName");
                y.Property(x => x.CountryCode).HasColumnName("CountryCode");
                y.Property(x => x.PostCode).HasColumnName("PostCode");
            });

            builder.Property<bool>("_isAddressWashable")
                .HasColumnName("IsAddressWashable");

            builder.Property<PhysicalState>("_physicalState")
                .HasColumnName("PhysicalStatusOfMeteringPoint")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<PhysicalState>(fromDbValue));

            builder.Property<MeteringPointSubType>("_meteringPointSubType")
                .HasColumnName("MeteringPointSubType")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<MeteringPointSubType>(fromDbValue));

            builder.Property<MeteringPointType>("_meteringPointType")
                .HasColumnName("TypeOfMeteringPoint")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<MeteringPointType>(fromDbValue));

            builder.Property<GridAreaId>("_gridAreaId")
                .HasColumnName("MeteringGridArea")
                .HasConversion(
                    toDbValue => toDbValue.Value,
                    fromDbValue => new GridAreaId(fromDbValue));

            builder.Property<GsrnNumber>("_powerPlantGsrnNumber")
                .HasColumnName("PowerPlant")
                .HasConversion(toDbValue => toDbValue.Value, fromDbValue => GsrnNumber.Create(fromDbValue));

            builder.Property<string>("_locationDescription")
                .HasColumnName("LocationDescription");

            builder.Property<ProductType>("_productType")
                .HasColumnName("ProductType")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<ProductType>(fromDbValue));

            builder.Property("_parentRelatedMeteringPoint")
                .HasColumnName("ParentRelatedMeteringPoint");

            builder.Property<MeasurementUnitType>("_unitType")
                .HasColumnName("UnitType")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<MeasurementUnitType>(fromDbValue));

            builder.Property("_meterNumber")
                .HasColumnName("MeterNumber");

            builder.Property<ReadingOccurrence>("_meterReadingOccurrence")
                .HasColumnName("MeterReadingOccurrence")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<ReadingOccurrence>(fromDbValue));

            builder.Property("_maximumCurrent")
                .HasColumnName("MaximumCurrent");

            builder.Property("_maximumPower")
                .HasColumnName("MaximumPower");

            builder.Property("_occurenceDate")
                .HasColumnName("OccurenceDate");
        }
    }

    public class ConsumptionMeteringPointEntityConfiguration : IEntityTypeConfiguration<ConsumptionMeteringPoint>
    {
        public void Configure(EntityTypeBuilder<ConsumptionMeteringPoint> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("ConsumptionMeteringPoints", "dbo");

            builder.Property<AssetType>("_assetType")
                .HasColumnName("AssetType")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<AssetType>(fromDbValue));
            builder.Property<ConnectionType>("_connectionType")
                .HasColumnName("ConnectionType")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<ConnectionType>(fromDbValue));

            builder.Property<DisconnectionType>("_disconnectionType")
                .HasColumnName("DisconnectionType")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<DisconnectionType>(fromDbValue));

            builder.Property<SettlementMethod>("_settlementMethod")
                .HasColumnName("SettlementMethod")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<SettlementMethod>(fromDbValue));

            builder.Property("_netSettlementGroup")
                .HasColumnName("NetSettlementGroup");
        }
    }

    public class ProductionMeteringPointEntityConfiguration : IEntityTypeConfiguration<ProductionMeteringPoint>
    {
        public void Configure(EntityTypeBuilder<ProductionMeteringPoint> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("ProductionMeteringPoints", "dbo");

            builder.Property("_productionObligation")
                .HasColumnName("ProductionObligation");
            builder.Property("_netSettlementGroup")
                .HasColumnName("NetSettlementGroup");
            builder.Property<DisconnectionType>("_disconnectionType")
                .HasColumnName("DisconnectionType")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<DisconnectionType>(fromDbValue));
            builder.Property<ConnectionType>("_connectionType")
                .HasColumnName("ConnectionType")
                .HasConversion(
                    toDbValue => toDbValue.Name,
                    fromDbValue => EnumerationType.FromName<ConnectionType>(fromDbValue));
        }
    }

    public class ExchangeMeteringPointEntityConfiguration : IEntityTypeConfiguration<ExchangeMeteringPoint>
    {
        public void Configure(EntityTypeBuilder<ExchangeMeteringPoint> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("ExchangeMeteringPoints", "dbo");

            builder.Property("_toGrid")
                .HasColumnName("ToGrid");
            builder.Property("_fromGrid")
                .HasColumnName("FromGrid");
        }
    }
}
