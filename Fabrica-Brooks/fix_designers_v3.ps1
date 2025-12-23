$formsDir = "SILC.Web\forms"
Write-Output "Searching in $formsDir"
if (Test-Path $formsDir) {
    Write-Output "Directory exists."
} else {
    Write-Output "Directory NOT found!"
    exit
}

$designerFiles = Get-ChildItem -Path $formsDir -Filter "*.designer.cs" -Recurse
Write-Output "Found $($designerFiles.Count) designer files."

foreach ($file in $designerFiles) {
    $content = Get-Content $file.FullName -Raw
    $newContent = $content
    $modified = $false
    
    # Replace SILC.Web.forms.forms_XYZ with SILC.Web.forms.XYZ
    if ($newContent -match "SILC\.Web\.forms\.forms_") {
        Write-Output "Fixing references in $($file.Name)"
        $newContent = $newContent -replace "SILC\.Web\.forms\.forms_", "SILC.Web.forms."
        $modified = $true
    }
    
    # Fix Inteiro2 casing
    if ($newContent -match "SILC\.Web\.forms\.Inteiro2") {
        Write-Output "Fixing Inteiro2 casing in $($file.Name)"
        $newContent = $newContent -replace "SILC\.Web\.forms\.Inteiro2", "SILC.Web.forms.INTEIRO2"
        $modified = $true
    }

    if ($modified) {
        Set-Content $file.FullName $newContent
        Write-Output "Updated $($file.Name)"
    }
}
