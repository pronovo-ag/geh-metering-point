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
using System.Threading.Tasks;
using Energinet.DataHub.MeteringPoints.Infrastructure.Helpers;
using Energinet.DataHub.MeteringPoints.Infrastructure.IntegrationServices.Helpers;
using Energinet.DataHub.MeteringPoints.Infrastructure.IntegrationServices.Repository;

namespace Energinet.DataHub.MeteringPoints.Infrastructure.IntegrationServices.Services
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IIntegrationEventRepository _integrationEventRepository;

        public EventPublisher(IIntegrationEventRepository integrationEventRepository)
        {
            _integrationEventRepository = integrationEventRepository;
        }

        public async Task<Task> PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent)
            where TIntegrationEvent : IIntegrationEvent
        {
            if (integrationEvent is null)
            {
                throw new ArgumentNullException(nameof(integrationEvent));
            }

            await _integrationEventRepository.SaveIntegrationEventMessageToOutboxAsync(integrationEvent)
                .ConfigureAwait(false);

            return Task.CompletedTask;
        }
    }
}