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

using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Energinet.DataHub.MeteringPoints.Application;
using Energinet.DataHub.MeteringPoints.Application.Transport;
using Energinet.DataHub.MeteringPoints.Application.UserIdentity;

namespace Energinet.DataHub.MeteringPoints.Infrastructure.Ingestion
{
    public class InternalServiceBus : Channel
    {
        private readonly IUserContext _userContext;
        private readonly ICorrelationContext _correlationContext;
        private readonly ServiceBusSender _sender;

        public InternalServiceBus(
            IUserContext userContext,
            ICorrelationContext correlationContext,
            ServiceBusSender sender)
        {
            _userContext = userContext;
            _correlationContext = correlationContext;
            _sender = sender;
        }

        protected override async Task WriteAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            var message = new ServiceBusMessage(data);
            message.CorrelationId = _correlationContext.GetCorrelationId();
            message.ApplicationProperties.Add(_userContext.Key, _userContext.CurrentUser.AsString());

            await _sender.SendMessageAsync(message, cancellationToken).ConfigureAwait(false);
        }
    }
}