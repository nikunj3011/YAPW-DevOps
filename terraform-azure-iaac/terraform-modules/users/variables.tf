variable client_id {}
variable client_secret {}
variable ssh_public_key {}

variable environment {
    default = "production"
}

variable location {
    default = "eastus"
}

variable node_count {
  default = 2
}

variable dns_prefix {
  default = "yapwdns"
}

variable cluster_name {
  default = "yapwcluster"
}

variable resource_group {
  default = "yapw"
}