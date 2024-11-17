package org.example.Models;

public class Student {
    private String name;
    private int roomNumber;
    private double dormPayment;

    public Student(String name, int roomNumber, double dormPayment) {
        this.name = name;
        this.roomNumber = roomNumber;
        this.dormPayment = dormPayment;
    }
    public Student() {}
    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public int getRoomNumber() {
        return roomNumber;
    }
    public void setRoomNumber(int roomNumber) {
        this.roomNumber = roomNumber;
    }
    public double getDormPayment() {
        return dormPayment;
    }
    public void setDormPayment(double dormPayment) {
        this.dormPayment = dormPayment;
    }

}
