using ApplicationLayer.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.BLL.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            // Check if product exists and has stock
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null || !product.IsActive)
                throw new Exception("Product not available");

            if (product.StockQuantity < quantity)
                throw new Exception("Insufficient stock");

            // Check if item already in cart
            var existingCartItem = await _cartRepository.GetCartItemAsync(userId, productId);

            if (existingCartItem != null)
            {
                // Update quantity
                existingCartItem.Quantity += quantity;
                await _cartRepository.UpdateAsync(existingCartItem);
            }
            else
            {
                // Add new item
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    AddedAt = DateTime.UtcNow
                };
                await _cartRepository.AddAsync(cartItem);
            }
        }

        public async Task RemoveFromCartAsync(string userId, int productId)
        {
            await _cartRepository.RemoveAsync(userId, productId);
        }

        public async Task<IEnumerable<CartItemDTO>> GetCartItemsAsync(string userId)
        {
            var cartItems = await _cartRepository.GetUserCartItemsAsync(userId);

            return cartItems.Select(ci => new CartItemDTO
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                DiscountPrice = ci.Product.DiscountPrice,
                ImageUrl = ci.Product.ImageUrl,
                Quantity = ci.Quantity,
                SubTotal = ci.Quantity * (ci.Product.DiscountPrice ?? ci.Product.Price)
            });
        }
}
