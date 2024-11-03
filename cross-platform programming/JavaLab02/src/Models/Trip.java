package Models;

import Services.TransportService;

import java.time.LocalDateTime;
import java.util.LinkedList;
import java.util.StringJoiner;

public class Trip {
    private String name;
    private boolean isCalculated = false;
    private final LinkedList<Point> points = new LinkedList<>();
    private LocalDateTime startTime;

    public boolean isCalculated() {
        return isCalculated;
    }

    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public LinkedList<Point> getPoints() {
        return points;
    }
    public void addPoints(LinkedList<Point> points) {
        this.points.addAll(points);
    }
    public LocalDateTime getStartTime() {
        return startTime;
    }
    public void setStartTime(LocalDateTime startTime) {
        CalculatedCheck();
        this.startTime = startTime;
    }

    public boolean calculate() {
        this.isCalculated = true;
        return true;
    }

    private void CalculatedCheck() {
        if (this.isCalculated) {
            throw new IllegalStateException("The trip has already been calculated");
        }
    }

    public LocalDateTime  getEstimatedEndTime() {
        if (startTime == null || !isCalculated) {
            throw new IllegalStateException("Start time must be set and trip must be calculated.");
        }
        double totalTripTimeInHours = TransportService.getTripTimeInHours(this);
        return startTime.plusHours((long) totalTripTimeInHours).plusMinutes((long) ((totalTripTimeInHours - (long) totalTripTimeInHours) * 60));
    }

    @Override
    public String toString() {
        StringJoiner tripDetails = new StringJoiner("\n");
        tripDetails.add("Trip Summary:");
        tripDetails.add("Name: " + (name != null ? name : "Unnamed Trip"));
        tripDetails.add("Start Time: " + (startTime != null ? startTime : "Not Set"));
        tripDetails.add("Is Calculated: " + isCalculated);
        tripDetails.add("Points:\n");

        for (Point point : points) {
            tripDetails.add("\t  - " + point.toString());
        }

        tripDetails.add("Estimated End Time: " + getEstimatedEndTime());

        return tripDetails.toString();
    }
}

