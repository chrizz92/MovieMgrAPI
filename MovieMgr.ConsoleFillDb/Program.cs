using MovieMgr.Persistence;
using System;

namespace MovieMgr.ConsoleFillDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Löschen und migrieren der Datenbank, Import der Daten aus dem csv-File.....");
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.FillDb();

                int cnt=uow.MovieRepository.GetCount();
                Console.WriteLine("{0} Filme in Datenbank importiert!",cnt);
                Console.WriteLine("<Taste drücken>");
                Console.ReadKey();
            }
        }
    }
}
