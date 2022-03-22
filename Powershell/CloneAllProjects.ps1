# Clone all projects within a azure devops team name. This works only if there is access provided.

$accessToken = "csofej6ly322jog2zf35zav3vgygr2vxmo5crtzlv37hvgs2jwua"
$teamName = "exxat-team"
$projects = @('Evaluation', 'PatientLog', 'Platform', 'V4')

$base64AuthInfo= [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes(":$accessToken"))
$headers = @{Authorization=("Basic {0}" -f $base64AuthInfo)}

$result = Invoke-RestMethod -Uri "https://dev.azure.com/$teamName/_apis/projects?api-version=6.0" -Method Get -Headers $headers

Write-Host $projects

$projects | ForEach-Object {   
    
    $project = $_
    New-Item -Path "D:\Source" -Name $project -ItemType "directory"
    $directory = "D:\Source\" + $project
    Set-Location -Path $directory
    
    $result = Invoke-RestMethod -Uri "https://dev.azure.com/$teamName/$project/_apis/git/repositories?api-version=6.0" -Method Get -Headers $headers      
    
    $repositories = $result.value.remoteUrl
    
    $repositories | ForEach-Object {        
        $repository = $_

        Write-Host $repository + "---"
        git clone $repository
    }    

    Set-Location -Path "..\"
    
} | Sort-Object