package org.example.Services;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import org.example.Models.Student;

import java.io.*;
import java.util.ArrayList;
import java.util.List;

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

            Student student = new Student(name, roomNumber, payment);
            return student;
        } catch (Exception e) {
            System.out.println("Error reading student: "+e.getMessage());
        }
        return null;
    }

    public static Student readOrderWithoutDishes(String filename){
        Student student = null;
        int num=0;
        List<Dish> dishList=new ArrayList<>();

        try(FileInputStream fis = new FileInputStream(filename+"_orders");
            ObjectInputStream ois = new ObjectInputStream(fis);){
            num=ois.readInt();
            client=readClient(ois);

            return new Order(client, dishList, num);
        } catch (Exception e) {
            System.out.println("Error reading order: "+e.getMessage());
        }
        return null;
    }

    public static Student readStudentJson(String filename){
        try (Reader reader = new FileReader(filename);) {
            Gson gson = new GsonBuilder().setPrettyPrinting().create();
            Student deserializedStudent= gson.fromJson(reader, Student.class);
            return deserializedStudent;
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        }
    }

    public static Student readOrderYaml(String filename){
        try (InputStream inputStream = new FileInputStream(filename)) {
            Yaml yaml = new Yaml();
            Order deserializedOrder = yaml.loadAs(inputStream, Order.class);
            return deserializedOrder;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
}
