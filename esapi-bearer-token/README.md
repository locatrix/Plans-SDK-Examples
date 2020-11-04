# ESAPI Bearer token generator console

## Overview

This is a console application that simply generates a bearer token that is required for all communications with the [Enterprise Services API](https://api.locatrix.com/esapi/api/docs/index.html) from [Locatrix](https://locatrix.com) in .NET Core 3.1.

## Prerequisites

- [.NET Core 3.1.402](https://dotnet.microsoft.com/download/dotnet-core/3.1) or higher.

## Quick Start

`dotnet run`

## Deep Dive

- the bearer token displayed on the console is valid for 1 hour.
- test out the generated token using the [Swagger API test page](https://api.locatrix.com/esapi/api/docs/index.html).
- a bearer token can be used multiple times until it expires.
- a new bearer token is created for each run.