# resource "azurerm_mssql_server" "yapw_mssql_server" {
#   name                         = "yapw-sqlserver"
#   resource_group_name          = azurerm_resource_group.resource_group.name
#   location                     = azurerm_resource_group.resource_group.location
#   version                      = "12.0"
#   administrator_login          = "4dm1n157r470r"
#   administrator_login_password = "4-v3ry-53cr37-p455w0rd"
# }

# resource "azurerm_mssql_database" "yapw_db" {
#   name           = "yapw-db"
#   server_id      = azurerm_mssql_server.yapw_mssql_server.id
#   collation      = "SQL_Latin1_General_CP1_CI_AS"
#   license_type   = "LicenseIncluded"
#   max_size_gb    = 2
#   sku_name       = "Basic"
#   zone_redundant = false

#   tags = {
#     Environment = var.environment
#   }
# }