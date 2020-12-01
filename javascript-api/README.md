# JavaScript API

## Overview

Contains hard wired examples of usage of the JavaScript API.  

This example creates an interactive plan viewer and illustrates basically how the Embed API was created.  The HTML creates and loads a `locatrix.plans.Viewer` JavaScript object and points it at the `id` of the HTML `div` element that is to contain the rendered plan.

You can then use the [JavaScript API](https://api.locatrix.com/docs/plans-javascript-sdk/sdk-reference/viewer.html) methods to modify the way that the plan viewer behaves to create your own custom interactive viewers.

## Prerequisites

- Download the HTML file!

## Quick Start

- Download the HTML file.
- Open the file in a modern browser.

## Deep Dive

That's it.  Try some of the viewers' interactive capabilities:
- `panning` - [mouse-click-down] + [mouse-move] 
- `zoom` - mouse wheel
- `mini-map` displayed when you are zoomed in and the plan is clipped to your screen limits
- `scalebar` - note that the scale bar updates in line with the current zoom level

## Online Documentation

[Locatrix API Documentation](https://api.locatrix.com/docs/)