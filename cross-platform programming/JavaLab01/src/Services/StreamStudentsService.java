package Services;
import Models.IStudent;
import Models.PartTimeStudent;
import Models.Student;
import java.util.*;
import java.util.stream.Collectors;

public class StreamStudentsService implements IStudentsService {
    @Override
    public String toString() {
        return "Student operation using Stream API";
    }

    @Override
    public Map<Class<?>, List<IStudent>> separateStudents(List<IStudent> students) {
        Map<Class<?>, List<IStudent>> map = new HashMap<>();

        map.put(Student.class, new LinkedList<>());
        map.put(PartTimeStudent.class, new LinkedList<>());

        students.stream().forEach(student -> {
            map.get(student.getClass()).add(student);
        });

        return map;
    }

    @Override
    public Map<String, List<IStudent>> groupStudents(List<IStudent> students) {
        return students.stream().collect(Collectors.groupingBy(IStudent::getGroup));
    }

    @Override
    public Map<String, Map<Integer, List<IStudent>>> marksAndCorrespondingStudents(List<IStudent> students) {
        var result = new HashMap<String, Map<Integer, List<IStudent>>>();
        var subjects = students.stream()
                .map(x -> x.getMarks().keySet())
                .flatMap(Set::stream)
                .distinct()
                .toList();

        subjects.forEach(subject -> {
            result.put(subject, new HashMap<>());

            var marks = students.stream().map(x -> x.getMarks().get(subject)).distinct().collect(Collectors.toList());
            marks.forEach(mark -> {
                result.get(subject).put(mark, new LinkedList<>());
            });
        });

        students.stream().forEach(student -> {
            student.getMarks().forEach((subject, mark) -> {
                result.get(subject).get(mark).add(student);
            });
        });
        return result;
    }

    @Override
    public List<String> UniqueSubjects(List<IStudent> students) {
        return students.stream()
                .map(x -> x.getMarks().keySet())
                .flatMap(Set::stream)
                .distinct()
                .collect(Collectors.toList());
    }

    @Override
    public List<IStudent> sortStudentsBuAverageMarks(List<IStudent> students) {
        return students.stream()
                .sorted(Comparator.comparingDouble((IStudent x) ->
                                x.getMarks().values().stream()
                                        .mapToInt(Integer::intValue)
                                        .average()
                                        .orElse(0.0))
                        .reversed())
                .collect(Collectors.toList());
    }

    @Override
    public IStudent getStudentWithTheHighestMark(List<IStudent> students, String subject) {
        return students.stream().max(Comparator.comparingDouble(x -> x.getMarks().get(subject))).orElse(null);
    }
}
