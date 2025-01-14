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

using System.Linq;
using Energinet.DataHub.MeteringPoints.Domain.SeedWork;
using Energinet.DataHub.MeteringPoints.Infrastructure.DataAccess;

namespace Energinet.DataHub.MeteringPoints.Infrastructure.Outbox
{
    public class OutboxManager : IOutboxManager
    {
        private readonly MeteringPointContext _context;
        private readonly ISystemDateTimeProvider _dateTimeProvider;

        public OutboxManager(
            MeteringPointContext context,
            ISystemDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public void Add(OutboxMessage message)
        {
            _context.OutboxMessages.Add(message);
        }

        public OutboxMessage? GetNext(OutboxMessageCategory category)
        {
            return _context.OutboxMessages
                .OrderBy(message => message.CreationDate)
                .Where(message => !message.ProcessedDate.HasValue)
                .FirstOrDefault(message => message.Category == category);
        }

        public void MarkProcessed(OutboxMessage outboxMessage)
        {
            var processedMessage = _context.OutboxMessages.Single(message => message.Id == outboxMessage.Id);
            processedMessage.SetProcessed(_dateTimeProvider.Now());
        }
    }
}
