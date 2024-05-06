using tutorial08.Models;

namespace tutorial08;

public static class LinqTasks
{
    public static IEnumerable<Emp> Emps { get; set; }
    public static IEnumerable<Dept> Depts { get; set; }

    static LinqTasks()
    {
        var empsCol = new List<Emp>();
        var deptsCol = new List<Dept>();

        #region Load depts

        var d1 = new Dept
        {
            Deptno = 1,
            Dname = "Research",
            Loc = "Warsaw"
        };

        var d2 = new Dept
        {
            Deptno = 2,
            Dname = "Human Resources",
            Loc = "New York"
        };

        var d3 = new Dept
        {
            Deptno = 3,
            Dname = "IT",
            Loc = "Los Angeles"
        };

        deptsCol.Add(d1);
        deptsCol.Add(d2);
        deptsCol.Add(d3);
        Depts = deptsCol;

        #endregion

        #region Load emps

        var e1 = new Emp
        {
            Deptno = 1,
            Empno = 1,
            Ename = "Jan Kowalski",
            HireDate = DateTime.Now.AddMonths(-5),
            Job = "Backend programmer",
            Mgr = null,
            Salary = 2000
        };

        var e2 = new Emp
        {
            Deptno = 1,
            Empno = 20,
            Ename = "Anna Malewska",
            HireDate = DateTime.Now.AddMonths(-7),
            Job = "Frontend programmer",
            Mgr = e1,
            Salary = 4000
        };

        var e3 = new Emp
        {
            Deptno = 1,
            Empno = 2,
            Ename = "Marcin Korewski",
            HireDate = DateTime.Now.AddMonths(-3),
            Job = "Frontend programmer",
            Mgr = null,
            Salary = 5000
        };

        var e4 = new Emp
        {
            Deptno = 2,
            Empno = 3,
            Ename = "Paweł Latowski",
            HireDate = DateTime.Now.AddMonths(-2),
            Job = "Frontend programmer",
            Mgr = e2,
            Salary = 5500
        };

        var e5 = new Emp
        {
            Deptno = 2,
            Empno = 4,
            Ename = "Michał Kowalski",
            HireDate = DateTime.Now.AddMonths(-2),
            Job = "Backend programmer",
            Mgr = e2,
            Salary = 5500
        };

        var e6 = new Emp
        {
            Deptno = 2,
            Empno = 5,
            Ename = "Katarzyna Malewska",
            HireDate = DateTime.Now.AddMonths(-3),
            Job = "Manager",
            Mgr = null,
            Salary = 8000
        };

        var e7 = new Emp
        {
            Deptno = null,
            Empno = 6,
            Ename = "Andrzej Kwiatkowski",
            HireDate = DateTime.Now.AddMonths(-3),
            Job = "System administrator",
            Mgr = null,
            Salary = 7500
        };

        var e8 = new Emp
        {
            Deptno = 2,
            Empno = 7,
            Ename = "Marcin Polewski",
            HireDate = DateTime.Now.AddMonths(-3),
            Job = "Mobile developer",
            Mgr = null,
            Salary = 4000
        };

        var e9 = new Emp
        {
            Deptno = 2,
            Empno = 8,
            Ename = "Władysław Torzewski",
            HireDate = DateTime.Now.AddMonths(-9),
            Job = "CTO",
            Mgr = null,
            Salary = 12000
        };

        var e10 = new Emp
        {
            Deptno = 2,
            Empno = 9,
            Ename = "Andrzej Dalewski",
            HireDate = DateTime.Now.AddMonths(-4),
            Job = "Database administrator",
            Mgr = null,
            Salary = 9000
        };

        empsCol.Add(e1);
        empsCol.Add(e2);
        empsCol.Add(e3);
        empsCol.Add(e4);
        empsCol.Add(e5);
        empsCol.Add(e6);
        empsCol.Add(e7);
        empsCol.Add(e8);
        empsCol.Add(e9);
        empsCol.Add(e10);
        Emps = empsCol;

        #endregion
    }

    /// <summary>
    ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
    /// </summary>
    public static IEnumerable<Emp> Task1()
    {
        IEnumerable<Emp> result = Emps
            .Where(emp => emp.Job == "Backend programmer");

        return result;
    }

    /// <summary>
    ///     SELECT * FROM Emps WHERE Job = "Frontend programmer" AND Salary > 1000 ORDER BY Ename DESC;
    /// </summary>
    public static IEnumerable<Emp> Task2()
    {
        IEnumerable<Emp> result = Emps
            .Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000)
            .OrderByDescending(emp => emp.Ename);

        return result;
    }


    /// <summary>
    ///     SELECT MAX(Salary) FROM Emps;
    /// </summary>
    public static int Task3()
    {
        int result = Emps
            .Max(emp => emp.Salary);

        return result;
    }

    /// <summary>
    ///     SELECT * FROM Emps WHERE Salary = (SELECT MAX(Salary) FROM Emps);
    /// </summary>
    public static IEnumerable<Emp> Task4()
    {
        // int max = Emps
        //     .Max(emp => emp.Salary);
        //
        // IEnumerable<Emp> result = Emps
        //     .Where(emp => emp.Salary == max);

        IEnumerable<Emp> result = Emps
            .Where(emp => emp.Salary == Emps
                .Max(e => e.Salary));
        return result;
    }

    /// <summary>
    ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
    /// </summary>
    public static IEnumerable<object> Task5()
    {
        IEnumerable<object> result = Emps
            .Select(emp => new { Nazwisko = emp.Ename, Praca = emp.Job });
        return result;
    }

    /// <summary>
    ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
    ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
    ///     Rezultat: Złączenie kolekcji Emps i Depts.
    /// </summary>
    public static IEnumerable<object> Task6()
    {
        IEnumerable<object> result = Emps
            .Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new { emp.Ename, emp.Job, dept.Dname });
        return result;
    }

    /// <summary>
    ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
    /// </summary>
    public static IEnumerable<object> Task7()
    {
        IEnumerable<object> result = Emps
            .GroupBy(emp => emp.Job)
            .Select(group => new { Praca = group.Key, LiczbaPracownikow = group.Count() });
        return result;
    }

    /// <summary>
    ///     Return the value "true" if at least one
    ///     of the elements in the collection works as a "Backend programmer".
    /// </summary>
    public static bool Task8()
    {
        bool result = Emps
            .Any(emp => emp.Job == "Backend programmer");

        return result;
    }

    /// <summary>
    ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
    ///     ORDER BY HireDate DESC;
    /// </summary>
    public static Emp Task9()
    {
        Emp result = Emps
            .Where(emp => emp.Job == "Frontend programmer")
            .OrderByDescending(emp => emp.HireDate)
            .FirstOrDefault();

        return result;
    }

    /// <summary>
    ///     SELECT Ename, Job, Hiredate FROM Emps
    ///     UNION
    ///     SELECT "Brak wartości", null, null;
    /// </summary>
    public static IEnumerable<object> Task10()
    {
        IEnumerable<object> result = Emps
            .Select(emp => new { emp.Ename, emp.Job, emp.HireDate })
            .Union(new[] { new { Ename = "Brak wartosci", Job = (string)null, HireDate = (DateTime?)null } });

        return result;
    }

    /// <summary>
    /// Using LINQ, retrieve employees divided by departments, keeping in mind that:
    /// 1. We are only interested in departments with more than 1 employee
    /// 2. We want to return a list of objects with the following structure:
    ///    [
    ///      {name: "RESEARCH", numOfEmployees: 3},
    ///      {name: "SALES", numOfEmployees: 5},
    ///      ...
    ///    ]
    /// 3. Use anonymous types
    /// </summary>
    public static IEnumerable<object> Task11()
    {
        IEnumerable<object> result = Emps
            .Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new { emp, dept })
            .GroupBy(x => x.dept)
            .Where(x => x.Count() > 1)
            .Select(x => new { name = x.Key.Dname, numOfEmployees = x.Count() })
            .ToList();

        return result;
    }

    /// <summary>
    /// Write your own extension method that will allow the following code snippet to compile.
    /// Add the method to the CustomExtensionMethods class, which is defined below.
    ///
    /// The method should return only those employees who have at least 1 direct subordinate.
    /// Employees should be sorted within the collection by surname (ascending) and salary (descending).
    /// </summary>
    public static IEnumerable<Emp> Task12()
    {
        IEnumerable<Emp> result = null;
        return result;
    }

    /// <summary>
    /// The method below should return a single int number.
    /// It takes a list of integers as input.
    /// Try to find, using LINQ, the number that appears an odd number of times in the array of ints.
    /// It is assumed that there will always be one such number.
    /// For example: {1,1,1,1,1,1,10,1,1,1,1} => 10
    /// </summary>
    public static int Task13(int[] arr)
    {
        int result = 0;
        //result=
        return result;
    }

    /// <summary>
    /// Return only those departments that have exactly 5 employees or no employees at all.
    /// Sort the result by department name in ascending order.
    /// </summary>
    public static IEnumerable<Dept> Task14()
    {
        IEnumerable<Dept> result = null;
        //result =
        return result;
    }

    /// <summary>
    ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps
    ///     WHERE Job LIKE '%A%'
    ///     GROUP BY Job
    ///     HAVING COUNT(*)>2
    ///     ORDER BY COUNT(*) DESC;
    /// </summary>
    public static IEnumerable<Dept> Task15()
    {
        IEnumerable<Dept> result = null;
        //result =
        return result;
    }

    /// <summary>
    ///     SELECT * FROM Emps, Depts;
    /// </summary>
    public static IEnumerable<Dept> Task16()
    {
        IEnumerable<Dept> result = null;
        //result =
        return result;
    }
}

public static class CustomExtensionMethods
{
    //Put your extension methods here
    public static IEnumerable<Emp> GetEmpsWithSubordinates(this IEnumerable<Emp> emps)
    {
        var result = emps.Where(e => emps.Any(e2 => e2.Mgr == e.Mgr)).OrderBy(e => e.Ename)
            .ThenByDescending(e => e.Salary);
        return result;
    }
}