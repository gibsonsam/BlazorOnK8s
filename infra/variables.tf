variable "location" {
  description = "Azure region to deploy into"
  type        = string
  default     = "ukwest"
}

variable "resource_group_name" {
  description = "Name of the Azure Resource Group"
  type        = string
  default     = "rg-aspire"
}

variable "acr_name" {
  description = "Name of the Azure Container Registry"
  type        = string
  default     = "myaspireregistry" # Must be globally unique
}

variable "aks_name" {
  description = "Name of the Azure Kubernetes Service cluster"
  type        = string
  default     = "aks-aspire"
}

variable "node_count" {
  description = "Number of nodes in the AKS default node pool"
  type        = number
  default     = 1
}

variable "environment" {
  description = "Name of the target environment"
  type        = string
  default     = "Development"
}