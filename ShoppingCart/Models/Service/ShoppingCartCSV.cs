using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic.FileIO;
using ShoppingCart.Commons.Models.Objects;
using ShoppingCart.Commons.Models.Attributes;
using System.Text;
using System.IO;
using ShoppingCart.Commons;
using ShoppingCart.commons.Models.Objects;
using System.Web.SessionState;

namespace ShoppingCart.Models.Service
{
    public static class ShoppingCartCSV
    {
        private static string _productsPath = "/Assets/csv/products.csv";
        private static string _categoriesPath = "/Assets/csv/categories.csv";
        private static string _typesPath = "/Assets/csv/types.csv";
        private static string _ordersPath = "/Assets/csv/orders.csv";
        private static string _orderLinesPath = "/Assets/csv/orderLines.csv";
        private static string _imagesPath = "/Assets/csv/images.csv";

        public static List<Image> GetImages()
        {
            List<Image> images = new List<Image>();
            string mapPath = HttpContext.Current.Server.MapPath(_imagesPath);
            string rootPath = "/Assets/images/";

            bool titleRow = true;
            try
            {
                String[] delimeters = { ";" };

                using (TextFieldParser parser = FileSystem.OpenTextFieldParser(mapPath, delimeters))
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        if (titleRow == false)
                            images.Add(
                                new Image
                                {
                                    Id = int.Parse(fields[0]),
                                    ProductId = int.Parse(fields[1]),
                                    FilePath = rootPath + fields[2]
                                });
                        else
                            titleRow = false;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }

            return images;
        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string mapPath = HttpContext.Current.Server.MapPath(_productsPath);

            
            bool titleRow = true;
            try
            {
                String[] delimeters = { ";" };
                
                using (TextFieldParser parser = FileSystem.OpenTextFieldParser(mapPath, delimeters))
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        var s = GetProductCategories().Where(x => x.Id.Equals(fields[4])).FirstOrDefault();

                        if (titleRow == false)
                            products.Add(
                                new Product
                                {
                                    Id = int.Parse(fields[0]),
                                    Price = Double.Parse(fields[1]),
                                    Model = fields[2],
                                    Brand = fields[3],
                                    Category = GetProductCategories().Where(x => x.Id.Equals(int.Parse(fields[4]))).FirstOrDefault(),//get the object relative to cell match
                                    Type = GetProductTypes().Where(x => x.Id.Equals(int.Parse(fields[5]))).FirstOrDefault(),
                                    Image = GetImages().Where(x => x.Id.Equals(int.Parse(fields[6]))).FirstOrDefault(),
                                    Description = fields[7]
                                });
                        else
                            titleRow = false;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }

            return products;
        }
        public static List<ProductCategory> GetProductCategories()
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            string mapPath = HttpContext.Current.Server.MapPath(_categoriesPath);

            bool titleRow = true;
            try
            {
                String[] delimeters = { ";" };
                using (TextFieldParser parser = FileSystem.OpenTextFieldParser(mapPath, delimeters))
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        if (titleRow == false)
                            productCategories.Add(
                                new ProductCategory
                                {
                                    Id = int.Parse(fields[0]),
                                    Name = fields[1]
                                });
                        else
                            titleRow = false;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }

            return productCategories;
        }
        public static List<ProductType> GetProductTypes()
        {
            List<ProductType> productTypes = new List<ProductType>();
            string mapPath = HttpContext.Current.Server.MapPath(_typesPath);

            bool titleRow = true;
            try
            {
                String[] delimeters = { ";" };
                using (TextFieldParser parser = FileSystem.OpenTextFieldParser(mapPath, delimeters))
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        if (titleRow == false)
                            productTypes.Add(
                                new ProductType
                                {
                                    Id = int.Parse(fields[0]),
                                    Name = fields[1]
                                });
                        else
                            titleRow = false;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }

            return productTypes;
        }
        
        public static List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            string mapPath = HttpContext.Current.Server.MapPath(_ordersPath);

            bool titleRow = true;
            try
            {
                String[] delimeters = { ";" };
                using (TextFieldParser parser = FileSystem.OpenTextFieldParser(mapPath, delimeters))
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        
                        if (titleRow == false)
                        {
                            orders.Add(
                                new Order
                                {
                                    Id = int.Parse(fields[0]),
                                    AccountId = int.Parse(fields[1]),
                                    DeliveryAddress = fields[2],
                                    OrderDate = DateTime.Parse(fields[3]),
                                    DeliveryDate = DateTime.Parse(fields[4]),
                                    DeliveryRecipient = fields[5],
                                    Total = double.Parse(fields[6])
                                });
                        }
                        else
                            titleRow = false;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }

            return orders;
        }

        public static List<OrderLine> GetOrderLines()
        {
            List<OrderLine> orderLines = new List<OrderLine>();
            string mapPath = HttpContext.Current.Server.MapPath(_orderLinesPath);
            
            bool titleRow = true;
            try
            {
                String[] delimeters = { ";" };
                using (TextFieldParser parser = FileSystem.OpenTextFieldParser(mapPath, delimeters))
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        if (titleRow == false)
                        {
                            orderLines.Add
                                (new OrderLine
                                {
                                    OrderId = int.Parse(fields[0]),
                                    Item = new Item
                                    {
                                        Product = GetProduct(int.Parse(fields[1])),
                                        Quantity = int.Parse(fields[2])
                                    }
                                });
                        }
                        else
                            titleRow = false;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }

            return orderLines;
        }

        public static Product GetProduct(int id)
        {
            Product product = GetProducts().Where(x => x.Id == id).FirstOrDefault();

            return product;
        }

        public static void AddOrder(int accountId, Order order, Cart cart) //re/create csv
        {
            string[] headers = { "Id", "AccountId", "DeliveryAddress", "OrderDate", "DeliveryDate", "DeliveryRecipient", "Payable" };
            List<List<string>> rows = new List<List<string>>();
            List<Order> orders = GetOrders().Where(x => x.AccountId.Equals(accountId)).ToList();

            //reinsert current value
            foreach (var row in orders)
            {
                rows.Add
                    (new List<string>()
                        {
                            row.Id.ToString(),
                            accountId.ToString(),
                            row.DeliveryAddress,
                            row.OrderDate.ToString(),
                            row.DeliveryDate.ToString(),
                            row.DeliveryRecipient,
                            row.Total.ToString()
                        }
                );
            };

            int newOrderId = orders.Count == 0 ? 0 : (orders.Max(x => x.Id)) + 1;
            //insert new value
            rows.Add
                (new List<string>()
                {
                    newOrderId.ToString(),
                    accountId.ToString(),
                    order.DeliveryAddress,
                    order.OrderDate.ToString(),
                    order.DeliveryDate.ToString(),
                    order.DeliveryRecipient,
                    order.Total.ToString()
            });
            Save(headers, rows, _ordersPath);

            AddOrderLines(newOrderId, cart.Items);
        }

        public static void AddOrderLines(int newOrderId, List<Item> items)
        {
            string[] headers = { "OrderId", "ProductId", "Quantity", "Total" };
            List<List<string>> rows = new List<List<string>>();
            
            //reinsert current value
            foreach (var row in GetOrderLines())
            {
                rows.Add
                    (new List<string>()
                        {
                            row.OrderId.ToString(),
                            row.Item.Product.Id.ToString(),
                            row.Item.Quantity.ToString(),
                            row.Item.Total.ToString()
                        }
                );
            };
            //insert new value
            foreach (var row in items)
            {
                rows.Add
                    (new List<string>()
                        {
                            newOrderId.ToString(),
                            row.Product.Id.ToString(),
                            row.Quantity.ToString(),
                            row.Total.ToString()
                        }
                );
            };
            Save(headers, rows, _orderLinesPath);
        }

        public static void CancelOrder(int accountId, int orderId)
        {
            List<Order> orders = GetOrders();
            string[] headers = { "Id", "AccountId", "DeliveryAddress", "OrderDate", "DeliveryDate", "DeliveryRecipient", "Payable" };
            List<List<string>> rows = new List<List<string>>();

            Order order = orders.Where(x => (x.Id.Equals(orderId) && x.AccountId.Equals(accountId))).FirstOrDefault();
            orders.Remove(order);

            //re-create orders csv
            foreach (var row in orders)
            {
                rows.Add
                    (new List<string>()
                        {
                            row.Id.ToString(),
                            row.AccountId.ToString(),
                            row.DeliveryAddress,
                            row.OrderDate.ToString(),
                            row.DeliveryDate.ToString(),
                            row.DeliveryRecipient,
                            row.Total.ToString()
                        }
                );
            }
            Save(headers, rows, _ordersPath);

            DeleteOrderLine(order.Id);
        }

        private static void DeleteOrderLine(int orderId)
        {
            string mapPath = HttpContext.Current.Server.MapPath(_orderLinesPath);
            string[] headers = { "OrderId", "ProductId", "Quantity", "Total" };
            List<OrderLine> orderLines = GetOrderLines();
            List<List<string>> rows = new List<List<string>>();
            
            orderLines.RemoveAll(x => x.OrderId.Equals(orderId));

            foreach (var row in orderLines)
            {
                rows.Add
                    (new List<string>()
                        {
                            row.OrderId.ToString(),
                            row.Item.Product.Id.ToString(),
                            row.Item.Quantity.ToString(),
                            row.Item.Total.ToString()
                        }
                );
            };
            Save(headers, rows, _orderLinesPath);
        }

        private static void Save(string[] headers, List<List<string>> rows, string outPath)
        {
            string mapPath = HttpContext.Current.Server.MapPath(outPath);
            string delimiter = ";";

            System.Text.StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(delimiter, headers));
            for (int i = 0; i < rows.Count(); i++)
                sb.AppendLine(string.Join(delimiter, rows[i]));
            File.WriteAllText(mapPath, sb.ToString());
        }
    }
}