package org.example.Services;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import org.example.Models.Student;
import org.yaml.snakeyaml.Yaml;

import java.io.*;

public class StudentWriter {
    private static void writeStudent(Student student, ObjectOutputStream oos) {
        try{
            oos.writeObject(student);
            oos.flush();
        } catch (Exception e) {
            System.out.println("Error writing student: "+e.getMessage());
        }
    }

    private static void writeStudent(Student student, DataOutputStream dos) {
        try {
            dos.writeUTF(student.getName());
            dos.writeInt(student.getRoomNumber());
            dos.writeDouble(student.getDormPayment());
            dos.writeBoolean(student.hasDiscount());
        } catch (Exception e){
            System.out.println("Error writing student: "+e.getMessage());
        }
    }

    public static void writeStudent(Student student, String fileName) {
        try (FileOutputStream fos = new FileOutputStream(fileName);
             DataOutputStream dos = new DataOutputStream(fos);){
            writeStudent(student, dos) ;
        } catch (Exception e) {
            System.out.println("Error writing student: " + e.getMessage());
        }

        try (
                FileOutputStream fos = new FileOutputStream(fileName);
                BufferedOutputStream bos = new BufferedOutputStream(fos);
                ObjectOutputStream oos = new ObjectOutputStream(bos)
        ) {
            writeStudent(student, oos);
            System.out.println("Student written to file: "+fileName);
        } catch (Exception e) {
            System.out.println("Error writing student: " + e.getMessage());
        }
    }

    public static void writeStudentBasedOnRoom(Student student, String fileName) {
        if (student.getRoomNumber() > 100) {
            System.out.println("Room number > 100, student will not be written");
            return;
        }
        try (
                FileOutputStream fos = new FileOutputStream(fileName);
                DataOutputStream dos = new DataOutputStream(fos)
        ) {
           writeStudent(student, dos);

            System.out.println("Student written to file: "+fileName);
        } catch (Exception e) {
            System.out.println("Error writing student: " + e.getMessage());
            e.printStackTrace();
        }
    }

    public static void writeStudentToJson(Student student, String fileName) {
        Gson gson = new GsonBuilder().setPrettyPrinting().create();
        String json = gson.toJson(student);

        try (Writer writer = new FileWriter(fileName)) {
            writer.write(json);
            System.out.println("Order written to file: "+fileName);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public static void writeStudentToYaml(Student student, String fileName) {
        Student studentToSerialize=student.copy();
        Yaml yaml = new Yaml();
        try (Writer writer = new FileWriter(fileName)) {
            if (studentToSerialize.hasDiscount()) {
                return;
            }
            yaml.dump(studentToSerialize, writer);
            System.out.println("Student written to file: " + fileName);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
