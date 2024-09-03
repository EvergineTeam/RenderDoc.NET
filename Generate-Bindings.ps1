<#
.SYNOPSIS
	Evergine bindings generator script, (c) 2022 Evergine Team
.DESCRIPTION
	This script generates RenderDoc bindings used in Evergine
	It's meant to have the same behavior when executed locally as when it's executed in a CI pipeline.
.EXAMPLE
	<script>
.LINK
	https://evergine.com/
#>

param (
	[string]$buildVerbosity = "normal",
	[string]$buildConfiguration = "Release",
	[string]$renderdocGenCsprojPath = "RenderDocGen\RenderDocGen\RenderDocGen.csproj"
)

# Utility functions
function LogDebug($line)
{ Write-Host "##[debug] $line" -Foreground Blue -Background Black
}

# Show variables
LogDebug "############## VARIABLES ##############"
LogDebug "Build configuration.: $buildConfiguration"
LogDebug "Build verbosity.....: $buildVerbosity"
LogDebug "#######################################"

# Compile generator
LogDebug "START generator build process"
dotnet publish -v:$buildVerbosity -p:Configuration=$buildConfiguration $renderdocGenCsprojPath
if($?)
{
   LogDebug "END generator build process"
}
else
{
	LogDebug "ERROR; Generator build failed"
   	exit -1
}

# Run generator
LogDebug "START binding generator process"
pushd .\RenderDocGen\RenderDocGen\bin\Release\net8.0\
.\publish\RenderDocGen.exe
if($?)
{
   LogDebug "END binding generator process"
}
else
{
	LogDebug "ERROR; Binding Generation failed"
   	exit -1
}
popd
