using System;
using System.Collections.Generic;
using System.Linq;

// Interface (Contract)
public interface IRepository<T>
{
    void Add(T item);
    T GetById(int id);
    List<T> GetAll();
    void Update(T item);
    void Delete(int id);
}

// Employee Class
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }

    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Department: {Department}");
        Console.WriteLine($"Salary: ${Salary}");
        Console.WriteLine("------------------------");
    }
}

// Employee Repository (Data Management)
public class EmployeeRepository : IRepository<Employee>
{
    private List<Employee> employees = new List<Employee>();
    private int nextId = 1;

    // Create - Add new employee
    public void Add(Employee employee)
    {
        employee.Id = nextId++;
        employees.Add(employee);
        Console.WriteLine("✓ Employee added successfully!");
    }

    // Read - Get employee by ID
    public Employee GetById(int id)
    {
        return employees.FirstOrDefault(e => e.Id == id);
    }

    // Read - Get all employees
    public List<Employee> GetAll()
    {
        return employees;
    }

    // Update - Update employee information
    public void Update(Employee updatedEmployee)
    {
        var employee = GetById(updatedEmployee.Id);
        if (employee != null)
        {
            employee.Name = updatedEmployee.Name;
            employee.Department = updatedEmployee.Department;
            employee.Salary = updatedEmployee.Salary;
            Console.WriteLine("✓ Employee updated successfully!");
        }
        else
        {
            Console.WriteLine("✗ Employee not found!");
        }
    }

    // Delete - Remove employee
    public void Delete(int id)
    {
        var employee = GetById(id);
        if (employee != null)
        {
            employees.Remove(employee);
            Console.WriteLine("✓ Employee deleted successfully!");
        }
        else
        {
            Console.WriteLine("✗ Employee not found!");
        }
    }
}

// Main Program
class Program
{
    static EmployeeRepository repository = new EmployeeRepository();

    static void Main(string[] args)
    {
        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEmployee();
                    break;
                case "2":
                    ViewAllEmployees();
                    break;
                case "3":
                    ViewEmployeeById();
                    break;
                case "4":
                    UpdateEmployee();
                    break;
                case "5":
                    DeleteEmployee();
                    break;
                case "6":
                    Console.WriteLine("Thank you for using the system!");
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n========== Employee Management System ==========");
        Console.WriteLine("1. Add New Employee");
        Console.WriteLine("2. View All Employees");
        Console.WriteLine("3. Search Employee by ID");
        Console.WriteLine("4. Update Employee");
        Console.WriteLine("5. Delete Employee");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");
    }

    static void AddEmployee()
    {
        Console.WriteLine("\n--- Add New Employee ---");
        
        Employee emp = new Employee();
        
        Console.Write("Enter Name: ");
        emp.Name = Console.ReadLine();
        
        Console.Write("Enter Department: ");
        emp.Department = Console.ReadLine();
        
        Console.Write("Enter Salary: ");
        emp.Salary = decimal.Parse(Console.ReadLine());
        
        repository.Add(emp);
    }

    static void ViewAllEmployees()
    {
        Console.WriteLine("\n--- Employee List ---");
        var employees = repository.GetAll();
        
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found!");
            return;
        }
        
        foreach (var emp in employees)
        {
            emp.DisplayInfo();
        }
    }

    static void ViewEmployeeById()
    {
        Console.Write("\nEnter Employee ID: ");
        int id = int.Parse(Console.ReadLine());
        
        var emp = repository.GetById(id);
        if (emp != null)
        {
            emp.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Employee not found!");
        }
    }

    static void UpdateEmployee()
    {
        Console.Write("\nEnter Employee ID to update: ");
        int id = int.Parse(Console.ReadLine());
        
        var emp = repository.GetById(id);
        if (emp == null)
        {
            Console.WriteLine("Employee not found!");
            return;
        }
        
        Console.WriteLine("Current Information:");
        emp.DisplayInfo();
        
        Employee updated = new Employee { Id = id };
        
        Console.Write("Enter New Name: ");
        updated.Name = Console.ReadLine();
        
        Console.Write("Enter New Department: ");
        updated.Department = Console.ReadLine();
        
        Console.Write("Enter New Salary: ");
        updated.Salary = decimal.Parse(Console.ReadLine());
        
        repository.Update(updated);
    }

    static void DeleteEmployee()
    {
        Console.Write("\nEnter Employee ID to delete: ");
        int id = int.Parse(Console.ReadLine());
        
        repository.Delete(id);
    }
}
