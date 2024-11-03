package Models;

import Enums.PointType;
import Enums.TransportType;

import java.time.Duration;

public class Point {
    private PointType type;
    private Coordinate coordinate;
    private String name;
    private TransportType transportType;
    private Duration stayDuration;

    public Point(PointType type, Coordinate coordinate) {
        this.type = type;
        this.coordinate = coordinate;
    }

    public TransportType getTransportType() {
        return transportType;
    }
    public void setTransportType(TransportType transportType) {
        this.transportType = transportType;
    }
    public PointType getType() {
        return type;
    }
    public void setType(PointType type) {
        this.type = type;
    }
    public Coordinate getCoordinate() {
        return coordinate;
    }
    public void setCoordinate(Coordinate coordinate) {
        this.coordinate = coordinate;
    }
    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public Duration getStayDuration() {
        return stayDuration;
    }
    public void setStayDuration(Duration stayDuration) {
        this.stayDuration = stayDuration;
    }

    @Override
    public String toString() {
        return String.format(
                "Name = %s, type = %s, coordinate = (%7.5f, %7.5f), transportType = %s, stayDuration = %s",
                name != null ? name : "Unnamed",
                type,
                coordinate.getLongitude(),
                coordinate.getLatitude(),
                transportType,
                stayDuration != null ? formatDuration(stayDuration) : "None"
        );
    }

    private String formatDuration(Duration duration) {
        long hours = duration.toHours();
        long minutes = duration.toMinutesPart();
        return String.format("%dh:%dm", hours, minutes);
    }
}

