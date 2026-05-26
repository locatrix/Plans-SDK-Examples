# Embed API

## Overview

Contains examples of both siteplans and floorplans.  

Renderings are interactive plan viewers, each in an `iframe` HTML element.  
This example illustrates the way that you can embed interactive floorplan viewers in your applications with minimal effort.

## Prerequisites

- Download the HTML file!

## Quick Start

### Authentication

See: https://api.locatrix.com/docs/esapi/authentication.html for how to get your API credentials.

You will need the following:

- Application ID
- Application Secret
- API Key
- API Secret

Following this, use the API to retrieve viewer tokens for the viewer embedding. See https://api.locatrix.com/docs/esapi/.

### Running

- Download the HTML file.
- Replace the viewer tokens at the top of the file with the ones you have retrieved.
- Open the file in a modern browser.
- As each embedded viewer needs to render a floorplan, it may take several seconds before all are present.

## Deep Dive

That's it.  Try some of the viewers' interactive capabilities:
- `panning` - [mouse-click-down] + [mouse-move] 
- `zoom` - mouse wheel
- `mini-map` displayed when you are zoomed in and the plan is clipped to your screen limits
- `metadata` - right click on icons to see additional information entered information