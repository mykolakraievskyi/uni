import Enums.PointType;
import Enums.TransportType;
import Models.Coordinate;
import Models.Point;
import Models.Trip;
import Utils.FileReader;
import Utils.RegexMatcher;

import java.time.Duration;
import java.time.LocalDateTime;
import java.util.LinkedList;
import java.util.List;

public class Main {


    public static void main(String[] args) {
        // Task 1
        Trip trip = new Trip();
        trip.setName("Hehe trip");
        trip.setStartTime(LocalDateTime.of(2024, 11, 10, 8, 0)); // Lviv, 10.11.2024 at 08:00 AM

        Point lviv = new Point(PointType.Start, new Coordinate(24.0316, 49.84296)); // Lviv coordinates
        lviv.setName("Lviv");
        lviv.setStayDuration(Duration.ofHours(2));
        lviv.setTransportType(TransportType.Bus);
        trip.addPoints(new LinkedList<>(List.of(lviv)));

        Point kyiv = new Point(PointType.Point, new Coordinate(30.5155, 50.4501)); // Kyiv coordinates
        kyiv.setName("Kyiv");
        kyiv.setStayDuration(Duration.ofDays(1));
        kyiv.setTransportType(TransportType.Bus);
        trip.addPoints(new LinkedList<>(List.of(kyiv)));

        Point rome = new Point(PointType.Point, new Coordinate(12.4964, 41.9028)); // Rome coordinates
        rome.setName("Rome");
        rome.setStayDuration(Duration.ofDays(2));
        rome.setTransportType(TransportType.Airplane);
        trip.addPoints(new LinkedList<>(List.of(rome)));

        Point italianAlps = new Point(PointType.Point, new Coordinate(10.9640, 46.5640)); // Coordinates of Italian Alps
        italianAlps.setName("Italian Alps");
        italianAlps.setStayDuration(Duration.ofDays(1));
        italianAlps.setTransportType(TransportType.Bike);
        trip.addPoints(new LinkedList<>(List.of(italianAlps)));

        Point switzerland = new Point(PointType.Point, new Coordinate(9.5300, 46.8182)); // Coordinates of Switzerland
        switzerland.setName("Switzerland");
        switzerland.setStayDuration(Duration.ofDays(1));
        switzerland.setTransportType(TransportType.Bike);
        trip.addPoints(new LinkedList<>(List.of(switzerland)));

        Point bern = new Point(PointType.Point, new Coordinate(7.4474, 46.9481)); // Bern coordinates
        bern.setName("Bern");
        bern.setStayDuration(Duration.ofHours(1));
        bern.setTransportType(TransportType.Bus);
        trip.addPoints(new LinkedList<>(List.of(bern)));

        Point returnToLviv = new Point(PointType.End, new Coordinate(24.0316, 49.84296)); // Lviv coordinates
        returnToLviv.setName("Return to Lviv");
        returnToLviv.setStayDuration(Duration.ZERO);
        returnToLviv.setTransportType(TransportType.Airplane);
        trip.addPoints(new LinkedList<>(List.of(returnToLviv)));

        trip.calculate();
        System.out.println(trip.toString());

        // Task 2
        // /^.*\(.*\(.*\).*\).*/gm
        System.out.println("Речення, що містять вкладені дужки");
        String regex = "^.*\\(.*\\(.*\\).*\\).*";
        String text = FileReader.readFile("text.txt");
        LinkedList<String> matches = RegexMatcher.getMatches(text, regex);

        matches.forEach(System.out::println);
    }
}