output "client_key" {
  value = azurerm_kubernetes_cluster.terraform-k8s.kube_config.0.client_key
  sensitive = true
}

output "client_certificate" {
  value = azurerm_kubernetes_cluster.terraform-k8s.kube_config.0.client_certificate
  sensitive = true
}

output "cluster_ca_certificate" {
  value = azurerm_kubernetes_cluster.terraform-k8s.kube_config.0.cluster_ca_certificate
  sensitive = true
}

output "cluster_username" {
  value = azurerm_kubernetes_cluster.terraform-k8s.kube_config.0.username
  sensitive = true
}

output "cluster_password" {
  value = azurerm_kubernetes_cluster.terraform-k8s.kube_config.0.password
  sensitive = true
}

output "kube_config" {
  value = azurerm_kubernetes_cluster.terraform-k8s.kube_config_raw
  sensitive = true
}

output "host" {
  value = azurerm_kubernetes_cluster.terraform-k8s.kube_config.0.host
  sensitive = true
}

# output "connection_string" {
#   description = "Connection string for the Azure SQL Database created."
#   sensitive   = true
#   value       = "Server=tcp:${azurerm_sql_server.server.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_sql_database.db.name};Persist Security Info=False;User ID=${azurerm_sql_server.server.administrator_login};Password=${azurerm_sql_server.server.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
# }

# output "database_name" {
#   description = "Database name of the Azure SQL Database created."
#   value       = azurerm_sql_database.db.name
# }

# output "sql_server_fqdn" {
#   description = "Fully Qualified Domain Name (FQDN) of the Azure SQL Database created."
#   value       = azurerm_sql_server.server.fully_qualified_domain_name
# }

# output "sql_server_location" {
#   description = "Location of the Azure SQL Database created."
#   value       = azurerm_sql_server.server.location
# }

# output "sql_server_name" {
#   description = "Server name of the Azure SQL Database created."
#   value       = azurerm_sql_server.server.name
# }

# output "sql_server_version" {
#   description = "Version the Azure SQL Database created."
#   value       = azurerm_sql_server.server.version
# }