# script for resetting current highscore
$registryPath = "HKCU:\Software\Unity\UnityEditor\DefaultCompany\Assignment3"

# this variable probably (?) has to be adapted on each pc
$Name = "highscore_h2666423107"


$value = 0
# reset current highscore to 0
New-ItemProperty -Path $registryPath -Name $name -Value $value -PropertyType DWORD -Force | Out-Null

