package org.example.Services;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import org.example.Models.Student;

import java.io.*;

import org.yaml.snakeyaml.Yaml;

public class StudentReader {

    public static Student readStudent(ObjectInputStream ois){
        try{
            return (Student) ois.readObject();
        } catch (Exception e) {
            System.out.println("Error reading student: "+e.getMessage());
        }
        return null;
    }

    public static Student readStudent(DataInputStream dis){
        try{
            String name = dis.readUTF();
            int roomNumber = dis.readInt();
            double payment = dis.readDouble();
            boolean discount = dis.readBoolean();

            Student student = new Student(name, roomNumber, payment, discount);
            return student;
        } catch (Exception e) {
            System.out.println("Error reading student: "+e.getMessage());
            e.printStackTrace();
        }
        return null;
    }

    public static Student readStudent(String filename){
        Student student = null;

        try(FileInputStream fis = new FileInputStream(filename);
            ObjectInputStream ois = new ObjectInputStream(fis)){
            student=readStudent(ois);

            return student;
        } catch (Exception e) {
            System.out.println("Error reading student: "+e.getMessage());
        }
        return null;
    }

    public static Student readStudentBasedOnRoom(String filename){
        Student student = null;

        try(FileInputStream fis = new FileInputStream(filename);
            DataInputStream dis = new DataInputStream(fis)){
            student=readStudent(dis);

            return student;
        } catch (Exception e) {
            System.out.println("Error reading student: "+e.getMessage());
        }
        return null;
    }

    public static Student readStudentJson(String filename){
        try (Reader reader = new FileReader(filename);) {
            Gson gson = new GsonBuilder().setPrettyPrinting().create();
            return gson.fromJson(reader, Student.class);
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        }
    }

    public static Student readStudentYaml(String filename){
        try (InputStream inputStream = new FileInputStream(filename)) {
            Yaml yaml = new Yaml();
            return yaml.loadAs(inputStream, Student.class);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
}
