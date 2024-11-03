package Services;

import Enums.TransportType;
import Models.Point;
import Models.Trip;

import java.util.EnumMap;
import java.util.Map;

public class TransportService {
    private static final Map<TransportType, Double> averageSpeeds;

    static {
        averageSpeeds = new EnumMap<>(TransportType.class);
        averageSpeeds.put(TransportType.Stop, 0.0);
        averageSpeeds.put(TransportType.Walk, 5.0);
        averageSpeeds.put(TransportType.Bike, 15.0);
        averageSpeeds.put(TransportType.Car, 100.0);
        averageSpeeds.put(TransportType.Bus, 50.0);
        averageSpeeds.put(TransportType.Airplane, 800.0);
    }

    public static double getAverageSpeed(TransportType transportType) {
        return averageSpeeds.getOrDefault(transportType, 0.0);
    }

    public static double getTripTimeInHours(Trip trip) {
        var points = trip.getPoints();
        double totalTime = 0.0;

        for (int i = 0; i < points.size(); i++) {
            Point currentPoint = points.get(i);
            totalTime += currentPoint.getStayDuration().toHours(); // Convert duration to hours

            if (i < points.size() - 1) {
                Point nextPoint = points.get(i + 1);
                double distance = getDistanceTo(currentPoint, nextPoint);
                double speed = getAverageSpeed(currentPoint.getTransportType());

                if (speed > 0) {
                    totalTime += distance / speed;
                }
            }
        }
        return totalTime;
    }

    // in kilometers
    public static double getDistanceTo(Point firstPoint, Point secondPoint) {
        double lat1 = firstPoint.getCoordinate().getLatitude();
        double lon1 = firstPoint.getCoordinate().getLongitude();

        double lat2 = secondPoint.getCoordinate().getLatitude();
        double lon2 = secondPoint.getCoordinate().getLongitude();

        final int R = 6371;
        double deltaLat = Math.toRadians(lat2 - lat1);
        double deltaLon = Math.toRadians(lon2 - lon1);

        double a = Math.sin(deltaLat / 2) * Math.sin(deltaLat / 2)
                + Math.cos(Math.toRadians(lat1)) * Math.cos(Math.toRadians(lat2))
                * Math.sin(deltaLon / 2) * Math.sin(deltaLon / 2);
        double c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

        return R * c;
    }
}
