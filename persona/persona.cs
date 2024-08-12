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

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}