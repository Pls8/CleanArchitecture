using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.BLL.Services
{
    public class WishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public async Task AddToWishlistAsync(string userId, int productId)
        {
            // Check if already in wishlist
            var exists = await _wishlistRepository.ExistsAsync(userId, productId);
            if (!exists)
            {
                var wishlistItem = new WishlistItem
                {
                    UserId = userId,
                    ProductId = productId,
                    AddedAt = DateTime.UtcNow
                };
                await _wishlistRepository.AddAsync(wishlistItem);
            }
        }

        public async Task RemoveFromWishlistAsync(string userId, int productId)
        {
            await _wishlistRepository.RemoveAsync(userId, productId);
        }

        public async Task MoveToCartAsync(string userId, int productId, int quantity = 1)
        {
            // Remove from wishlist
            await RemoveFromWishlistAsync(userId, productId);

            // Add to cart (would need cart service injected)
            // await _cartService.AddToCartAsync(userId, productId, quantity);
        }
    }
}
