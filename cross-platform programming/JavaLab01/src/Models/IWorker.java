package Models;

public interface IWorker {
    default String getLabourPlace() {
        return null;
    }
    default void setLabourPlace(String labourPlace) {
    }
}
