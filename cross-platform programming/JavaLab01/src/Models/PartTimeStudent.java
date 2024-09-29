package Models;

public class PartTimeStudent extends Student implements IWorker {
    private String labourPlace;

    public PartTimeStudent(String firstname, String lastname, String group, String labourPlace) {
        super(firstname, lastname, group);
        this.labourPlace = labourPlace;
    }

    @Override
    public String toString() {
        return super.toString() + "\nPlace of work: " + labourPlace;
    }

    @Override
    public String getLabourPlace() {
        return this.labourPlace;
    }

    @Override
    public void setLabourPlace(String labourPlace) {
        this.labourPlace = labourPlace;
    }
}
