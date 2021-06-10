# Make Your Own Plan Viewer: viewToken generator console and also MYO PlanViewer

## Overview

This is a console application that illustrates how to generate both a `bearer token` and an AllAreas ```viewerToken``` that enables the use of the JavaScript viewer object as described in [Loading a Plan into a viewer object](https://api.locatrix.com/docs/plans-javascript-sdk/sdk-reference/viewer.html#viewerloadplanviewertoken-options) from [Locatrix](https://locatrix.com) in .NET Core 3.1.

## Prerequisites

- [.NET Core 3.1.402](https://dotnet.microsoft.com/download/dotnet-core/3.1) or higher.

## Quick Start

1. dotnet run to generate ```viewer token```
1. paste ```viewer token``` into javascript-myo-plan-viewer\my-plan-viewer.html at teh appropriate point (see below)
1. view your [MYO Plan Viewer](javascript-myo-plan-viewer\my-plan-viewer.html) in a browser

## Deep Dive

- the ```viewer token``` displayed on the console is valid for 1 hour.
- test out the generated `viewer token` in the example viewer HTML in the file (javascript-myo-plan-viewer\my-plan-viewer.html) in the line that reads
```javascript
        // ---------------------------------------
        // Add your viewer token here - from ESAPI
        // ---------------------------------------
        var allAreasToken = "<your view token>";
```
- a ```viewer token``` can be used multiple times until it expires.
- a new ```viewer token``` is created for each run.