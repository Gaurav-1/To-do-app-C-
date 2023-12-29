using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp
{
    class Program
    {
        todoAppDataContext todoContext = new todoAppDataContext();
        todo todo = new todo();

        void ShowTodo()
        {
            var result = from row in todoContext.todos select row;
            if (result.Count() > 0)
            {
                Console.WriteLine("\t\t\t\t\t------Todo Tasks------\n");
                Console.WriteLine("\tTask Id\t\t\t\tTasks\t\t\t\t\tIs Completed\n");
                foreach (var val in result)
                    Console.WriteLine($"\t{val.id}\t\t\t\t{val.task}\t\t\t\t\t{val.isCompleted}");
                Console.Write("\n\n!! Press any key to return !!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No Task Left To Complete");
                Console.ReadKey();
            }
            return;
        }

        void InsertTodo()
        {
            char ch = 'y';

            Console.WriteLine("\t\t\t\t\t------Add Todo Tasks------");
            while (ch == 'y' || ch == 'Y')
            {
                todo todotask = new todo();
                string task = "";
                while (task.Trim() == "")
                {
                    Console.Write("\nTask Name: ");
                    task = Console.ReadLine();
                }

                todotask.task = task;
                todotask.isCompleted = 0;

                try
                {
                    todoContext.todos.InsertOnSubmit(todotask);
                    todoContext.SubmitChanges();
                    Console.WriteLine("\n------Task Added------");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}\n");
                }

                Console.WriteLine("\n------------------------");
                Console.Write("Add More [y/n]: ");
                ch = Console.ReadKey().KeyChar;

                Console.WriteLine("\n");
            }
        }

        void UpdateTodo()
        {
            int id = 0;
            char ch = 'y';
            Console.WriteLine("\t\t\t\t\t------Mark Task Complete or Uncomplete------");
            while (ch == 'y' || ch == 'Y')
            {
                while (id < 1)
                {
                    Console.Write("\nTask Id: ");
                    try
                    {
                        id = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        id = 0;
                    }
                }
                try
                {
                    todo = todoContext.todos.Single(todo => todo.id == id);
                    todo.isCompleted = (byte?)((todo.isCompleted==1)? 0 : 1);
                    todoContext.SubmitChanges();
                    Console.WriteLine("\n------Task Updated------");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}\n");
                }
                id = 0;
                Console.WriteLine("\n------------------------");
                Console.Write("Finish More [y/n]: ");
                ch = Console.ReadKey().KeyChar;
                Console.Write("\n\n");
            }
        }

        void DeleteTodo()
        {
            int id = 0;
            char ch = 'y';
            Console.WriteLine("\t\t\t\t\t------ Finish Task ------");
            while (ch == 'y' || ch =='Y') {
                while (id < 1)
                {
                    Console.Write("\nTask Id: ");
                    try
                    {
                        id = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        id = 0;
                    }
                }
                try { 
                    todo = todoContext.todos.Single(todo => todo.id == id);
                    todoContext.todos.DeleteOnSubmit(todo);
                    todoContext.SubmitChanges();
                    Console.WriteLine("\n------ \\(*-*)/ Task Finished ------\n");
                }
                catch(Exception e) { 
                    Console.WriteLine($"Error: {e.Message}\n");
                }
                id = 0;
                Console.WriteLine("\n------------------------");
                Console.Write("Finish More [y/n]: ");
                ch = Console.ReadKey().KeyChar;

                Console.WriteLine("\n\n");

            }

        }

        static void Main(string[] args)
        {
            Program obj = new Program();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("------MENU------\n");
                Console.WriteLine("1. Show Tasks\n2. Add Task\n3. Mark Complete/Uncomplete\n4. Finish Task\n5. Exit");
                Console.WriteLine("\n------------------------------");
                Console.Write("Select an option: ");
                int ch = 0;
                try
                {
                    ch = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception e)
                {
                    ch = 0;
                }
                Console.Clear();

                switch (ch)
                {
                    case 1:
                        obj.ShowTodo();
                        break;
                    case 2:
                        obj.InsertTodo();
                        break;
                    case 3:
                        obj.UpdateTodo();
                        break;
                    case 4:
                        obj.DeleteTodo();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Select correct option!!!");
                        Console.ReadKey();
                        break;
                }
            }

        }
    }
}
