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
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Energinet.DataHub.MeteringPoints.EntryPoints.Outbox
{
    public static class ActorMessageDispatcher
    {
        [Function("ActorMessageDispatcher")]
        public static void Run(
            [TimerTrigger("%ACTOR_MESSAGE_DISPATCH_TRIGGER_TIMER%")] string timerInformation,
            FunctionContext context)
        {
            var logger = context.GetLogger("ActorMessageDispatcher");
            logger.LogInformation($"C# Timer trigger function executed at: {DateTimeOffset.UtcNow} (UTC)");
            logger.LogInformation($"From function timer trigger input: {timerInformation}");
        }
    }
}
