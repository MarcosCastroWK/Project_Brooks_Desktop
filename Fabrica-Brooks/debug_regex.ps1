$formsDir = "c:\Users\Ideapad 3\Desktop\Fabrica-Brooks\SILC.Web\forms"
$file = Join-Path $formsDir "INTEIRO7.ascx"
$content = Get-Content $file -Raw
Write-Host "Content of INTEIRO7.ascx:"
Write-Host $content
if ($content -match 'Inherits="SILC\.Web\.forms\.forms_(\w+)"') {
    Write-Host "Matched!"
} else {
    Write-Host "Not matched."
}
