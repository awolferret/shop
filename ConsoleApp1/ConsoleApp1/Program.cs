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
        private bool _isWorking = true;
        private List<Product> _products = new List<Product>();
        Player player = new Player();

        public void Working()
        {
            AddToAssortment();

            while (_isWorking)
            {
                Console.WriteLine("1. Посмотреть ассортимент");
                Console.WriteLine("2. Посмотреть свои инвентарь");
                Console.WriteLine("3. Купить предмет");
                Console.WriteLine("4. Выход");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowAssortment();
                        break;
                    case "2":
                        
                        break;
                }
            }
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
        }
        public void AddToAssortment()
        {
            _products.Add(new Product("Lamp oil", 100, 3));
            _products.Add(new Product("Rope", 150, 2));
            _products.Add(new Product("Bomb", 200, 5));
        }

        public void BuyProduct()
        {
            Console.WriteLine("Введите номер товара который хотите купить");
            string input = Console.ReadLine();
            int number;

            if (int.TryParse(input, out number) && number <= _products.Count)
            {
                Player.Inventory.Add();
            }

        }
    }

    class Product
    {
        private string _productName;
        private int _productPrice;
        private int _productAmount;

        public Product(string productName,int productPrice,int productAmount)
        {
            _productName = productName;
            _productPrice = productPrice;
            _productAmount = productAmount;
        }

        public void ShowProductInfo()
        {
            Console.WriteLine($"Предмет {_productName} по цене {_productPrice} рублей в колличестве {_productAmount}");
        }
    }

    class Player
    {
        private int _money;
        public List<Player> Inventory = new List<Player>();
        //public Player(int money)
        //{
        //    _money = money;
        //}

        public void AddToInventory()
        {
            
        }

        public void ShowPlayerMoney()
        {
            Console.WriteLine($"У вас {_money} рублей");
        }

        public void ShowPlayerProduct()
        {
            
        }

        public void ShowPlayerInvetory()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                Console.Write($"{i+1}.");
                Inventory[i].ShowPlayerProduct();
            }
        }
    }
}