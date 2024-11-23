package org.example.Models;

import java.io.Serializable;

public class Student implements Serializable {
    private String name;
    private int roomNumber;
    private double dormPayment;
    private boolean hasDiscount;

    public Student(String name, int roomNumber, double dormPayment, boolean hasDiscount) {
        this.name = name;
        this.roomNumber = roomNumber;
        this.dormPayment = dormPayment;
        this.hasDiscount = hasDiscount;
    }
    public Student() {}
    public Student copy() {
        return new Student(this.name, this.roomNumber, this.dormPayment, this.hasDiscount);
    }
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
    public boolean hasDiscount() {
        return hasDiscount;
    }
    public void setHasDiscount(boolean hasDiscount) {
        this.hasDiscount = hasDiscount;
    }

    @Override
    public String toString() {
        return "Name: " + name + ", room number: " + roomNumber + ", dorm payment: " + dormPayment + ", has discount: " + hasDiscount;
    }
}
