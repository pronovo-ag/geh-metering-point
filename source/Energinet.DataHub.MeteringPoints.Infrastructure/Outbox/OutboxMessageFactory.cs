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
using Energinet.DataHub.MeteringPoints.Domain.SeedWork;
using Energinet.DataHub.MeteringPoints.Infrastructure.Helpers;

namespace Energinet.DataHub.MeteringPoints.Infrastructure.Outbox
{
    public class OutboxMessageFactory : IOutboxMessageFactory
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ISystemDateTimeProvider _systemDateTimeProvider;

        public OutboxMessageFactory(IJsonSerializer jsonSerializer, ISystemDateTimeProvider systemDateTimeProvider)
        {
            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
            _systemDateTimeProvider = systemDateTimeProvider ?? throw new ArgumentNullException(nameof(systemDateTimeProvider));
        }

        public OutboxMessage CreateFrom<T>(T message, OutboxMessageCategory category)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var type = message.GetType().FullName;
            if (string.IsNullOrEmpty(type))
            {
                throw new OutboxMessageException("Failed to extract message type name.");
            }

            var data = _jsonSerializer.Serialize(message);

            return new OutboxMessage(type, data, category, _systemDateTimeProvider.Now());
        }
    }
}
