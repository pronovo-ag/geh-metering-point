# Copyright 2020 Energinet DataHub A/S
#
# Licensed under the Apache License, Version 2.0 (the "License2");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
module "azfun_processing" {
  source                                    = "git::https://github.com/Energinet-DataHub/geh-terraform-modules.git//function-app?ref=1.2.0"
  name                                      = "azfun-processing-${var.project}-${var.organisation}-${var.environment}"
  resource_group_name                       = data.azurerm_resource_group.main.name
  location                                  = data.azurerm_resource_group.main.location
  storage_account_access_key                = module.azfun_processing_stor.primary_access_key
  storage_account_name                      = module.azfun_processing_stor.name
  app_service_plan_id                       = module.azfun_processing_plan.id
  application_insights_instrumentation_key  = module.appi.instrumentation_key
  tags                                      = data.azurerm_resource_group.main.tags
  always_on                                 = true
  app_settings                              = {
    # Region: Default Values
    WEBSITE_ENABLE_SYNC_UPDATE_SITE       = true
    WEBSITE_RUN_FROM_PACKAGE              = 1
    WEBSITES_ENABLE_APP_SERVICE_STORAGE   = true
    FUNCTIONS_WORKER_RUNTIME              = "dotnet-isolated"
    # Endregion: Default Values
    # VALIDATION_REPORTS_QUEUE_TOPIC        = data.azurerm_key_vault_secret.VALIDATION_REPORTS_QUEUE_TOPIC.value
    # VALIDATION_REPORTS_URL                = data.azurerm_key_vault_secret.VALIDATION_REPORTS_QUEUE_URL.value
    # VALIDATION_REPORTS_CONNECTION_STRING  = data.azurerm_key_vault_secret.VALIDATION_REPORTS_CONNECTION_STRING.value
    METERINGPOINT_QUEUE_URL                 = "${module.sbn_meteringpoint.name}.servicebus.windows.net:9093"
    METERINGPOINT_QUEUE_CONNECTION_STRING   = module.sbnar_meteringpoint_listener.primary_connection_string
    METERINGPOINT_DB_CONNECTION_STRING      = local.METERING_POINT_CONNECTION_STRING
    METERINGPOINT_QUEUE_TOPIC_NAME          = module.sbq_meteringpoint.name
    # POST_OFFICE_QUEUE_CONNECTION_STRING   = data.azurerm_key_vault_secret.POST_OFFICE_QUEUE_CONNECTION_STRING.value
    # POST_OFFICE_QUEUE_TOPIC_NAME          = data.azurerm_key_vault_secret.POST_OFFICE_QUEUE_MARKETDATA_TOPIC_NAME.value
  }
  dependencies                              = [
    module.appi.dependent_on,
    module.azfun_processing_plan.dependent_on,
    module.azfun_processing_stor.dependent_on,
    module.sbnar_meteringpoint_listener.dependent_on,
    module.sbq_meteringpoint.dependent_on,
  ]
}

module "azfun_processing_plan" {
  source              = "git::https://github.com/Energinet-DataHub/geh-terraform-modules.git//app-service-plan?ref=1.2.0"
  name                = "asp-processing-${var.project}-${var.organisation}-${var.environment}"
  resource_group_name = data.azurerm_resource_group.main.name
  location            = data.azurerm_resource_group.main.location
  kind                = "FunctionApp"
  sku                 = {
    tier  = "Basic"
    size  = "B1"
  }
  tags                = data.azurerm_resource_group.main.tags
}

module "azfun_processing_stor" {
  source                    = "git::https://github.com/Energinet-DataHub/geh-terraform-modules.git//storage-account?ref=1.2.0"
  name                      = "stor${random_string.processing.result}"
  resource_group_name       = data.azurerm_resource_group.main.name
  location                  = data.azurerm_resource_group.main.location
  account_replication_type  = "LRS"
  access_tier               = "Cool"
  account_tier              = "Standard"
  tags                      = data.azurerm_resource_group.main.tags
}

# Since all functions need a storage connected we just generate a random name
resource "random_string" "processing" {
  length  = 10
  special = false
  upper   = false
}