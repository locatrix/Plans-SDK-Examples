# Plans-SDK-Examples

## The simplest Example

To dislay a [North Up render of the Locatrix offices](https://api.locatrix.com/plans-static/v1/image?rotation=northUp&partner=ptnr_0djkhp1jlh316r3n48alwnzp9&floor=flr_oopxxdnfthppgvlgunj46p7dn&format=png&w=640&h=360&expiry=2055369600&apikey=65101207-60c6-2250-3ec1-c99f3eb20793&sas=gE8LRRlH3Jw5zJoPiBBtojU6robHJwulxkoNp%2BMIFEo%3D) using the Static API.

## Overview

| Folder        | Type          |Description|
| ------------- |:-------------:| ----|
| esapi-bearer-token | .net core console | console app to simply create a bearer token suitable for use for all calls to the ESAPI. |
| esapi-browser | .net core blazor app | MVC app to demonstrate authorisation, ESAPI connectivity and use of the Embed API to display floorplans on the screen |
|static-api-html|HTML|georeferenced/map overlay examples using a no-install HTML file to illustrate the way that you can create URLs in your applications.|
|embed-api-html| HTML | examples using a no-install HTML file to illustrate the way that you can simply embed interactive plan viewers in your applications.|
|building-footprints|.net core console|application to enumerate all buildings in a partnership.  The launch file is a statically served HTML file which accesses the JavaScript that is created when the application has been run.  The JavaScript file populates/defines an array of GeoJSON objects that are overlayed on a map.  <br/> This example illustrates how to show building footprints in GeoJSON format and overlay them onto a map using Leaflet.js|
|javascript-api| JavaScript | example of use of the JavaScript API to create a simple customisable interactive plan viewer.|