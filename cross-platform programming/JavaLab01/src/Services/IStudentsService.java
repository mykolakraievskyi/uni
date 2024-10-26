package Services;

import Models.IStudent;

import java.util.List;
import java.util.Map;

public interface IStudentsService {
    Map<Class<?>, List<IStudent>> separateStudents(List<IStudent> students);
    Map<String, List<IStudent>> groupStudents(List<IStudent> students);
    Map<String, Map<Integer, List<IStudent>>> marksAndCorrespondingStudents(List<IStudent> students);
    List<String> UniqueSubjects(List<IStudent> students);
    List<IStudent> sortStudentsBuAverageMarks(List<IStudent> students);
    IStudent getStudentWithTheHighestMark(List<IStudent> students, String subject);
}
