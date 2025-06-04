Connect-AzAccount

$vmName = Read-Host -Prompt "Enter VM name"
$location = Read-Host -Prompt "Enter VM location"
$adminUsername = Read-Host -Prompt "Enter the administrator username"
$adminPassword = Read-Host -Prompt "Enter the administrator password" -AsSecureString
$templateFile = "C:\Development\Azure-Cloud-Services-201\VirtualMachines\virtual-machine-template.json";

New-AzResourceGroupDeployment `
    -Name deployVMToVPN `
    -vmName $vmName `
    -location $location `
    -ResourceGroupName Nate_Main_RG `
    -TemplateFile $templateFile `
    -adminUsername $adminUsername `
    -adminPassword $adminPassword
