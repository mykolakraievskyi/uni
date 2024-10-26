package Services;

import Models.IStudent;
import Models.PartTimeStudent;
import Models.Student;
import Utils.AverageMarkComparator;

import java.util.*;

public class NoStreamStudentService implements IStudentsService{
    @Override
    public String toString() {
        return "Student operation WITHOUT Stream API";
    }

    @Override
    public Map<Class<?>, List<IStudent>> separateStudents(List<IStudent> students) {
        var map = new HashMap<Class<?>, List<IStudent>>();

        map.put(Student.class, new LinkedList<>());
        map.put(PartTimeStudent.class, new LinkedList<>());

        for (var student : students) {
            map.get(student.getClass()).add(student);
        }

        return map;
    }

    @Override
    public Map<String, List<IStudent>> groupStudents(List<IStudent> students) {
        var map = new HashMap<String, List<IStudent>>();

        var groups = new HashSet<>();
        for (var student : students) {
            groups.add(student.getGroup());
        }

        for (var group : groups) {
            map.put((String)group, new LinkedList<>());
        }

        for (var student : students) {
            map.get((String)student.getGroup()).add(student);
        }
        return map;
    }

    @Override
    public Map<String, Map<Integer, List<IStudent>>> marksAndCorrespondingStudents(List<IStudent> students) {
        var result = new HashMap<String, Map<Integer, List<IStudent>>>();

        var subjects = new HashSet<String>();
        for (var student : students) {
            subjects.addAll(student.getMarks().keySet());
        }

        for (var subject : subjects) {
            result.put(subject, new HashMap<>());
            var marks = new HashSet<Integer>();
            for (var student : students) {
                marks.addAll(student.getMarks().values());
            }
            for (var mark : marks) {
                result.get(subject).put(mark, new ArrayList<>());
            }
            for (var student : students) {
                result.get(subject).get(student.getMarks().get(subject)).add(student);
            }
        }
        return result;
    }

    @Override
    public List<String> UniqueSubjects(List<IStudent> students) {
        var subjects = new HashSet<String>();
        for (var student : students) {
            subjects.addAll(student.getMarks().keySet());
        }

        return new LinkedList<>(subjects);
    }

    @Override
    public List<IStudent> sortStudentsBuAverageMarks(List<IStudent> students) {
        var list = new LinkedList<IStudent>(students);
        var sorting = new SortingService<IStudent>();
        var comparator = new AverageMarkComparator();
        sorting.sort(list, comparator);
        return list;
    }

    @Override
    public IStudent getStudentWithTheHighestMark(List<IStudent> students, String subject) {
        if (!students.isEmpty()) {
            var studentWithTheHighestMark = students.getFirst();

            for (var student : students) {
                if (student.getMarks().get(subject) > studentWithTheHighestMark.getMarks().get(subject)) {
                    studentWithTheHighestMark = student;
                }
            }

            return studentWithTheHighestMark;
        }
        return null;
    }
}
