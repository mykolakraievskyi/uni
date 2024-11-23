package org.example;

import org.example.Models.Student;
import org.example.Services.StudentReader;
import org.example.Services.StudentWriter;

import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        Student studentMykola = new Student("Mykola", 201, 4000, true);
        Student studentAndriy = new Student("Andriy", 18, 4000, false);

        var students = new ArrayList<Student>();
        students.add(studentMykola);
        students.add(studentAndriy);

        System.out.println("\n\n");
        System.out.println(studentMykola);
        System.out.println(studentAndriy);
        System.out.println("\n\n");


        StudentWriter.writeStudent(studentMykola, "student");
        System.out.println("Read from file: " + StudentReader.readStudent("student") + "\n\n");

        StudentWriter.writeStudentBasedOnRoom(studentMykola, "student-room-201");
        System.out.println("Student from 201: " + StudentReader.readStudentBasedOnRoom("student-room-201") + "\n\n");

        StudentWriter.writeStudentBasedOnRoom(studentAndriy, "student-room-18");
        System.out.println("Student from 18: " + StudentReader.readStudentBasedOnRoom("student-room-18") + "\n\n");

        StudentWriter.writeStudentToJson(studentMykola, "student.json");
        System.out.println("Student from json: " + StudentReader.readStudentJson("student.json") + "\n\n");

        StudentWriter.writeStudentToJson(studentAndriy, "student-no-discount.yaml");
        System.out.println("Student from yaml (with no discount): " + StudentReader.readStudentYaml("student-no-discount.yaml") + "\n\n");

        StudentWriter.writeStudentToYaml(studentMykola, "student-discount.yaml");
        System.out.println("Student from yaml (with discount): " + StudentReader.readStudentYaml("student-discount.yaml") + "\n\n");
    }
}