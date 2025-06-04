Connect-AzAccount

$accountName = Read-Host -Prompt "Enter the account name"
$primaryRegion = Read-Host -Prompt "Enter primary region"
$secondaryRegion = Read-Host -Prompt "Enter secondary region"
$api = Read-Host -Prompt "Enter account type"
$templateFile = "C:\Development\Azure-Cloud-Services-201\AzureCosmosDB\azure-cosmos-db.json"


New-AzResourceGroupDeployment `
    -Name deployCosmosDB `
    -accountName $accountName `
    -api $api `
    -primaryRegion $primaryRegion `
    -secondaryRegion $secondaryRegion `
    -ResourceGroupName Nate_Main_RG `
    -TemplateFile $templateFile