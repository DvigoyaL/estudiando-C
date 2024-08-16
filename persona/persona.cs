public class Persona
{
    private int id;
    private string name;
    private int age;

    public Persona(int id, string name, int age)
    {
        this.id = id;
        this.name = name;
        this.age = age;
    }

    public int getId() { return id; }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Id
    {
        get { return id; }
    }

    public int Age
    {
        get { return age; }
        set { age = value; }
    }
}