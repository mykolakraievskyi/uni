package Services;

import Models.IStudent;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.List;

public class DataService {
    public static List<IStudent> getStudents() {
        List<IStudent> students;

        try (FileInputStream fileIn = new FileInputStream("students.ser");
             ObjectInputStream in = new ObjectInputStream(fileIn)) {

            students = (List<IStudent>) in.readObject();
            return students;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    public static void writeStudents(List<IStudent> students) {
        try (FileOutputStream fileOut = new FileOutputStream("students.ser");
             ObjectOutputStream out = new ObjectOutputStream(fileOut)) {

            out.writeObject(students);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
