Connect-AzAccount

$templateFile = "C:\Development\Azure-Cloud-Services-201\VirtualNetworks\virtual-network-template.json"

New-AzResourceGroupDeployment `
  -Name deployvpn `
  -ResourceGroupName Nate_Main_RG `
  -TemplateFile $templateFile