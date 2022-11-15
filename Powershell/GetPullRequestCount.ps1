function GetPullRequest{
    param(
[string]$org,
[string]$project,
[string]$repo,
[string]$token
)
    $base64AuthInfo = [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(("{0}:{1}" -f "test",$token)))
    $count=0
    $i=0
    do{
        $uri="https://dev.azure.com/$org/$project/_apis/git/repositories/$repo/pullRequests?api-version=5.0&`$top=100&`$skip=$i"
        $i+=100
        Write-Output $uri
        $result= Invoke-RestMethod -Method Get -Uri $Uri -ContentType "application/json" -Headers @{Authorization=("Basic {0}" -f $base64AuthInfo)} -Body $bodyJson
        Write-Output $result
        $count+=$result.Count
        if($result.Count-lt 100){
            break;
        }
    }while($true)
    write-output "Finish. Total Pull Request count: $count";
}

GetPullRequest -org "exxat-team" -project "Evaluation" -repo "Evaluation-Workflow-Service" -token "hifplye6r67fmmb2t7nkt5cxrir727cw37qlbbtxon2tmfmhhbga"