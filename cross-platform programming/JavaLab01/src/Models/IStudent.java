package Models;

import java.util.Map;

public interface IStudent {
    String getFirstname();
    void setFirstname(String firstname);

    String getLastname();
    void setLastname(String lastname);

    String getGroup();
    void setGroup(String group);

    Map<String, Integer> getMarks();
}
