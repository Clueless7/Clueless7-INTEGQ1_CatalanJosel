using ShoppingAppCommon;
using ShoppingAppDL;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ShoppingAppBL
{
    public class ShoppingAppService
    {
        static public List<Product> GetAllProductsData()
        {
            return SqlData.GetAllProductsData();
        }

        static public Product GetProductData(int productId)
        {
            return SqlData.GetProductData(productId);
        }

        static public string BuyProduct(int quantity, int productId)
        {
            int result = SqlData.ReduceProductQuantity(quantity, productId);

            if (result == -2)
            {
                return "You entered more than the stock amount. Please try again.";
            }
            else if (result == -3)
            {
                return "Sorry, there are no more stocks of this item.";
            }
            else if (result == -4)
            {
                return "Please enter a valid quantity.";
            }
            else
            {
                return "Purchase success";
            }
        }

        static public int GetTotalPrice(int buyingQuantity, int productId)
        {
            int totalPrice = SqlData.GetProductPrice(productId) * buyingQuantity;
            int quantityRemaining = SqlData.GetProductQuantity(productId);

            if (buyingQuantity > quantityRemaining)
            {
                return -2; //Means having greater buying quantity than remaining stocks
            }
            else if (quantityRemaining == 0)
            {
                return -3; //Means there is no remaining stocks
            }
            else if (buyingQuantity < 0)
            {
                return -4; //Means the user entered a negative value
            }

            return totalPrice;
        }

        //var productList = SqlData.GetAllProductsData();
        //foreach (var product in productList)
        //{
        //    Console.WriteLine($"{product.ProductId} - {product.Name}");
        //    //foreach (PropertyInfo prop in product.GetType().GetProperties())
        //    //{
        //    //    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
        //    //    Console.WriteLine(prop.GetValue(product, null).ToString());
        //    //}
        //}
    }
}
