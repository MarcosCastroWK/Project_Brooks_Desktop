$csprojPath = "c:\Users\Ideapad 3\Desktop\Fabrica-Brooks\SILC.Web\SILC.Web.csproj"
try {
    $xml = [xml](Get-Content $csprojPath)
    $nsManager = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
    $nsManager.AddNamespace("ms", "http://schemas.microsoft.com/developer/msbuild/2003")

    $compileItems = $xml.SelectNodes("//ms:Compile", $nsManager)
    Write-Host "Total Compile items: $($compileItems.Count)"

    $seen = @{}
    $duplicates = @()

    foreach ($item in $compileItems) {
        $include = $item.Include
        # Normalize path for comparison (lowercase)
        $key = $include.ToLower()
        
        if ($seen.ContainsKey($key)) {
            $duplicates += $item
            Write-Host "Found duplicate: $include"
        } else {
            $seen[$key] = $true
        }
    }

    if ($duplicates.Count -gt 0) {
        foreach ($item in $duplicates) {
            $item.ParentNode.RemoveChild($item) | Out-Null
        }
        $xml.Save($csprojPath)
        Write-Host "Removed $($duplicates.Count) duplicates."
    } else {
        Write-Host "No duplicates found."
    }
} catch {
    Write-Host "Error: $_"
}
