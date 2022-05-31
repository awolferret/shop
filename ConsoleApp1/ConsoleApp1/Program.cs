using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Shop shop = new Shop();
            shop.Working();
        }
    }
    class Shop
    {
        Player player = new Player();
        private bool _isWorking = true;
        public List<Product> Products = new List<Product>();

        public void Working()
        {
            AddToAssortment();
            player.AddToBallance();

            while (_isWorking)
            {
                Console.WriteLine("1. Посмотреть ассортимент");
                Console.WriteLine("2. Купить предмет");
                Console.WriteLine("3. Посмотреть свои инвентарь");
                Console.WriteLine("4. Выход");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowAssortment();
                        break;
                    case "2":
                        Buy();
                        break;
                    case "3":
                        ShowInventory();
                        break;
                    case "4":
                        Exit();
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод");
                        break;
                }
            }
        }
        public void AddToAssortment()
        {
            Products.Add(new Product("Lamp Oil", 100));
            Products.Add(new Product("Rope", 150));
            Products.Add(new Product("Bomb", 200));
        }

        public void ShowAssortment()
        {
            if (Products.Count > 0)
            {
                for (int i = 0; i < Products.Count; i++)
                {
                    Console.Write($"{i + 1}.");
                    Products[i].ShowProductInfo();
                }
            }
            else
            {
                Console.WriteLine("Нечего предложить");
            }

            ClearConsole();
        }

        public void ShowInventory()
        {
            if (player.inventory.Count > 0)
            {
                for (int i = 0; i < player.inventory.Count; i++)
                {
                    Console.Write($"{i + 1}.");
                    player.inventory[i].ShowInventoryProductInfo();
                }
                player.ShowMoney();
            }
            else
            {
                Console.WriteLine($"В инвентаре пусто, но у вас есть {player.Money} рублей");
            }

            ClearConsole();
        }

        public void Buy()
        {
            Console.WriteLine("Какой товар вы хотите купить?");
            string input = Console.ReadLine();
            int number;
            int.TryParse(input, out number);

            if (player.Money >= Products[number - 1].ProductPrice)
            {
                BuyProduct(number);
                player.ChangeBallance(Products[number - 1].ProductPrice);
            }
            else
            {
                Console.WriteLine("Я не даю кредиты, возвращайся когда станешь богаче");
            }

            ClearConsole();
        }

        public void BuyProduct(int number)
        {
            player.inventory.Add(new Product(Products[number-1].ProductName, Products[number-1].ProductPrice));
            Products.RemoveAt(number-1);
        }

        public void ClearConsole()
        {
            Console.ReadKey();
            Console.Clear();
        }

        public void Exit()
        {
            _isWorking = false;
        }
    }
    class Product
    {
        public string ProductName { get; private set; }
        public int ProductPrice { get; private set; }

        public Product(string productName, int productPrice)
        {
            ProductName = productName;
            ProductPrice = productPrice;
        }

        public void ShowProductInfo()
        {
            Console.WriteLine($"{ProductName} по цене {ProductPrice}");
        }

        public void ShowInventoryProductInfo()
        {
            Console.WriteLine($"{ProductName}");
        }
    }

    class Player
    {
        public int Money { get; private set; }
        public List<Product> inventory = new List<Product>();

        public void AddToBallance()
        {
            Money = 500;
        }

        public void ChangeBallance(int productPrice)
        {
            Money -= productPrice;
        }

        public void ShowMoney()
        {
            Console.WriteLine($"У вас {Money} рублей");
        }
    }
}