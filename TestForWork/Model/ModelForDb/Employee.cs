namespace TestForWork.Model.DataBase.DbCs;

public class Employee
{
    private string _name;
    private string _secondName;
    private string _lastName;
    private DateTime _dateEmploy;
    private DateTime? _dateUneploy;
    private string _status;
    private string _dep;
    private string _post;

    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public string SecondName
    {
        get => _secondName;
        set => _secondName = value;
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }

    public DateTime DateEmploy
    {
        get => _dateEmploy;
        set => _dateEmploy = value;
    }
    public DateTime? DateUnEmploy
    {
        get => _dateUneploy;
        set => _dateUneploy = value;
    }

    public string Status
    {
        get => _status;
        set => _status = value;
    }
    public string Dep
    {
        get => _dep;
        set => _dep = value;
    }
    public string Post
    {
        get => _post;
        set => _post = value;
    }

    public Employee(string name, string secondName,string lastName,DateTime dateEmploy,DateTime? dateUneploy,string status,string dep,string post)
    {
        Name = name ;
        SecondName =secondName ;
        LastName = lastName;
        DateEmploy = dateEmploy;
        DateUnEmploy = dateUneploy;
        Status =status;
        Dep = dep;
        Post =post ;
    }

    public void Write()
    {
        Console.WriteLine($"Привет {Name} ,{SecondName},{LastName},{DateEmploy},{DateUnEmploy}{Status}{Dep}{Post}");
    }
    
}