Clear-Host

Connect-AzAccount

$subscriptionName = Read-Host -Prompt "Enter subscription name"
$subscriptionId = Read-Host -Prompt "Enter subscription ID"
$location = Read-Host -Prompt "Enter location"
$resourceGroup = Read-Host -Prompt "Enter resource group"
$storageAccountName = Read-Host -Prompt "Enter storage account name"

Write-Host "Starting script deployment"

New-AzResourceGroup -Name $resourceGroup -Location $location

$storageAccount = New-AzStorageAccount -ResourceGroupName $resourceGroup `
  -Name $storageAccountName `
  -SkuName Standard_LRS `
  -Location $location `
  -EnableHttpsTrafficOnly $TRUE `

$ctx = $storageAccount.Context