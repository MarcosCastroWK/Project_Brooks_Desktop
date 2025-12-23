$formsDir = "c:\Users\Ideapad 3\Desktop\Fabrica-Brooks\SILC.Web\forms"
$ascxFiles = Get-ChildItem -Path $formsDir -Filter "*.ascx"

foreach ($file in $ascxFiles) {
    $content = Get-Content $file.FullName -Raw
    if ($content -match 'Inherits="SILC\.Web\.forms\.forms_(\w+)"') {
        $className = $matches[1]
        Write-Host "Fixing $className in $($file.Name)"
        
        # Update .ascx
        $newContent = $content -replace 'Inherits="SILC\.Web\.forms\.forms_' + $className + '"', ('Inherits="SILC.Web.forms.' + $className + '"')
        Set-Content $file.FullName $newContent
        
        # Update .ascx.cs
        $csFile = $file.FullName + ".cs"
        if (Test-Path $csFile) {
            $csContent = Get-Content $csFile -Raw
            # Replace class declaration
            $csNewContent = $csContent -replace 'public partial class forms_' + $className, ('public partial class ' + $className)
            # Replace designer partial class if it exists in .cs (unlikely but possible)
            
            Set-Content $csFile $csNewContent
        }
        
        # Update .ascx.designer.cs if exists (just in case)
        $designerFile = $file.FullName + ".designer.cs"
        if (Test-Path $designerFile) {
            $designerContent = Get-Content $designerFile -Raw
             $designerNewContent = $designerContent -replace 'public partial class forms_' + $className, ('public partial class ' + $className)
             Set-Content $designerFile $designerNewContent
        }
    }
}
