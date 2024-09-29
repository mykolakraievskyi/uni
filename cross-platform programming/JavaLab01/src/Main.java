import Models.IStudent;
import Models.PartTimeStudent;
import Models.Student;
import Services.DataService;
import Services.IStudentsService;
import Services.NoStreamStudentService;
import Services.StreamStudentsService;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Random;
import java.util.stream.Collectors;

public class Main {
    static String[] subjects = {
            "Software Development Life Cycle (SDLC)",
            "Database Management Systems",
            "Web Development",
            "Cloud Computing",
            "Microservices Architecture",
            "Continuous Integration and Continuous Deployment (CI/CD)",
            "Data Structures and Algorithms",
            "Artificial Intelligence in Software Engineering"
    };

    public static void main(String[] args) {
       //seed();
       test();
    }

    public static void test() {
        var students = DataService.getStudents();
        var services = new ArrayList<IStudentsService>();
        services.add(new StreamStudentsService());
        services.add(new NoStreamStudentService());

        for (var service: services) {
            System.out.println("\n" + service);
            Arrays.stream(subjects).forEach(subject -> {
                var student = service.getStudentWithTheHighestMark(students, subject);
                if (student == null) {
                    System.out.println("Oops! The are no students for " + subject);
                }
                System.out.println("Subject: " + subject + "\nStudent with the highest mark:\n" + student + "\nMark: " + student.getMarks().get(subject) + "\n");
            });

            var groupedStudents = service.groupStudents(students);
            groupedStudents.forEach((group, groupStudents) -> {
                System.out.println("Group: " + group + "\nStudents: " + groupStudents.stream()
                        .map(student -> student.getFirstname() + " " + student.getLastname())
                        .collect(Collectors.joining(", ")) + "\n");
            });

            var uniqueSubjects = service.UniqueSubjects(students);
            System.out.println("UniqueSubjects: " + uniqueSubjects.stream().collect(Collectors.joining(", ")) + "\n");

            System.out.println("Students sorted by average mark");
            var sortedStudents = service.sortStudentsBuAverageMarks(students).stream()
                    .map(x -> x.getFirstname() + " " + x.getLastname() + ": " + x.getMarks().values().stream().mapToInt(Integer::intValue).average().getAsDouble());
            sortedStudents.forEach(System.out::println);
            System.out.println(" ");

            var separatedStudents = service.separateStudents(students);
            System.out.println("Regular students:\n" + separatedStudents.get(Student.class).stream() .map(x -> x.getFirstname() + " " + x.getLastname()).collect(Collectors.joining(", ")));
            System.out.println("Роботяжки)):\n" + separatedStudents.get(PartTimeStudent.class).stream() .map(x -> x.getFirstname() + " " + x.getLastname()).collect(Collectors.joining(", ")));

            var result = service.marksAndCorrespondingStudents(students);
            for (var subject : result.keySet()) {
                System.out.println("Subject: " + subject);
                for (var mark : result.get(subject).keySet()) {
                    System.out.println("\tMark: " + mark + " - " + result.get(subject).get(mark).stream().map(x -> x.getFirstname() + " " + x.getLastname()).collect(Collectors.joining(", ")));
                }
            }
        }
    }

    public static void seed() {
        List<IStudent> students = new ArrayList<>();
        List<IStudent> partTimeStudents = new ArrayList<>();

        String[] studentFirstnames = {
                "John", "Emily", "Michael", "Sarah", "David",
                "Emma", "Daniel", "Olivia", "James", "Sophia",
                "William", "Ava", "Benjamin", "Mia", "Lucas",
                "Isabella", "Henry", "Charlotte", "Alexander", "Amelia",
                "Ethan", "Abigail", "Matthew", "Ella", "Elijah",
                "Grace", "Joseph", "Chloe", "Samuel", "Scarlett",
                "Jackson", "Victoria", "Sebastian", "Sofia", "Carter",
                "Madison", "David", "Liam", "Avery", "Leo",
                "Lily", "Owen", "Zoe", "Gabriel", "Nora"
        };

        String[] studentLastnames = {
                "Smith", "Johnson", "Williams", "Jones", "Brown",
                "Davis", "Miller", "Wilson", "Moore", "Taylor",
                "Anderson", "Thomas", "Jackson", "White", "Harris",
                "Martin", "Thompson", "Garcia", "Martinez", "Robinson",
                "Clark", "Rodriguez", "Lewis", "Lee", "Walker",
                "Hall", "Allen", "Young", "King", "Wright",
                "Scott", "Torres", "Nguyen", "Hill", "Flores",
                "Green", "Adams", "Nelson", "Baker", "Carter"
        };

        String[] partTimeLabourPlaces = {
                "Starbucks", "Walmart", "McDonald's", "UPS", "Target",
                "Home Depot", "Kroger", "CVS", "Bank of America", "Best Buy",
                "Pizza Hut", "Lowe's", "Subway", "FedEx", "Costco",
                "The Home Depot", "Dunkin'", "Chick-fil-A", "Grocery Outlet", "The Cheesecake Factory"
        };

        for (int i = 0; i < 20; i++) {
            students.add(new Student(studentFirstnames[i], studentLastnames[i], "SE-3" + (i % 4 + 4)));
            partTimeStudents.add(new PartTimeStudent(studentFirstnames[20 + i], studentLastnames[20 + i], "SE-3" + (i % 4 + 4), partTimeLabourPlaces[i]));
        }
        List<IStudent> allStudents = new ArrayList<>();
        allStudents.addAll(students);
        allStudents.addAll(partTimeStudents);

        Random random = new Random();
        allStudents.forEach(student -> {
            Arrays.stream(subjects).forEach(subject -> {
                student.getMarks().put(subject, random.nextInt(5) + 1);
            });
        });

        DataService.writeStudents(allStudents);
    }
}