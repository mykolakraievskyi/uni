package Models;

import Enums.PointType;
import Enums.TransportType;

import java.time.Duration;
import java.time.ZoneId;

public class Point {
    private PointType type;
    private Coordinate coordinate;
    private String name;
    private TransportType transportType;
    private Duration stayDuration;
    private ZoneId timeZone;

    public Point(PointType type, Coordinate coordinate, ZoneId timeZone) {
        this.type = type;
        this.coordinate = coordinate;
        this.timeZone = timeZone;
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
    public ZoneId getTimeZone() {
        return timeZone;
    }

    public void setTimeZone(ZoneId timeZone) {
        this.timeZone = timeZone;
    }

    @Override
    public String toString() {
        return String.format(
                "Name = %s, type = %s, coordinate = (%7.5f, %7.5f), transportType = %s, stayDuration = %s, timeZone = %s",
                name != null ? name : "Unnamed",
                type,
                coordinate.getLongitude(),
                coordinate.getLatitude(),
                transportType,
                stayDuration != null ? formatDuration(stayDuration) : "None",
                timeZone != null ? timeZone : "None"
        );
    }

    private String formatDuration(Duration duration) {
        long hours = duration.toHours();
        long minutes = duration.toMinutesPart();
        return String.format("%dh:%dm", hours, minutes);
    }
}

