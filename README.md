# MeteringPoints

[![codecov](https://codecov.io/gh/Energinet-DataHub/geh-metering-point/branch/main/graph/badge.svg?token=XR3CF7GC90)](https://codecov.io/gh/Energinet-DataHub/geh-metering-point)

## Intro

The metering point domain is in charge of maintaining grid areas and creating the different metering point types in their respective grid areas, as well as maintaining the metering point state.
Furthermore the domain supports metering point master data updates not related to an energy supplier or consumer updates.

These are the processes maintained by this domain.

| Process                                                                      |
| ---------------------------------------------------------------------------- |
| [Create Metering Point](docs/business-processes/create-metering-point.md) |
| [Submission of master data – grid access provider](docs/business-processes/submission-of-master-data-grid-acess-provider.md)                |
| [Close down metering point](docs/business-processes/close-down-metering-point.md)                                               |
| [Connection of metering point with status new](docs/business-processes/connection-of-metering-point-with-status-new.md)                                             |
| [Disconnect or reconnect metering point](docs/business-processes/disconnect-or-reconnect-metering-point.md)                                                            |
| [Change of settlement method](docs/business-processes/change-of-settlement-method.md)                                                        |
| [Update production obligation](docs/business-processes/update-production-obligation.md)                                                              |
| [Request service from grid access provider](docs/business-processes/request-service-from-grid-access-provider.md)                             |
| ....                                                                         |

## Architecture

<img width="615" alt="MP domain Architecture" src="https://user-images.githubusercontent.com/25637982/117973312-87033780-b32c-11eb-9232-32c90cdb0fdb.PNG">

## Context Streams

<img width="412" alt="MP context stream" src="https://user-images.githubusercontent.com/25637982/114844794-6dc5a480-9ddb-11eb-9603-56d6c36a15af.PNG">

## Domain Roadmap

In current program increment we are working on the following>

* We are able to create consumption metering points
* We can messages as described in the CoS sequence diagram (TBD)
* We are able to create exchange metering points
* We are able to create production metering points
* We are able to create Dxx metering points (Danish MP types)
* We publish a MeteringPointCreated event upon MP creation containing MP Type and GSRN

## Getting Started

[Read here how to get started](https://github.com/Energinet-DataHub/green-energy-hub/blob/main/docs/getting-started.md).

## Where can I get more help?

Please see the [community documentation](https://github.com/Energinet-DataHub/green-energy-hub/blob/main/COMMUNITY.md)
