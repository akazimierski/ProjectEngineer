using UnityEngine;
using System.Collections;

//var proj = new MercatorProjection();
//var G = google.maps;
//var centerPoint = new G.LatLng(49.141404, -121.960988);
//var zoom = 10;
//getCorners(centerPoint, zoom,640,640);

public class MercatorProjection : MonoBehaviour {

    int MERCATOR_RANGE = 256;

    Vector2 pixelOrigin;
    float pixelsPerLonDegree;
    float pixelsPerLonRadian;

    MercatorProjection()
    {
        this.pixelOrigin = new Vector2(MERCATOR_RANGE / 2, MERCATOR_RANGE / 2);
        this.pixelsPerLonDegree = MERCATOR_RANGE / 360;
        this.pixelsPerLonRadian = MERCATOR_RANGE / (2 * Mathf.PI);
    }

    float bound(float value, float opt_min, float opt_max)
    {
        if (opt_min != 0) value = Mathf.Max(value, opt_min);
        if (opt_max != 0) value = Mathf.Min(value, opt_max);
        return value;
    }

    float degreesToRadians(float deg)
    {
        return deg * (Mathf.PI / 180);
    }

    float radiansToDegrees(float rad)
    {
        return rad / (Mathf.PI / 180);
    }

    Vector2 fromLatLngToPoint(Vector2 latLng)
    {
        Vector2 opt_point = new Vector2(0, 0);
        return fromLatLngToPoint(latLng, opt_point);
    }

    Vector2 fromLatLngToPoint(Vector2 latLng, Vector2 opt_point)
    {
        var origin = this.pixelOrigin;
        opt_point.x = origin.x + latLng.y * this.pixelsPerLonDegree;
        // NOTE(appleton): Truncating to 0.9999 effectively limits latitude to
        // 89.189.  This is about a third of a tile past the edge of the world tile.
        var siny = bound(Mathf.Sin(degreesToRadians(latLng.x)), -0.9999f, 0.9999f);
        opt_point.y = origin.y + 0.5f * Mathf.Log((1 + siny) / (1 - siny)) * -this.pixelsPerLonRadian;
        return opt_point;
    }

    Vector2 fromPointToLatLng(Vector2 point)
    {
        var origin = this.pixelOrigin;
        var lng = (point.x - origin.x) / this.pixelsPerLonDegree;
        var latRadians = (point.y - origin.y) / -this.pixelsPerLonRadian;
        var lat = radiansToDegrees(2 * Mathf.Atan(Mathf.Exp(latRadians)) - Mathf.PI / 2);
        return new Vector2(lat, lng);
    }

    //pixelCoordinate = worldCoordinate * Math.pow(2,zoomLevel)


    Vector2[] getCorners(Vector2 center, int zoom, int mapWidth, int mapHeight)
    {
        var scale = Mathf.Pow(2, zoom);
        var centerPx = fromLatLngToPoint(center);

        var SWPoint = new Vector2((centerPx.x - (mapWidth / 2) / scale) , (centerPx.y + (mapHeight / 2) / scale));
        var SWLatLon = fromPointToLatLng(SWPoint);

        var NEPoint = new Vector2 ((centerPx.x + (mapWidth / 2) / scale), (centerPx.y - (mapHeight / 2) / scale));
        var NELatLon = fromPointToLatLng(NEPoint);

        Vector2[] corners = { SWLatLon, NELatLon };
        return corners;
    }


}
