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
using NodaTime;

namespace Energinet.DataHub.MeteringPoints.Infrastructure.Outbox
{
    public class OutboxMessage
    {
        public OutboxMessage(string type, string data, OutboxMessageCategory category, Instant creationDate)
        {
            Id = Guid.NewGuid();
            Type = type;
            Data = data;
            Category = category;
            CreationDate = creationDate;
        }

        public OutboxMessage(string type, string data, OutboxMessageCategory category, Instant creationDate, Guid id)
        {
            Id = id;
            Type = type;
            Data = data;
            Category = category;
            CreationDate = creationDate;
        }

        public Guid Id { get; }

        public string Type { get; }

        public string Data { get; }

        public OutboxMessageCategory Category { get; }

        public Instant CreationDate { get; }

        public Instant? ProcessedDate { get; internal set; } // TODO: Make private

        public void SetProcessed(Instant when)
        {
            ProcessedDate = when;
        }
    }
}
