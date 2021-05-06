// Copyright 2020 Energinet DataHub A/S
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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Energinet.DataHub.MeteringPoints.Application;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace Energinet.DataHub.MeteringPoints.EntryPoints.Common
{
    public sealed class TelemetryMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly TelemetryClient _telemetryClient;
        private ICorrelationContext _correlationContext;

        public TelemetryMiddleware(
            TelemetryClient telemetryClient,
            ICorrelationContext correlationContext)
        {
            _telemetryClient = telemetryClient;
            _correlationContext = correlationContext;
        }

        public async Task Invoke(FunctionContext context, [NotNull] FunctionExecutionDelegate next)
        {
            // if (context == null) throw new ArgumentNullException(nameof(context));
            //
            // var activity = Activity.Current ?? new Activity(context.FunctionDefinition.Name);
            //
            // // requestActivity.SetIdFormat(ActivityIdFormat.W3C);
            // var parentId = context.TraceContext.TraceParent.Substring(36, 16);
            // if (!string.IsNullOrEmpty(parentId) && parentId != "0000000000000000")
            // {
            //     activity.SetParentId(parentId);
            // }
            //
            // activity.Start();
            //
            // var requestTelemetry = new RequestTelemetry { Name = activity.OperationName };
            // requestTelemetry.Id = activity.Id;
            // requestTelemetry.Context.Operation.Id = activity.RootId;
            // requestTelemetry.Context.Operation.ParentId = activity.ParentId;
            //
            // var operation = _telemetryClient.StartOperation(requestTelemetry);
            await next(context).ConfigureAwait(false);

            // _telemetryClient.StopOperation(operation);
            // _telemetryClient.Flush();
        }
    }
}
