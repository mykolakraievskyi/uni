package Models;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;

public class Student implements IStudent, Serializable {
    private String firstname;
    private String lastname;
    private String group;
    private final Map<String, Integer> marks = new HashMap<>();

    public Student(String firstname, String lastname, String group) {
        this.firstname = firstname;
        this.lastname = lastname;
        this.group = group;
    }

    @Override
    public String toString() {
        return "Name: " + firstname + "\nLastname: " + lastname + "\nGroup: " + group;
    }

    @Override
    public String getFirstname() {
        return firstname;
    }

    @Override
    public void setFirstname(String firstname) {
        this.firstname = firstname;
    }

    @Override
    public String getLastname() {
        return lastname;
    }

    @Override
    public void setLastname(String lastname) {
        this.lastname = lastname;
    }

    @Override
    public String getGroup() {
        return group;
    }

    @Override
    public void setGroup(String group) {
        this.group = group;
    }

    @Override
    public Map<String, Integer> getMarks() {
        return marks;
    }
}
