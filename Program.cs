using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public abstract class LibraryItem
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public LibraryItem(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public abstract void GetInfo();
    }

    public interface IDownloadable
    {
        void Download();
    }

    public class Book : LibraryItem
    {
        public int PageCount { get; set; }

        public Book(string title, string author, int pageCount)
            : base(title, author)
        {
            PageCount = pageCount;
        }

        public override void GetInfo()
        {
            Console.WriteLine($"[Друкована книга] \"{Title}\" — {Author} ({PageCount} стор.)");
        }
    }

    public class EBook : LibraryItem, IDownloadable
    {
        public double FileSizeMb { get; set; }

        public EBook(string title, string author, double fileSizeMb)
            : base(title, author)
        {
            FileSizeMb = fileSizeMb;
        }

        public override void GetInfo()
        {
            Console.WriteLine($"[Електронна книга] \"{Title}\" — {Author} (Розмір: {FileSizeMb} МБ)");
        }

        public void Download()
        {
            Console.WriteLine($"Завантаження книги \"{Title}\" ({FileSizeMb} МБ)... Готово!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Book paperBook = new Book("Крах людини", "Дадзай Осаму", 107);
            EBook digitalBook = new EBook("Собака Баскервілів", "Артур Конан Дойл", 1.2);

            List<LibraryItem> library = new List<LibraryItem>
            {
                paperBook,
                digitalBook
            };

            Console.WriteLine("=== Інформація про елементи бібліотеки ===");
            foreach (var item in library)
            {
                item.GetInfo();
            }

            Console.WriteLine("\n=== Перевірка можливості завантаження ===");
            foreach (var item in library)
            {
                if (item is IDownloadable downloadableItem)
                {
                    downloadableItem.Download();
                }
                else
                {
                    Console.WriteLine($"Книгу \"{item.Title}\" неможливо завантажити (це друковане видання).");
                }
            }

            Console.ReadKey();
        }
    }
}