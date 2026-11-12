<#
.SYNOPSIS
    Builds projects that use the TestKit packages from a folder, the way a consumer's project does.

.DESCRIPTION
    The build and the tests use the projects; only a consumer sees what the package adds: the .targets, the data file
    task it loads, driver.js and the target framework check. This builds
    - a net10.0 project shaped like the pp-test-ui-selenium template, with data files that use @extends, and checks
      its output;
    - a net8.0 project, which must stop with TESTKIT001.

.EXAMPLE
    ./.github/scripts/Test-Package.ps1 -Feed ./artifacts/packages -Version 1.1.0-local
#>
[CmdletBinding()]
param(
    # The folder with the TALXIS.TestKit.Bindings, .Selectors and .Selectors.Browser packages.
    [Parameter(Mandatory)] [string] $Feed,
    # Their version.
    [Parameter(Mandatory)] [string] $Version
)

$ErrorActionPreference = 'Stop'
$Feed = (Resolve-Path $Feed).Path
$work = Join-Path ([IO.Path]::GetTempPath()) "testkit-package-$([Guid]::NewGuid().ToString('N').Substring(0, 8))"
New-Item -ItemType Directory -Path $work | Out-Null
# A package cache of its own, so a package with the same version from an earlier run is not used instead.
$env:NUGET_PACKAGES = Join-Path $work 'packages'
$failures = [Collections.Generic.List[string]]::new()

function New-Consumer([string] $Name, [string] $TargetFramework) {
    $dir = Join-Path $work $Name
    New-Item -ItemType Directory -Path (Join-Path $dir 'Features') | Out-Null
    $feedXml = [Security.SecurityElement]::Escape($Feed)
    Set-Content (Join-Path $dir 'nuget.config') @"
<configuration>
  <packageSources>
    <clear />
    <add key="testkit" value="$feedXml" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
"@
    # The references the pp-test-ui-selenium template generates (MSTest variant).
    Set-Content (Join-Path $dir "$Name.csproj") @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>$TargetFramework</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.8.1" />
    <PackageReference Include="Reqnroll.MsTest" Version="2.*" />
    <PackageReference Include="MSTest.TestFramework" Version="3.*" />
    <PackageReference Include="MSTest.TestAdapter" Version="3.*" />
    <PackageReference Include="FluentAssertions" Version="6.12.0" />
    <PackageReference Include="TALXIS.TestKit.Bindings" Version="$Version" />
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="10.0.10" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="10.0.10" />
  </ItemGroup>
  <ItemGroup>
    <None Update="reqnroll.json" CopyToOutputDirectory="Always" />
  </ItemGroup>
</Project>
"@
    Set-Content (Join-Path $dir 'reqnroll.json') '{ "bindingAssemblies": [ { "assembly": "TALXIS.TestKit.Bindings" } ] }'
    Set-Content (Join-Path $dir 'Features/Smoke.feature') @"
Feature: Smoke

Scenario: Log in
    Given I am logged in to the 'Sales Hub' app as 'an admin'
"@
    return $dir
}

function Invoke-Build([string] $Dir) {
    $output = & dotnet build $Dir --configuration Release 2>&1 | Out-String
    return [pscustomobject]@{ ExitCode = $LASTEXITCODE; Output = $output }
}

function Assert([bool] $Condition, [string] $Message) {
    if ($Condition) {
        Write-Host "  ok    $Message"
    } else {
        Write-Host "  FAIL  $Message"
        $failures.Add($Message)
    }
}

Write-Host "net10.0 project with data files"
$current = New-Consumer 'Net10Consumer' 'net10.0'
$data = Join-Path $current 'Data'
New-Item -ItemType Directory -Path (Join-Path $data 'Orders') | Out-Null
Set-Content (Join-Path $data 'base.json') '{ "@logicalName": "account", "@alias": "base account", "name": "Base", "telephone1": "123" }'
Set-Content (Join-Path $data 'derived.json') '{ "@extends": "base", "@alias": "derived account", "name": "Derived" }'
Set-Content (Join-Path $data 'Orders/order.json') '{ "@logicalName": "salesorder", "name": "Order" }'

$build = Invoke-Build $current
Assert ($build.ExitCode -eq 0) 'builds'
if ($build.ExitCode -ne 0) { Write-Host $build.Output }
$bin = Join-Path $current 'bin/Release/net10.0'
Assert (Test-Path (Join-Path $bin 'TALXIS.TestKit.Bindings.dll')) 'the binding assembly is in the output'
Assert (Test-Path (Join-Path $bin 'driver.js')) 'driver.js is in the output'
Assert (Test-Path (Join-Path $bin 'Data/base.json')) 'Data/base.json is compiled'
Assert (Test-Path (Join-Path $bin 'Data/Orders order.json')) "Data/Orders/order.json is compiled as 'Orders order.json'"
$derivedPath = Join-Path $bin 'Data/derived.json'
if (Test-Path $derivedPath) {
    $derived = Get-Content $derivedPath -Raw | ConvertFrom-Json
    Assert ($derived.name -eq 'Derived' -and $derived.telephone1 -eq '123' -and $derived.'@logicalName' -eq 'account') 'derived.json merges base.json'
    Assert ($null -eq $derived.PSObject.Properties['@extends']) 'derived.json no longer has @extends'
} else {
    Assert $false 'Data/derived.json is compiled'
}

Write-Host "net8.0 project"
$old = New-Consumer 'Net8Consumer' 'net8.0'
$build = Invoke-Build $old
Assert ($build.ExitCode -ne 0 -and $build.Output -match 'TESTKIT001') 'stops with TESTKIT001'
if ($build.Output -notmatch 'TESTKIT001') { Write-Host $build.Output }

Remove-Item $work -Recurse -Force -ErrorAction SilentlyContinue
# Exit explicitly: the net8.0 build fails on purpose, and its exit code would otherwise become the script's.
if ($failures.Count -gt 0) {
    Write-Host "$($failures.Count) package check(s) failed: $($failures -join '; ')"
    exit 1
}
Write-Host 'All package checks passed.'
exit 0
