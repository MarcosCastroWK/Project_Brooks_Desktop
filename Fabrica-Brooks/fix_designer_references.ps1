$formsDir = "c:\Users\Ideapad 3\Desktop\Fabrica-Brooks\SILC.Web\forms"
$designerFiles = Get-ChildItem -Path $formsDir -Filter "*.designer.cs" -Recurse

foreach ($file in $designerFiles) {
    $content = Get-Content $file.FullName -Raw
    $newContent = $content
    
    # Replace SILC.Web.forms.forms_XYZ with SILC.Web.forms.XYZ
    if ($newContent -match "SILC\.Web\.forms\.forms_") {
        Write-Host "Fixing references in $($file.Name)"
        $newContent = $newContent -replace "SILC\.Web\.forms\.forms_", "SILC.Web.forms."
    }
    
    # Fix Inteiro2 casing if needed (SILC.Web.forms.Inteiro2 -> SILC.Web.forms.INTEIRO2)
    # Be careful not to break if it's already correct.
    if ($newContent -match "SILC\.Web\.forms\.Inteiro2") {
        Write-Host "Fixing Inteiro2 casing in $($file.Name)"
        $newContent = $newContent -replace "SILC\.Web\.forms\.Inteiro2", "SILC.Web.forms.INTEIRO2"
    }

    if ($newContent -ne $content) {
        Set-Content $file.FullName $newContent
    }
}
