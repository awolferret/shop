using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Merchant merchant = new Merchant();
            merchant.Work();
        }
    }

    class Inventory
    {
        protected List<Product> _products = new List<Product>();
    }

    class Merchant : Inventory
    {
        public Player player = new Player();
        private bool _isWorking = true;

        public void Work()
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
                        player.ShowInventory();
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
            _products.Add(new Product("Lamp Oil", 100));
            _products.Add(new Product("Rope", 150));
            _products.Add(new Product("Bomb", 200));
        }

        public void ShowAssortment()
        {
            if (_products.Count > 0)
            {
                for (int i = 0; i < _products.Count; i++)
                {
                    Console.Write($"{i + 1}.");
                    _products[i].ShowProductInfo();
                }
            }
            else
            {
                Console.WriteLine("Нечего предложить");
            }

            ClearConsole();
        }

        public void Buy()
        {
            Console.WriteLine("Какой товар вы хотите купить?");
            string input = Console.ReadLine();
            int number;
            int.TryParse(input, out number);

            if (player.Money >= _products[number - 1].ProductPrice)
            {
                player.BuyProduct(number,_products);
                player.DecreaseBallance(_products[number - 1].ProductPrice);
            }
            else
            {
                Console.WriteLine("Я не даю кредиты, возвращайся когда станешь богаче");
            }

            ClearConsole();
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

    class Player : Inventory
    {
        public int Money { get; private set; }

        public void AddToBallance()
        {
            Money = 500;
        }

        public void DecreaseBallance(int productPrice)
        {
            Money -= productPrice;
        }

        public void ShowMoney()
        {
            Console.WriteLine($"У вас {Money} рублей");
        }

        public void ShowInventory()
        {
            if (_products.Count > 0)
            {
                for (int i = 0; i < _products.Count; i++)
                {
                    Console.Write($"{i + 1}.");
                    _products[i].ShowInventoryProductInfo();
                }
                ShowMoney();
            }
            else
            {
                Console.WriteLine($"В инвентаре пусто, но у вас есть {Money} рублей");
            }

            
        }

        public void BuyProduct(int number,List<Product> _products)
        {
            this._products.Add(new Product(_products[number - 1].ProductName, _products[number - 1].ProductPrice));
            _products.RemoveAt(number - 1);
        }
    }
}