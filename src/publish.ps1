$profiles = Get-ChildItem -Path . -Recurse -Filter *.pubxml | Where-Object { $_.FullName -match "PublishProfiles" }

if (-not $profiles) {
    Write-Host "No publish profiles found."
    exit 1
}

foreach ($profile in $profiles) {
    $project = Get-ChildItem -Path $profile.Directory.Parent.Parent.FullName -Filter *.csproj | Select-Object -First 1
    if (-not $project) {
        Write-Warning "No project file found for profile: $($profile.FullName)"
        continue
    }

    $profileName = [System.IO.Path]::GetFileNameWithoutExtension($profile.Name)
    Write-Host "Publishing $($project.Name) using profile $profileName ..."
    dotnet publish $project.FullName -c Release /p:PublishProfile=$profileName
}

exit 1
