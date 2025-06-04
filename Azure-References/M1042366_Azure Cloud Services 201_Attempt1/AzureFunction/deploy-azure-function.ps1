Connect-AzAccount

$appName = Read-Host -Prompt "Enter app name"
$runTime = Read-Host -Prompt "Enter run time (node, dotnet, or java]"
$repoURL = Read-Host -Prompt "Enter the repo URL"
$templateFile = "C:\Development\Azure-Cloud-Services-201\AzureFunction\azure-function-template.json";

New-AzResourceGroupDeployment `
    -Name deployAzureFunction `
    -appName $appName `
    -runTime $runTime `
    -repoURL $repoURL `
    -ResourceGroupName Nate_Main_RG `
    -TemplateFile $templateFile
