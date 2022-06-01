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
        protected List<Product> Products = new List<Product>();

        public virtual void ShowInventory()
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
                Console.WriteLine("Пусто");
            }
        }
    }

    class Merchant : Inventory
    {
        private Player _player = new Player();
        private bool _isWorking = true;

        public void Work()
        {
            AddToAssortment();
            _player.AddToBallance();

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
                        ShowInventory();
                        break;
                    case "2":
                        Buy();
                        break;
                    case "3":
                        _player.ShowInventory();
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
        private void AddToAssortment()
        {
            Products.Add(new Product("Lamp Oil", 100));
            Products.Add(new Product("Rope", 150));
            Products.Add(new Product("Bomb", 200));
        }

        public override void ShowInventory()
        {
            base.ShowInventory();
        }

        private void Buy()
        {
            Console.WriteLine("Какой товар вы хотите купить?");
            string input = Console.ReadLine();
            int number;
            int.TryParse(input, out number);

            if (_player.Money >= Products[number - 1].ProductPrice)
            {
                _player.BuyProduct(number,Products);
                _player.DecreaseBallance(Products[number - 1].ProductPrice);
            }
            else
            {
                Console.WriteLine("Я не даю кредиты, возвращайся когда станешь богаче");
            }

            ClearConsole();
        }

        private void ClearConsole()
        {
            Console.ReadKey();
            Console.Clear();
        }

        private void Exit()
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

        private void ShowMoney()
        {
            Console.WriteLine($"У вас {Money} рублей");
        }

        public override void ShowInventory()
        {
            base.ShowInventory();
            ShowMoney();
        }

        public void BuyProduct(int number,List<Product> _products)
        {
            this.Products.Add(new Product(_products[number - 1].ProductName, _products[number - 1].ProductPrice));
            _products.RemoveAt(number - 1);
        }
    }
}